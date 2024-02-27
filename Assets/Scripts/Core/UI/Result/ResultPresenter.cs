using UniRx;

namespace Core.UI.Result
{
	public sealed class ResultPresenter : APresenter<ResultView>
	{
		private readonly ResultModel _model;
		
		public override string Name => "Result";

		public ResultPresenter(ResultModel model) => _model = model;

		protected override void OnShow()
		{
			_model.SwitchPause(true);
			_model.OnShow();

			View.MenuButton.OnClickSubscribeDisposable(_model.OnMenu).AddTo(Disposable);
			View.RestartButton.OnClickSubscribeDisposable(_model.OnRestart).AddTo(Disposable);
		}

		protected override void OnClose()
		{
			_model.SwitchPause(false);
			_model.OnClose();
		}
	}
}
