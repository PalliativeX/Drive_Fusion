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
			// View.MenuButton.Subscribe();
			// View.RestartButton.Subscribe();
		}

		protected override void OnClose()
		{
			base.OnClose();
			_model.SwitchPause(false);
			
			View.ContinueButton.Unsubscribe(_model.OnContinue);
			// View.MenuButton.Unsubscribe();
			// View.RestartButton.Unsubscribe();
		}
	}
}
