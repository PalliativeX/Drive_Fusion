using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Integrations.SaveSystem;
using Core.Levels;
using Core.Tutorial;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.Infrastructure.GameFsm
{
	public class InitializeGlobalProgressState : IGameState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly LevelsHelper _levelsHelper;
		private readonly TutorialService _tutorial;
		private readonly SaveService _saveLoadService;

		public GameStateType Type => GameStateType.InitializeGlobalProgress;

		public event Action<GameStateType> RequestNextState;

		public InitializeGlobalProgressState(
			GameStateMachine stateMachine,
			SaveService saveLoadService,
			LevelsHelper levelsHelper,
			TutorialService tutorial
		)
		{
			_stateMachine = stateMachine;
			_levelsHelper = levelsHelper;
			_tutorial = tutorial;
			_saveLoadService = saveLoadService;
		}

		public void Enter() => Initialize();

		private async UniTaskVoid Initialize()
		{
			_saveLoadService.InitializeLoadables();
			
			var entity = _levelsHelper.Load(_saveLoadService.SaveData);

			await UniTask.Yield();
			
			if (!_tutorial.HasTutorial())
				entity.SetComponent(new RequestMenuLoad());
			else
				entity.SetComponent(new CurrentLevel { Value = 1 });
			
			_stateMachine.ChangeState(GameStateType.LoadLevel, entity);	
		}

		public void Update() { }

		public void Exit() { }
	}
}