using UnityEngine;

namespace SimpleInject
{
	public static class BindingDataModifiers
	{
		public static BaseBindingData FromNew(this BindingData data)
		{
			data.BindFrom = BindFromType.FromNew;
			return data;
		}
		
		public static BaseBindingData FromInstance(this BindingData data, object instance)
		{
			data.BindFrom = BindFromType.FromInstance;
			data.Object = instance;
			return data;
		}
		
		public static BaseBindingData FromComponentInNewPrefab<T>(this BindingData data, T prefab) where T : Object
		{
			data.BindFrom = BindFromType.FromComponentInPrefab;
			data.Object = prefab;
			return data;
		}
		
		public static BaseBindingData AsSingle(this BaseBindingData data)
		{
			data.Scope = BindingScope.Single;
			return data;
		}
		
		public static BaseBindingData AsTransient(this BaseBindingData data)
		{
			data.Scope = BindingScope.Transient;
			return data;
		}
	}
}