using Core.AssetManagement;
using Core.Currency;
using Core.ECS;
using Core.Gameplay;
using Core.Infrastructure.GameFsm;
using Core.Integrations;
using Core.Integrations.Ads;
using Core.Integrations.SaveSystem;
using Core.Levels;
using Core.Localization;
using Core.Menu;
using Core.SceneManagement;
using Core.SceneManagement.LoadingScreen;
using Core.Sound;
using Core.Tutorial;
using Scellecs.Morpeh;
using SimpleInject;
using UnityEngine;
using Utils;

namespace Core.Infrastructure.Installers
{
	public sealed class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private PoolContainer _poolContainer;
		[SerializeField] private JsEventsReceiver _jsEventsReceiver;
		[SerializeField] private LocalizationService _localizationService;
		[SerializeField] private Platform _platform;
		
		public override void InstallBindings()
		{
			Container.BindSelf<World>().FromInstance(World.Default).AsSingle();
			Container.BindSelf<GlobalWorld>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<Bootstrap>().FromNew().AsSingle();

			Container.BindInterfacesAndSelf<AssetPool>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<AssetProvider>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PoolContainer>().FromComponentInNewPrefab(_poolContainer).AsSingle();
			Container.BindInterfacesAndSelf<JsEventsReceiver>().FromComponentInNewPrefab(_jsEventsReceiver).AsSingle();
			Container.BindInterfacesAndSelf<LocalizationService>().FromComponentInNewPrefab(_localizationService).AsSingle();
			Container.BindInterfacesAndSelf<SceneLoader>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameParentProvider>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<LoadingScreenProvider>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<GeneralSettingsService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<Platform>().FromComponentInNewPrefab(_platform).AsSingle();

			Container.BindInterfacesAndSelf<MoneyManager>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<LeaderboardService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameplayParametersProvider>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<TutorialService>().FromNew().AsSingle();

			InstallIntegrations();
			InstallSaves();
			
			InstallSound();

			InstallGameStateMachine();

			InstallLevels();
			InstallVehicles();
		}

		private void InstallSound()
		{
			Container.BindInterfacesAndSelf<SoundService>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<AudioSourceCreator>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MusicPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SoundPlayer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<InitializeClipSystem>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PlayInitializedClipSystem>().FromNew().AsSingle();
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

		private void InstallVehicles()
		{
			Container.BindInterfacesAndSelf<VehiclesInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<VehicleSelectionService>().FromNew().AsSingle();
		}

		private void InstallIntegrations()
		{
			Container.BindInterfacesAndSelf<AdsService>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<RateUsShowController>().FromNew().AsSingle();
		}

		private void InstallSaves()
		{
			Container.BindInterfacesAndSelf<SaveService>().FromNew().AsSingle();
		}
	}
}