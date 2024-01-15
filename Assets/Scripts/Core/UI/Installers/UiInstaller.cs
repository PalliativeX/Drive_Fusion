using Core.UI.Menu;
using Core.UI.Systems;
using SimpleInject;
using UnityEngine;

namespace Core.UI
{
	public sealed class UiInstaller : MonoInstaller
	{
		[SerializeField] private UiParent _uiParent;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<UiParent>().FromInstance(_uiParent).AsSingle();
			Container.BindInterfacesAndSelf<PanelFactory>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PanelInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PanelController>().FromNew().AsSingle();
			
			InstallPanels();
			
			Container.BindInterfacesAndSelf<MenuPanelInitializer>().FromNew().AsSingle();
		}

		private void InstallPanels()
		{
			Container.BindInterfacesAndSelf<MenuPresenter>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<MenuModel>().FromNew().AsSingle();
		}
	}
}