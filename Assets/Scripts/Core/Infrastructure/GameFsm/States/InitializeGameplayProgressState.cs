using System;
using System.Threading.Tasks;
using Core.SceneManagement.LoadingScreen;

namespace Core.Infrastructure.GameFsm
{
	public class InitializeGameplayProgressState : IGameState
	{
		private readonly LoadingScreenProvider _loadingScreenProvider;
		// private readonly SaveLoadService _saveLoadService;

		public GameStateType Type => GameStateType.InitializeGameplayProgress;
		
		public event Action<GameStateType> RequestNextState;

		public InitializeGameplayProgressState(LoadingScreenProvider loadingScreenProvider)
		{
			_loadingScreenProvider = loadingScreenProvider;
		}

		// public InitializeGameplayProgressState(SaveLoadService saveLoadService)
		// {
			// _saveLoadService = saveLoadService;
		// }

		public async void Enter()
		{
			// _saveLoadService.RestoreState(_saveable);

			await Task.Delay(500);
			_loadingScreenProvider.LoadingScreen.Hide();
			
			RequestNextState?.Invoke(GameStateType.Menu);
		}

		public void Update() { }

		public void Exit()
		{
			
		}
	}
}