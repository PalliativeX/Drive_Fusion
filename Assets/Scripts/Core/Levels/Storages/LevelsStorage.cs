using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Levels.Storages
{
	[CreateAssetMenu(fileName = nameof(LevelsStorage), menuName = "Storages/" + nameof(LevelsStorage))]
	public sealed class LevelsStorage : ScriptableObject
	{
		public List<SceneByLevel> ScenesByLevel;

		public SceneReference GetScene(int levelIndex) => ScenesByLevel[levelIndex].Scene;

		public int LastLevel() => ScenesByLevel.Count - 1;
	}
	
	[Serializable]
	public class SceneByLevel
	{
		// public int Level;
		public SceneReference Scene;
	}
}