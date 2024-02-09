using Core.SceneManagement.LoadingScreen;
using SimpleInject;
using UnityEngine;

namespace Core.SceneManagement
{
	public sealed class InitializationInstaller : MonoInstaller
	{
		[SerializeField] private LoadingScreenBehaviour _loadingScreen;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<LoadingScreenBehaviour>().FromInstance(_loadingScreen).AsSingle();
			Container.BindInterfacesAndSelf<LoadingScreenBinder>().FromNew().AsSingle();
		}
	}
}