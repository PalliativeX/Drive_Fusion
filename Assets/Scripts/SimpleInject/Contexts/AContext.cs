using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleInject
{
	public abstract class AContext : MonoBehaviour
	{
		[SerializeField] private bool _autoRun = true;
		[SerializeField] private List<MonoInstaller> _monoInstallers;
		[SerializeField] private List<ScriptableInstaller> _scriptableInstallers;
		[SerializeField] private bool _parentUnderSelf;

		private List<IInstaller> _installers;

		private ContextHierarchy _contextHierarchy;

		private DiContainer _container;

		public abstract ContextScope Scope { get; }

		public bool Initialized { get; private set; }

		public DiContainer Container => _container;

		protected virtual void Awake()
		{
			_contextHierarchy = ContextHierarchy.Instance;
			_contextHierarchy.AddContext(this);

			if (_autoRun)
				Initialize();
		}

		protected virtual void Update()
		{
			if (!Initialized)
				return;

			if (!_container.TryResolve<List<ITickable>>(out var tickables, ContextScope.Local))
				return;
			
			foreach (var tickable in tickables)
				tickable.Tick();
		}

		public void Initialize()
		{
			if (Initialized)
				throw new Exception("Trying to Initialize Context again!");

			_container = new DiContainer();
			_container.BindSelf<DiContainer>().FromInstance(_container);

			SetUpInstallers();

			ResolveBindings();

			InvokeInitializable();

			Initialized = true;
		}

		private void SetUpInstallers()
		{
			_installers = new List<IInstaller>();
			_installers.AddRange(_monoInstallers);
			_installers.AddRange(_scriptableInstallers);

			foreach (IInstaller installer in _installers)
			{
				installer.Initialize(_container);
				installer.InstallBindings();
			}
		}

		private void ResolveBindings()
		{
			AContext parentContext = _contextHierarchy.GetParentContext(Scope);

			if (parentContext != null)
				_container.AddParentContainer(parentContext.Container);
			
			_container.ResolveBindings(_parentUnderSelf ? transform : null);
		}

		private void InvokeInitializable()
		{
			if (!_container.TryResolve<List<IInitializable>>(out var initializables, ContextScope.Local))
				return;
			
			foreach (var initializable in initializables)
				initializable.Initialize();
		}

		private void OnDestroy()
		{
			if (!_container.TryResolve<List<IDisposable>>(out var disposables, ContextScope.Local))
				return;
			
			foreach (var disposable in disposables)
				disposable.Dispose();
		}
	}
}