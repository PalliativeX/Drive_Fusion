namespace Core.UI.Revive
{
	public sealed class RevivePresenter : APresenter<ReviveView>
	{
		private readonly ReviveModel _model;
		
		public override string Name => "Revive";

		public RevivePresenter(ReviveModel model)
		{
			_model = model;
		}

		protected override void OnShow()
		{
			base.OnShow();
			_model.SwitchPause(true);
			
			View.ReviveButton.Subscribe(_model.OnRevive);
			View.RefuseButton.Subscribe(_model.OnRefuse);
		}

		protected override void OnClose()
		{
			base.OnClose();
			_model.SwitchPause(false);
			
			View.ReviveButton.Unsubscribe(_model.OnRevive);
			View.RefuseButton.Unsubscribe(_model.OnRefuse);
		}
	}
}
