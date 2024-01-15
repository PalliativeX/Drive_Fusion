using Core.Levels.Storages;
using Core.SceneManagement;
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
		private readonly LevelsStorage _levels;
		
		public World World { get; set; }

		public GameplaySceneInitializer(SceneLoader sceneLoader, LevelsStorage levels)
		{
			_sceneLoader = sceneLoader;
			_levels = levels;
		}

		public void OnAwake()
		{
			_sceneLoader.LoadLevel(_levels.GetScene(0), true, LoadSceneMode.Additive);
		}

		public void Dispose() { }
	}
}