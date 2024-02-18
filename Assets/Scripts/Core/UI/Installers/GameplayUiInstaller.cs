using Core.UI.Menu;
using Core.UI.Revive;
using Core.UI.Settings;
using Core.UI.Systems;
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
			
			Container.BindInterfacesAndSelf<PanelByStateInitializer>().FromNew().AsSingle();
		}
	}
}