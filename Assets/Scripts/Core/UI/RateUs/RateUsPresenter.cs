using UniRx;

namespace Core.UI.RateUs
{
	public sealed class RateUsPresenter : APresenter<RateUsView>
	{
		private readonly RateUsModel _model;
		
		public override string Name => "RateUs";

		public RateUsPresenter(RateUsModel model)
		{
			_model = model;
		}

		protected override void OnShow()
		{
			View.CloseButton.OnClickSubscribeDisposable(_model.OnClose).AddTo(Disposable);
			View.RateButton.OnClickSubscribeDisposable(_model.OnRate).AddTo(Disposable);
			
			_model.OnShow();
		}

		protected override void OnClose()
		{
			base.OnClose();
		}
	}
}
