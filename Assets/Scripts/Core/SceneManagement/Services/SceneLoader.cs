using System;
using System.Collections;
using Core.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.SceneManagement
{
	public sealed class SceneLoader : ISceneLoaderService
	{
		private const float MaxLoadProgress = 0.9f;

		private readonly CoroutineRunner _coroutineRunner;
		
		public bool IsLoadingScene { get; private set; }
		public float CurrentLoadProgress { get; private set; }

		public event Action LoadStarted;
		public event Action UnloadStarted;
		public event Action<SceneReference> LoadedLevel;
		public event Action<SceneReference> UnloadedLevel;
		
		public event Action<float> LoadProgressChanged;

		public SceneLoader(CoroutineRunner coroutineRunner) => 
			_coroutineRunner = coroutineRunner;

		public void LoadLevel(SceneReference scene, bool setActive, LoadSceneMode loadMode = LoadSceneMode.Additive, Action onLoaded = null) => 
			_coroutineRunner.StartCoroutine(LoadSceneCoroutine(scene, setActive, loadMode, onLoaded));

		public void UnloadLevel(SceneReference scene) => 
			_coroutineRunner.StartCoroutine(UnloadSceneCoroutine(scene));

		private IEnumerator LoadSceneCoroutine(
			SceneReference sceneReference,
			bool setActive,
			LoadSceneMode loadMode = LoadSceneMode.Additive,
			Action onLoaded = null
		)
		{
			string sceneName = sceneReference.GetSceneName();
			
			CurrentLoadProgress = 0f;
			IsLoadingScene = true;
			
			LoadStarted?.Invoke();
            
			AsyncOperation sceneLoadAsync = SceneManager.LoadSceneAsync(sceneName, loadMode);
			while (!sceneLoadAsync.isDone)
			{
				CurrentLoadProgress = Mathf.Clamp01(sceneLoadAsync.progress / MaxLoadProgress);
				LoadProgressChanged?.Invoke(CurrentLoadProgress);
				yield return null;
			}

			var scene = SceneManager.GetSceneByName(sceneName);
			if (setActive)
				SceneManager.SetActiveScene(scene);

			IsLoadingScene = false;
            
			onLoaded?.Invoke();
            
			LoadedLevel?.Invoke(sceneReference);
		}
		
		private IEnumerator UnloadSceneCoroutine(SceneReference sceneReference)
		{
			string sceneName = sceneReference.GetSceneName();
			
			UnloadStarted?.Invoke();
            
			AsyncOperation sceneUnloadAsync = SceneManager.UnloadSceneAsync(sceneName);
			while (!sceneUnloadAsync.isDone)
			{
				CurrentLoadProgress = sceneUnloadAsync.progress;
				yield return null;
			}

			UnloadedLevel?.Invoke(sceneReference);
		}
	}
}