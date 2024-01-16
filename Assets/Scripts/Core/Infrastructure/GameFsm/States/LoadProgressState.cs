using System;

namespace Core.Infrastructure.GameFsm
{
	public class LoadProgressState : IGameState
	{
		// private readonly SaveLoadService _saveLoadService;
		
		public GameStateType Type => GameStateType.LoadProgress;
		
		public event Action<GameStateType> RequestNextState;

		// public LoadProgressState(SaveLoadService saveLoadService)
		// {
			// _saveLoadService = saveLoadService;
		// }

		public void Enter()
		{
			// _saveLoadService.Initialize();
			
			RequestNextState?.Invoke(GameStateType.InitializeGlobalProgress);
		}

		public void Update() { }

		public void Exit() { }
	}
}