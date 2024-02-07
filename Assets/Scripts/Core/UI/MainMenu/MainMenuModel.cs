using Core.Levels;

namespace Core.UI.MainMenu
{
	public sealed class MainMenuModel
	{
		private readonly LevelsHelper _levelsHelper;
		
		public MainMenuModel(LevelsHelper levelsHelper) => 
			_levelsHelper = levelsHelper;

		public void OnPlay() => _levelsHelper.Play(1);
	}
}
