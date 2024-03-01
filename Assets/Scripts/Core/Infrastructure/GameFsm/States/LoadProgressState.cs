using System;
using Core.Integrations;
using Core.Integrations.SaveSystem;
using Utils;

namespace Core.Infrastructure.GameFsm
{
	public class LoadProgressState : IGameState
	{
		private readonly SaveService _saveLoadService;
		private readonly Platform _platform;

		public GameStateType Type => GameStateType.LoadProgress;
		
		public event Action<GameStateType> RequestNextState;

		public LoadProgressState(SaveService saveLoadService, Platform platform)
		{
			JsEventsReceiver.Instance.name = nameof(JsEventsReceiver);
			
			_saveLoadService = saveLoadService;
			_platform = platform;
		}

		public void Enter()
		{
			_saveLoadService.Initialized += OnSaveLoaded;
			_saveLoadService.Initialize();
		}

		public void Update() { }

		public void Exit() { }

		private void OnSaveLoaded() {
			_saveLoadService.Initialized -= OnSaveLoaded;
			RequestNextState?.Invoke(GameStateType.InitializeGlobalProgress);
		}
	}
}