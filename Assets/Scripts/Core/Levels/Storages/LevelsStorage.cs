using System;
using System.Collections.Generic;
using Core.Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Levels.Storages
{
	[CreateAssetMenu(fileName = nameof(LevelsStorage), menuName = "Storages/" + nameof(LevelsStorage))]
	public sealed class LevelsStorage : ScriptableObject
	{
		public SceneReference MenuScene;
		[FormerlySerializedAs("ScenesByLevel")] public List<LevelEntry> LevelEntries;

		public SceneReference GetScene(int levelIndex) => LevelEntries[levelIndex].Scene;

		public int LastLevel() => LevelEntries.Count - 1;
	}
	
	[Serializable]
	public class LevelEntry
	{
		public SceneReference Scene;
		public SoundId MainThemeId = SoundId.MainTheme;

	}
}