using UniRx;

namespace Core.UI.Settings
{
	public sealed class SettingsPresenter : APresenter<SettingsView>
	{
		private readonly SettingsModel _model;
		
		public override string Name => "Settings";

		public SettingsPresenter(SettingsModel model) => 
			_model = model;

		protected override void OnShow()
		{
			base.OnShow();
			_model.SwitchPause(true);
			
			View.ContinueButton.Subscribe(_model.OnContinue);
			View.MenuButton.Subscribe(_model.OnMenu);
			View.RestartButton.Subscribe(_model.OnRestart);
			
			View.SoundToggle.SwitchActive(_model.IsSoundActive());
			View.SoundToggle.Button.Subscribe(ToggleSoundActive);

			View.SensitivitySlider.value = _model.GetSensitivity();
			View.SensitivitySlider.onValueChanged.AddListener(ChangeSensitivity);
		}

		protected override void OnClose()
		{
			base.OnClose();
			_model.SwitchPause(false);
			
			View.ContinueButton.Unsubscribe(_model.OnContinue);
			View.MenuButton.Unsubscribe(_model.OnMenu);
			View.RestartButton.Unsubscribe(_model.OnRestart);
			View.SoundToggle.Button.Unsubscribe(ToggleSoundActive);
			
			View.SensitivitySlider.onValueChanged.RemoveListener(ChangeSensitivity);
		}
		
		private void ToggleSoundActive()
		{
			_model.ToggleSoundActive();
			View.SoundToggle.Toggle();
		}

		private void ChangeSensitivity(float newSensitivity)
		{
			_model.ChangeSensitivity(newSensitivity);
		}
	}
}
