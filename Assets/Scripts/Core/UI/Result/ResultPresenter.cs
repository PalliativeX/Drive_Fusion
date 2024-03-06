using UniRx;

namespace Core.UI.Result
{
	public sealed class ResultPresenter : APresenter<ResultView>
	{
		private readonly ResultModel _model;

		private int _earnedMoney;
		private int _moneyIncrease;
		
		public override string Name => "Result";

		public ResultPresenter(ResultModel model) => _model = model;

		protected override void OnShow()
		{
			_model.IncreasedReward += OnIncreasedReward;

			View.MenuButton.OnClickSubscribeDisposable(_model.OnMenu).AddTo(Disposable);
			View.RestartButton.OnClickSubscribeDisposable(_model.OnRestart).AddTo(Disposable);
			View.IncreaseRewardAds.OnClickSubscribeDisposable(_model.OnIncreaseReward).AddTo(Disposable);
			
			_model.SwitchPause(true);
			_model.OnShow();
			
			View.IncreaseRewardAds.interactable = true;

			(float currentScore, float previousRecord) = _model.GetScores();
			View.ScoreText.SetText(currentScore.ToString("F0"));
			View.PreviousRecordText.SetText(previousRecord.ToString("F0"));

			(int earnedMoney, int moneyIncrease) = _model.CalculateEarnings();

			_moneyIncrease = moneyIncrease;
			_earnedMoney = earnedMoney;
			
			View.EarnedMoneyText.SetText(earnedMoney.ToString());
			View.IncreaseMoneyText.SetText($"+{moneyIncrease}");
		}

		protected override void OnClose()
		{
			_model.IncreasedReward -= OnIncreasedReward;
			
			_model.SwitchPause(false);
			_model.OnClose();

			_earnedMoney = _moneyIncrease = 0;
		}

		private void OnIncreasedReward()
		{
			View.IncreaseRewardAds.interactable = false;
			
			View.EarnedMoneyText.SetText((_earnedMoney + _moneyIncrease).ToString());
		}
	}
}
