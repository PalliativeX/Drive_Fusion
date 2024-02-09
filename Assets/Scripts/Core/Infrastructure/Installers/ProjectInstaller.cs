using Core.AssetManagement;
using Core.Currency;
using Core.ECS;
using Core.Gameplay;
using Core.Infrastructure.GameFsm;
using Core.Levels;
using Core.SceneManagement;
using Core.SceneManagement.LoadingScreen;
using Core.Sound;
using Scellecs.Morpeh;
using SimpleInject;
using UnityEngine;

namespace Core.Infrastructure.Installers
{
	public sealed class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private PoolContainer _poolContainer;
		
		public override void InstallBindings()
		{
			Container.BindSelf<World>().FromInstance(World.Default).AsSingle();
			Container.BindSelf<GlobalWorld>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<AssetPool>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<AssetProvider>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PoolContainer>().FromComponentInNewPrefab(_poolContainer).AsSingle();
			Container.BindInterfacesAndSelf<SceneLoader>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameParentProvider>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<LoadingScreenProvider>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<GeneralSettingsInitializer>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<MoneyManager>().FromNew().AsSingle();
			
			InstallSound();

			InstallGameStateMachine();

			InstallLevels();
		}

		private void InstallSound()
		{
			Container.BindInterfacesAndSelf<AudioSourceCreator>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MusicPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SoundPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InitializeClipSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SoundActiveInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PlayInitializedClipSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SoundActiveChangedSystem>().FromNew().AsSingle();
		}

		private void InstallGameStateMachine()
		{
			Container.BindInterfacesAndSelf<GameStateMachine>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<LoadProgressState>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InitializeGlobalProgressState>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<LoadLevelState>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InitializeGameplayProgressState>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MenuState>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameplayState>().FromNew().AsSingle();
		}

		private void InstallLevels()
		{
			Container.BindInterfacesAndSelf<LevelsHelper>().FromNew().AsSingle();
		}
	}
}