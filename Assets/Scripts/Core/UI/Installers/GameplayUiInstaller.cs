﻿using Core.UI.Menu;
using Core.UI.RateUs;
using Core.UI.Result;
using Core.UI.Revive;
using Core.UI.Settings;
using Core.UI.Systems;
using Core.UI.Tutorial;
using SimpleInject;

namespace Core.UI
{
	public sealed class GameplayUiInstaller : AUiInstaller
	{
		protected override void InstallPanels()
		{
			Container.BindInterfacesAndSelf<MenuPresenter>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MenuModel>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<GamePresenter>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<GameModel>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<SettingsModel>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<SettingsPresenter>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<ReviveModel>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<RevivePresenter>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<ResultModel>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<ResultPresenter>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<RateUsModel>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<RateUsPresenter>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<TutorialPresenter>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<TutorialModel>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<PanelByStateInitializer>().FromNew().AsSingle();
		}
	}
}