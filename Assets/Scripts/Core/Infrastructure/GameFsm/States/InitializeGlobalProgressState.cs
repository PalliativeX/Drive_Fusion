using System;
using System.Threading.Tasks;
using Core.Levels;
using Scellecs.Morpeh;

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
			
			var entity = _levelsHelper.Initialize();

			await Task.Yield();
			
			entity.SetComponent(new RequestMenuLoad());
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);
		}

		public void Update() { }

		public void Exit() { }
	}
}