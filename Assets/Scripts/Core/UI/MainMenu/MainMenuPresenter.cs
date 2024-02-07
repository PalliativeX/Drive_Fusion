namespace Core.UI.MainMenu
{
	public sealed class MainMenuPresenter : APresenter<MainMenuView>
	{
		private readonly MainMenuModel _model;
		
		public override string Name => "MainMenu";

		public MainMenuPresenter(MainMenuModel model) => 
			_model = model;

		protected override void OnShow()
		{
			base.OnShow();
			
			View.PlayButton.Subscribe(_model.OnPlay);
		}

		protected override void OnClose()
		{
			base.OnClose();
		}
	}
}
