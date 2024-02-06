using UnityEngine;

namespace Core.UI.Settings
{
	public sealed class SettingsModel
	{
		private readonly PanelController _panelController;
		
		public SettingsModel(PanelController panelController)
		{
			_panelController = panelController;
		}

		public void SwitchPause(bool paused) => 
			Time.timeScale = paused ? 0f : 1f;

		public void OnContinue() => 
			_panelController.Close<SettingsPresenter>();
	}
}
