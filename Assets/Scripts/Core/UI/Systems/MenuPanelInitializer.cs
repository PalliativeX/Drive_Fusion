using Core.UI.Menu;
using SimpleInject;

namespace Core.UI.Systems
{
	public class MenuPanelInitializer : IInitializable
	{
		private readonly PanelController _panelController;
		
		public MenuPanelInitializer(PanelController panelController)
		{
			_panelController = panelController;
		}

		public void Initialize()
		{
			_panelController.Open<MenuPresenter>();
		}
	}
}