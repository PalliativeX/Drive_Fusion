using Core.Gameplay;
using Core.Sound;
using SimpleInject;

namespace Core.Infrastructure.Installers
{
	public sealed class SceneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<PrefabInstantiateSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PlayersInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameplaySceneInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GeneralSettingsInitializer>().FromNew().AsSingle();

			// InstallSound();
		}
		
		private void InstallSound()
		{
			Container.BindInterfacesAndSelf<AudioSourceCreator>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MusicPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SoundPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InitializeClipSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MainThemePlayInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PlayInitializedClipSystem>().FromNew().AsSingle();
		}
	}
}