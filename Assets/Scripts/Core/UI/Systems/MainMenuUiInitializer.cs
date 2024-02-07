using Core.UI.MainMenu;
using SimpleInject;

namespace Core.UI.Systems
{
	public class MainMenuUiInitializer : IInitializable
	{
		private readonly PanelController _panelController;
		
		public MainMenuUiInitializer(PanelController panelController) => 
			_panelController = panelController;

		public void Initialize() => 
			_panelController.Open<MainMenuPresenter>();
	}
}