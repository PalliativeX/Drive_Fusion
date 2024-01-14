using Core.SceneManagement.Services;
using Core.SceneManagement.Storages;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.SceneManagement;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class GameplaySceneInitializer : IInitializer
	{
		private readonly SceneLoader _sceneLoader;
		private readonly SceneReferenceStorage _sceneReferences;
		
		public World World { get; set; }

		public GameplaySceneInitializer(SceneLoader sceneLoader, SceneReferenceStorage sceneReferences)
		{
			_sceneLoader = sceneLoader;
			_sceneReferences = sceneReferences;
		}

		public void OnAwake()
		{
			_sceneLoader.LoadLevel(_sceneReferences.Gameplay, true, LoadSceneMode.Additive);
		}

		public void Dispose() { }
	}
}