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

		private readonly Filter _hasSoundFilter;
		private readonly Filter _currentLevelFilter;
		
		public MenuModel(GlobalWorld globalWorld, GameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;

			var world = globalWorld.World;
			_hasSoundFilter = world.Filter.With<SoundActive>().Build();
			_currentLevelFilter = world.Filter.With<CurrentLevel>().Build();
		}

		public bool IsSoundActive() => 
			_hasSoundFilter.First().GetComponent<SoundActive>().Value;

		public void ToggleSoundActive()
		{
			var entity = _hasSoundFilter.First();
			ref SoundActive soundActive = ref entity.GetComponent<SoundActive>();
			soundActive.Value = !soundActive.Value;
			entity.SetComponent(new Changed());
		}

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