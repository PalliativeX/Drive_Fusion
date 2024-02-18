using Core.ECS;
using Core.Infrastructure.GameFsm;
using Core.Levels;
using Core.Sound;
using Scellecs.Morpeh;

namespace Core.UI.Menu
{
	public class MenuModel
	{
		private readonly IGameStateMachine _stateMachine;
		private readonly SoundService _sound;

		private readonly Filter _hasSoundFilter;
		private readonly Filter _currentLevelFilter;
		
		public MenuModel(GlobalWorld globalWorld, GameStateMachine stateMachine, SoundService sound)
		{
			_stateMachine = stateMachine;
			_sound = sound;

			var world = globalWorld.World;
			_currentLevelFilter = world.Filter.With<CurrentLevel>().Build();
		}

		public bool IsSoundActive() => _sound.IsSoundActive;

		public void ToggleSoundActive() => _sound.ToggleActive();

		public int GetCurrentLevel()
		{
			var entity = _currentLevelFilter.First();
			return entity.GetComponent<CurrentLevel>().Value;
		}

		public void StartPlaying()
		{
			_stateMachine.ChangeState(GameStateType.Gameplay);
		}
	}
}