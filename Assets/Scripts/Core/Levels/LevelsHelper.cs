using Core.Infrastructure.GameFsm;
using Scellecs.Morpeh;

namespace Core.Levels
{
	public class LevelsHelper
	{
		private readonly World _world;
		private readonly IGameStateMachine _stateMachine;

		private readonly Filter _filter;

		public LevelsHelper(World world, GameStateMachine stateMachine)
		{
			_world = world;
			_stateMachine = stateMachine;

			_filter = world.Filter.With<CurrentLevel>().Build();
		}

		public Entity Initialize(int currentLevel, int levelsPassed)
		{
			var entity = _world.CreateEntity();
			entity.SetComponent(new CurrentLevel { Value = currentLevel });
			entity.SetComponent(new LevelsPassed { Value = levelsPassed });
			return entity;
		}

		public void NextLevel()
		{
			var entity = _filter.First();
			ref var currentLevel = ref entity.GetComponent<CurrentLevel>();
			currentLevel.Value++;

			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}

		public void RestartLevel()
		{
			var entity = _filter.First();
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}
	}
}