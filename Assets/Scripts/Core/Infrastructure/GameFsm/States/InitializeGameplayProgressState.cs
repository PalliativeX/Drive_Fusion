using System;

namespace Core.Infrastructure.GameFsm
{
	public class InitializeGameplayProgressState : IGameState
	{
		// private readonly SaveLoadService _saveLoadService;

		public GameStateType Type => GameStateType.InitializeGameplayProgress;
		
		public event Action<GameStateType> RequestNextState;

		// public InitializeGameplayProgressState(SaveLoadService saveLoadService)
		// {
			// _saveLoadService = saveLoadService;
		// }

		public void Enter()
		{
			// _saveLoadService.RestoreState(_saveable);
			
			RequestNextState?.Invoke(GameStateType.Menu);
		}

		public void Update() { }

		public void Exit()
		{
			
		}
	}
}