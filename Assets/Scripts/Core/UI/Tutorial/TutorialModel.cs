using Core.Tutorial;

namespace Core.UI.Tutorial
{
	public sealed class TutorialModel
	{
		private readonly GeneralSettingsService _generalSettings;
		private readonly PanelController _panelController;
		private readonly TutorialService _tutorial;

		public TutorialModel(GeneralSettingsService generalSettings, PanelController panelController, TutorialService tutorial)
		{
			_generalSettings = generalSettings;
			_panelController = panelController;
			_tutorial = tutorial;
		}

		public bool IsMobileDevice() => _generalSettings.IsMobileDevice;

		public void OnTutorialComplete()
		{
			_tutorial.SetTutorialComplete();
			_panelController.Close<TutorialPresenter>();
		}
	}
}
