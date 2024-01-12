using System;

namespace SimpleInject
{
	public sealed class BindingData : BaseBindingData
	{
		public BindFromType BindFrom;
		public object Object;

		public BindingData(BindingType bindingType, Type type) : base(bindingType, type)
		{
			BindFrom = BindFromType.FromNew;
			Object = null;
		}
	}

	public class BaseBindingData
	{
		public readonly BindingType BindingType;
		public readonly Type Type;
		
		public BindingScope Scope;

		protected BaseBindingData(BindingType bindingType, Type type)
		{
			BindingType = bindingType;
			Type = type;
			
			Scope = BindingScope.Single;
		}
	}

	[Flags]
	public enum BindingType
	{
		Self = 0,
		Interfaces = 1,
	}
	
	public enum BindFromType
	{
		FromNew = 0,
		FromInstance = 1,
		FromComponentInPrefab = 2,
	}

	// NOTE: Now only Single is implemented
	public enum BindingScope
	{
		Single = 0, 
		Transient = 1,
	}
}