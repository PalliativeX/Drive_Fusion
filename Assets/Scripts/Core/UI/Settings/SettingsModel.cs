using Core.Gameplay;
using Core.Levels;
using UnityEngine;

namespace Core.UI.Settings
{
	public sealed class SettingsModel
	{
		private readonly PanelController _panelController;
		private readonly LevelsHelper _levelsHelper;
		private readonly GamePauser _pauser;

		public SettingsModel(PanelController panelController, LevelsHelper levelsHelper, GamePauser pauser)
		{
			_panelController = panelController;
			_levelsHelper = levelsHelper;
			_pauser = pauser;
		}

		public void SwitchPause(bool paused) => 
			_pauser.SwitchPause(paused);

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
