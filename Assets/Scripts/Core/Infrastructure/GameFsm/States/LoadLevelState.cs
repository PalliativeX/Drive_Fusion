using System;
using Core.Levels;
using Core.Levels.Storages;
using Core.SceneManagement;
using Scellecs.Morpeh;
using UnityEngine.SceneManagement;

namespace Core.Infrastructure.GameFsm
{
	public class LoadLevelState : IParameterizedGameState<Entity>
	{
		private readonly ISceneLoaderService _sceneLoader;
		private readonly LevelsStorage _levels;

		// private readonly LoadingScreenProvider _loadingScreenProvider;
		
		public GameStateType Type => GameStateType.LoadLevel;

		public event Action<GameStateType> RequestNextState;

		public LoadLevelState(
			SceneLoader sceneLoader,
			LevelsStorage levels
			// LoadingScreenProvider loadingScreenProvider
		)
		{
			_sceneLoader = sceneLoader;
			_levels = levels;
			// _loadingScreenProvider = loadingScreenProvider;
		}

		public void Enter(Entity levelEntity)
		{
			LoadNewLevel(levelEntity);
		}

		private void LoadNewLevel(Entity levelEntity)
		{
			if (levelEntity.Has<LoadedLevel>())
			{
				var loadedLevel = levelEntity.GetComponent<LoadedLevel>();
				var loadedScene = loadedLevel.IsMenu ? _levels.MenuScene : _levels.GetScene(loadedLevel.Value - 1);
				_sceneLoader.UnloadLevel(loadedScene);
			}

			var currentLevel = levelEntity.GetComponent<CurrentLevel>();
			bool isRequestMenu = levelEntity.Has<RequestMenuLoad>();
			var scene = isRequestMenu ? _levels.MenuScene : _levels.GetScene(currentLevel.Index);

			if (isRequestMenu)
				levelEntity.RemoveComponent<RequestMenuLoad>();
			
			_sceneLoader.LoadLevel(
				scene,
				true,
				LoadSceneMode.Additive,
				() =>
				{
					levelEntity.SetComponent(new LoadedLevel { Value = currentLevel.Value, IsMenu = isRequestMenu});
					RequestNextState?.Invoke(GameStateType.InitializeGameplayProgress);
				}
			);
		}

		public void Enter() { }
		
		public void Update() { }

		public void Exit()
		{
			// _loadingScreenProvider.LoadingScreen.Hide();
		}
	}
}