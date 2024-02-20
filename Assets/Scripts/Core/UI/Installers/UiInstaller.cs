using Core.UI.Systems;
using SimpleInject;
using UnityEngine;

namespace Core.UI
{
	public abstract class AUiInstaller : MonoInstaller
	{
		[SerializeField] private UiParent _uiParent;
		
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelf<UiParent>().FromInstance(_uiParent).AsSingle();
			Container.BindInterfacesAndSelf<PanelFactory>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PanelInitializer>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PanelController>().FromNew().AsSingle();
			Container.BindInterfacesAndSelf<PanelsDisposeHandler>().FromNew().AsSingle();
			
			InstallPanels();
		}

		protected abstract void InstallPanels();
	}
}