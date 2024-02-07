namespace Core.UI.Settings
{
	public sealed class SettingsPresenter : APresenter<SettingsView>
	{
		private readonly SettingsModel _model;
		
		public override string Name => "Settings";

		public SettingsPresenter(SettingsModel model)
		{
			_model = model;
		}

		protected override void OnShow()
		{
			base.OnShow();
			_model.SwitchPause(true);
			
			View.ContinueButton.Subscribe(_model.OnContinue);
			View.MenuButton.Subscribe(_model.OnMenu);
			View.RestartButton.Subscribe(_model.OnRestart);
		}

		protected override void OnClose()
		{
			base.OnClose();
			_model.SwitchPause(false);
			
			View.ContinueButton.Unsubscribe(_model.OnContinue);
			View.MenuButton.Unsubscribe(_model.OnMenu);
			View.RestartButton.Unsubscribe(_model.OnRestart);
		}
	}
}
