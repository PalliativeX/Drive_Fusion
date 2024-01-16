using Core.CameraLogic;
using Core.ECS;
using Core.Gameplay;
using SimpleInject;

namespace Core.Infrastructure.Installers
{
	public sealed class SceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<PrefabInstantiateSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameplaySceneInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GeneralSettingsInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SetCameraFollowSystem>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<DestroyEntitySystem>().FromNew().AsSingle();
		}
	}
}