using System;

namespace Core.Infrastructure.GameFsm
{
	public class GameplayState : IGameState
	{
		// private readonly LoadingScreenProvider _loadingScreenProvider;
		
		public GameStateType Type => GameStateType.Gameplay;
		
		public event Action<GameStateType> RequestNextState;

		// public GameplayState(LoadingScreenProvider loadingScreenProvider) => 
			// _loadingScreenProvider = loadingScreenProvider;

		public void Enter()
		{
			// await Task.Delay(600);
			// _loadingScreenProvider.LoadingScreen.Hide();
		}

		public void Update() { }
		public void Exit() { }
	}
}