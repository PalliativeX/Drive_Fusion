using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Integrations.SaveSystem;
using Core.Levels;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.Infrastructure.GameFsm
{
	public class InitializeGlobalProgressState : IGameState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly LevelsHelper _levelsHelper;
		private readonly SaveService _saveLoadService;

		public GameStateType Type => GameStateType.InitializeGlobalProgress;

		public event Action<GameStateType> RequestNextState;

		public InitializeGlobalProgressState(
			GameStateMachine stateMachine,
			SaveService saveLoadService,
			LevelsHelper levelsHelper
		)
		{
			_stateMachine = stateMachine;
			_levelsHelper = levelsHelper;
			_saveLoadService = saveLoadService;
		}

		public void Enter() => Initialize();

		private async UniTaskVoid Initialize()
		{
			_saveLoadService.InitializeLoadables();
			
			var entity = _levelsHelper.Load(_saveLoadService.SaveData);

			await UniTask.Yield();
			
			entity.SetComponent(new RequestMenuLoad());
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);	
		}

		public void Update() { }

		public void Exit() { }
	}
}