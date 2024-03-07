using Core.Gameplay;
using Core.Levels;
using Core.Sound;
using UnityEngine;

namespace Core.UI.Settings
{
	public sealed class SettingsModel
	{
		private readonly PanelController _panelController;
		private readonly LevelsHelper _levelsHelper;
		private readonly GamePauser _pauser;
		private readonly SoundService _sound;
		private readonly GameplayParametersProvider _gameplayParameters;

		public SettingsModel(
			PanelController panelController,
			LevelsHelper levelsHelper,
			GamePauser pauser,
			SoundService sound,
			GameplayParametersProvider gameplayParameters
		)
		{
			_panelController = panelController;
			_levelsHelper = levelsHelper;
			_pauser = pauser;
			_sound = sound;
			_gameplayParameters = gameplayParameters;
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

		public bool IsSoundActive() => _sound.IsSoundActive;
		public void ToggleSoundActive() => _sound.ToggleActive();

		public float GetSensitivity() => _gameplayParameters.Sensitivity;

		public void ChangeSensitivity(float newSensitivity) => 
			_gameplayParameters.SetSensitivity(newSensitivity);
	}
}