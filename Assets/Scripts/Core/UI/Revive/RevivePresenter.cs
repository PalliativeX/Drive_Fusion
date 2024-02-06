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
		}

		protected override void OnClose()
		{
			base.OnClose();
			_model.SwitchPause(false);
		}
	}
}
