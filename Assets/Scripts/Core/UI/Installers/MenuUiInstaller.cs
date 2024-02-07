using Core.UI.MainMenu;
using Core.UI.Systems;
using SimpleInject;

namespace Core.UI
{
	public sealed class MenuUiInstaller : AUiInstaller
	{
		protected override void InstallPanels()
		{
			Container.BindInterfacesAndSelf<MainMenuPresenter>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MainMenuModel>().FromNew().AsSingle();
			
			Container.BindInterfacesAndSelf<MainMenuUiInitializer>().FromNew().AsSingle();
		}
	}
}