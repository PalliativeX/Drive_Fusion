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
				_sceneLoader.UnloadLevel(_levels.GetScene(levelEntity.GetComponent<LoadedLevel>().Value - 1));

			var currentLevel = levelEntity.GetComponent<CurrentLevel>();
			_sceneLoader.LoadLevel(
				_levels.GetScene(currentLevel.Index),
				true,
				LoadSceneMode.Additive,
				() =>
				{
					levelEntity.SetComponent(new LoadedLevel { Value = currentLevel.Value });
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