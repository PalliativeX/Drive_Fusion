using Core.Levels;
using UnityEngine;

namespace Core.UI.Settings
{
	public sealed class SettingsModel
	{
		private readonly PanelController _panelController;
		private readonly LevelsHelper _levelsHelper;

		public SettingsModel(PanelController panelController, LevelsHelper levelsHelper)
		{
			_panelController = panelController;
			_levelsHelper = levelsHelper;
		}

		public void SwitchPause(bool paused) => 
			Time.timeScale = paused ? 0f : 1f;

		public void OnContinue() => 
			_panelController.Close<SettingsPresenter>();

		public void OnMenu()
		{
			SwitchPause(false);
			_levelsHelper.LoadMenu();
		}

		public void OnRestart()
		{
			SwitchPause(false);
			_levelsHelper.RestartLevel();
		}
	}
}
