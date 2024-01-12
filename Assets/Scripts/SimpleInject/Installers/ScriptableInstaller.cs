using UnityEngine;

namespace SimpleInject
{
	public abstract class ScriptableInstaller : ScriptableObject, IInstaller
	{
		protected DiContainer Container;

		public void Initialize(DiContainer container)
		{
			Container = container;
		}

		public abstract void InstallBindings();
	}
}