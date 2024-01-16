using System;
using System.Threading.Tasks;
using Core.Levels;

namespace Core.Infrastructure.GameFsm
{
	public class InitializeGlobalProgressState : IGameState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly LevelsHelper _levelsHelper;
		// private readonly List<ILoadable> _loadable;
		// private readonly SaveLoadService _saveLoadService;

		public GameStateType Type => GameStateType.InitializeGlobalProgress;

		public event Action<GameStateType> RequestNextState;

		public InitializeGlobalProgressState(
			GameStateMachine stateMachine,
			// List<ILoadable> loadable,
			// SaveLoadService saveLoadService,
			LevelsHelper levelsHelper
		)
		{
			_stateMachine = stateMachine;
			_levelsHelper = levelsHelper;
			// _loadable = loadable;
			// _saveLoadService = saveLoadService;
		}

		public async void Enter()
		{
			// _saveLoadService.RestoreState(_loadable);

			int currentLevel = 1;
			
			var entity = _levelsHelper.Initialize(currentLevel, 0);

			await Task.Yield();

			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}

		public void Update() { }

		public void Exit() { }
	}
}