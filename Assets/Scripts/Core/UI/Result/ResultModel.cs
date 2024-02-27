using Core.Gameplay;
using Core.Levels;
using Core.UI.RateUs;
using Core.UI.Settings;

namespace Core.UI.Result
{
	public sealed class ResultModel
	{
		private readonly LevelsHelper _levelsHelper;
		private readonly GamePauser _pauser;
		private readonly CurrentLevelService _currentLevelService;
		private readonly RateUsModel _rateUs;

		public ResultModel(LevelsHelper levelsHelper, GamePauser pauser, CurrentLevelService currentLevelService, RateUsModel rateUs)
		{
			_levelsHelper = levelsHelper;
			_pauser = pauser;
			_currentLevelService = currentLevelService;
			_rateUs = rateUs;
		}

		public void OnShow()
		{
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

		public void OnClose() => _currentLevelService.TryUpdateRecord();
	}
}
