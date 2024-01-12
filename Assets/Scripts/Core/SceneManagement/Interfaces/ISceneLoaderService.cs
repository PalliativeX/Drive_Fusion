using System;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement.Interfaces
{
	public interface ISceneLoaderService
	{
		 bool IsLoadingScene { get; }
		 float CurrentLoadProgress { get; }

		 event Action LoadStarted;
		 event Action UnloadStarted;
		 event Action<SceneReference> LoadedLevel;
		 event Action<SceneReference> UnloadedLevel;
		
		 event Action<float> LoadProgressChanged;

		 void LoadLevel(SceneReference scene, bool setActive, LoadSceneMode loadMode = LoadSceneMode.Additive, Action onLoaded = null);
		 void UnloadLevel(SceneReference scene);
	}
}