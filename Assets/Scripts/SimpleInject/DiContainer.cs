using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace SimpleInject
{
	public sealed class DiContainer
	{
		private readonly List<BindingData> _bindingData;
		
		private readonly Dictionary<Type, object> _dependencies;
		private readonly Dictionary<Type, object> _uniqueObjects;

		private readonly List<DiContainer> _parentContainers;
		private readonly List<DiContainer> _searchContainers;

		public DiContainer()
		{
			_dependencies = new Dictionary<Type, object>();
			_uniqueObjects = new Dictionary<Type, object>();
			
			_bindingData = new List<BindingData>();

			_parentContainers = new List<DiContainer>();
			_searchContainers = new List<DiContainer> { this };
		}
		
		// NOTE: For now it's always Bind as Single
		// FIXME: Now a binding has to implement an empty Constructor
		public BindingData BindSelf<T>()
		{
			return Bind<T>(BindingType.Self);
		}
		
		public BindingData BindInterfaces<T>()
		{
			return Bind<T>(BindingType.Interfaces);
		}
		
		public BindingData BindInterfacesAndSelf<T>()
		{
			return Bind<T>(BindingType.Self | BindingType.Interfaces);
		}

		private BindingData Bind<T>(BindingType bindingType)
		{
			Type type = typeof(T);

			BindingData data = new BindingData(bindingType, type);
			_bindingData.Add(data);
			return data;
		}

		private void AddInterfaces(object dependency) {
			Type[] interfaces = dependency.GetType().GetInterfaces();
			foreach (Type @interface in interfaces)
				FillInterfaceList(@interface, dependency);
		}

		private object FillInterfaceList(Type interfaceType, object value)
		{
			Type listType = typeof(List<>).MakeGenericType(interfaceType);
			
			object listInstance;
			if (_dependencies.ContainsKey(listType))
				listInstance = _dependencies[listType];
			else
			{
				listInstance = Activator.CreateInstance(listType);
				_dependencies[listType] = listInstance;
			}

			var addMethod = listType.GetMethod("Add");
			
			addMethod.Invoke(listInstance, new[] { value });

			return listInstance;
		}

		public T Resolve<T>(ContextScope scope = ContextScope.Project)
		{
			if (TryResolve<T>(out T value, scope))
				return value;

			throw new Exception($"Object not found fo the provided type: {typeof(T)}");
		}

		public bool TryResolve<T>(out T value, ContextScope scope = ContextScope.Project)
		{
			Type type = typeof(T);

			if (scope == ContextScope.Local)
			{
				if (_dependencies.TryGetValue(type, out object dependency))
				{
					value = (T) dependency;
					return true;
				}
			}
			else
			{
				foreach (DiContainer searchContainer in _searchContainers)
				{
					if (searchContainer._dependencies.TryGetValue(type, out object dependency))
					{
						value = (T) dependency;
						return true;
					}
				}
			}

			value = default;
			return false;
		}

		// TODO: Add Lazy binding
		public void ResolveBindings(Transform parent)
		{
			foreach (var bindingData in _bindingData) 
				CreateBinding(bindingData, parent);

			foreach ((Type type, object value) in _uniqueObjects)
			{
				MethodInfo[] methods = type.GetMethods();
				foreach (MethodInfo method in methods) 
					InvokeInjectMethods(method, value);
			}
		}

		private object CreateBinding(BindingData bindingData, Transform parent)
		{
			Type type = bindingData.Type;

			// CONSIDER: Now this check enforces that there is only one unique object of a type
			// So we can't pass 2 different BindingDatas with one unique type
			if (_uniqueObjects.ContainsKey(type))
				return _uniqueObjects[type];
				
			object uniqueObject;

			switch (bindingData.BindFrom) {
				case BindFromType.FromNew:
					var constructors = type.GetConstructors();
					if (constructors.Length == 0)
						throw new Exception($"Constructor count is zero on type: '{type}'!");
					var constructorParameters = constructors[0].GetParameters();
					if (constructorParameters.Length == 0)
						uniqueObject = Activator.CreateInstance(type);
					else
					{
						object[] parameterObjects = new object[constructorParameters.Length];
						int index = 0;
						foreach (ParameterInfo parameter in constructorParameters)
						{
							Type parameterType = parameter.ParameterType;

							var parameterObject = GetBindingDependencyParameter(parameterType, parent);
							if (parameterObject == null)
								throw new Exception(
									$"Binding dependency of {type} not found for Parameter type: " + parameterType
								);
							
							parameterObjects[index++] = parameterObject;
						}
						
						uniqueObject = Activator.CreateInstance(type, parameterObjects);
					}
					break;
				case BindFromType.FromInstance:
					uniqueObject = bindingData.Object;
					break;
				case BindFromType.FromComponentInPrefab:
					uniqueObject = Object.Instantiate(bindingData.Object as Object, parent);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			_uniqueObjects[type] = uniqueObject;

			BindingType bindingType = bindingData.BindingType;
			if (bindingType.HasFlag(BindingType.Self)) 
				_dependencies[type] = uniqueObject;
			if (bindingType.HasFlag(BindingType.Interfaces))
				AddInterfaces(uniqueObject);

			return uniqueObject;
		}

		private object GetBindingDependencyParameter(Type parameterType, Transform parent)
		{
			foreach (var searchContainer in _searchContainers) {
				if (searchContainer._uniqueObjects.ContainsKey(parameterType))
					return searchContainer._uniqueObjects[parameterType];

				if (searchContainer == this)
				{
					var bindingDependency = _bindingData.FirstOrDefault(d => d.Type == parameterType);
					if (bindingDependency != null)
					{
						object parameterObject = CreateBinding(bindingDependency, parent);
						return parameterObject;
					}
				}
			}

			return null;
		}

		private void InvokeInjectMethods(MethodInfo method, object value) {
			var customAttributes = method.GetCustomAttributes();
			foreach (Attribute attribute in customAttributes) {
				if (attribute.GetType() != typeof(InjectAttribute))
					continue;

				ParameterInfo[] methodParameters = method.GetParameters();

				var arguments = new object[methodParameters.Length];
				int index = 0;
				foreach (var parameter in methodParameters)
				{
					var parameterAttribute = parameter.GetCustomAttribute<InjectScopeAttribute>();
					ContextScope scope = parameterAttribute?.Scope ?? ContextScope.Project;
					
					object dependency = GetDependencyInContainers(parameter.ParameterType, scope, value);
					
					arguments[index] = dependency;
					
					index++;
				}

				method.Invoke(value, arguments);
			}
		}

		private object GetDependencyInContainers(Type parameterType, ContextScope scope, object value)
		{
			if (scope == ContextScope.Local)
			{
				object dependency = TryGetDependency(this, parameterType);
				if (dependency != null)
					return dependency;

				throw new Exception($"Local container does not have dependency for type: '{parameterType}'. Class: {value.GetType()}");
			}

			foreach (DiContainer container in _searchContainers)
			{
				object dependency = TryGetDependency(container, parameterType);
				if (dependency != null)
					return dependency;
			}

			Debug.Log(_searchContainers.Count);
			throw new Exception($"Dependency not found for type: {parameterType}. Class: {value.GetType()}");
		}

		private object TryGetDependency(DiContainer container, Type parameterType)
		{
			return container._dependencies.TryGetValue(parameterType, out object dependency) ? 
				dependency : 
				null;
		}

		public void AddParentContainer(DiContainer container)
		{
			_parentContainers.Add(container);
			_searchContainers.Add(container);
		}

		public void Log()
		{
			foreach (var (key, value) in _uniqueObjects) {
				Debug.Log(key);
			}
		}
	}
}