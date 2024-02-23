using Core.Gameplay;
using Core.Levels;
using Core.UI.Settings;

namespace Core.UI.Result
{
	public sealed class ResultModel
	{
		private readonly LevelsHelper _levelsHelper;
		private readonly GamePauser _pauser;

		public ResultModel(LevelsHelper levelsHelper, GamePauser pauser)
		{
			_levelsHelper = levelsHelper;
			_pauser = pauser;
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
	}
}
