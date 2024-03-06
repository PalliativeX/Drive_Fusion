using System;
using Core.Currency;
using Core.Gameplay;
using Core.Integrations.Ads;
using Core.Levels;
using Core.UI.RateUs;
using Utils;
using Random = UnityEngine.Random;

namespace Core.UI.Result
{
	public sealed class ResultModel
	{
		private readonly LevelsHelper _levelsHelper;
		private readonly GamePauser _pauser;
		private readonly CurrentLevelService _currentLevelService;
		private readonly RateUsModel _rateUs;
		private readonly AdsService _ads;
		private readonly CurrencyConfig _currencyConfig;
		
		public int EarnedMoney { get; private set; }
		public int MoneyIncrease { get; private set; }
		public bool HasIncreasedReward { get; private set; }
		
		public event Action IncreasedReward;

		public ResultModel(
			LevelsHelper levelsHelper,
			GamePauser pauser,
			CurrentLevelService currentLevelService,
			RateUsModel rateUs,
			AdsService ads,
			CurrencyConfig currencyConfig
		)
		{
			_levelsHelper = levelsHelper;
			_pauser = pauser;
			_currentLevelService = currentLevelService;
			_rateUs = rateUs;
			_ads = ads;
			_currencyConfig = currencyConfig;
		}

		public void OnShow()
		{
			HasIncreasedReward = false;
			EarnedMoney = MoneyIncrease = 0;
			
			if (_rateUs.ShouldShowRateWindow())
				_rateUs.Show();
		}

		public void SwitchPause(bool paused) =>
			_pauser.SwitchPause(paused);

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

		public void OnIncreaseReward()
		{
			_ads.ShowRewardedAd(OnRewarded);
		}

		public (float currentScore, float previousRecord) GetScores() =>
			(_currentLevelService.Score, _currentLevelService.CurrentScoreRecord);

		public (int earnedMoney, int moneyIncrease) CalculateEarnings()
		{
			EarnedMoney = _currentLevelService.EarnedMoney + 
			              (_currentLevelService.Score * _currencyConfig.ScoreMoneyCoefficient * Random.Range(0.95f, 1.05f)).ToInt();
			MoneyIncrease = (EarnedMoney * Random.Range(0.85f, 1.35f)).ToInt();

			return (EarnedMoney, MoneyIncrease);
		}

		public void OnClose()
		{
			_currentLevelService.TryUpdateRecord();
			_currentLevelService.AddTotalEarnings(EarnedMoney + (HasIncreasedReward ? MoneyIncrease : 0));
		}

		private void OnRewarded(bool isSuccess)
		{
			if (!isSuccess)
				return;
			
			HasIncreasedReward = true;
			IncreasedReward?.Invoke();
		}
	}
}