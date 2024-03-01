using System;

namespace Core.Integrations.SaveSystem
{
	[Serializable]
	public class LevelScoreData
	{
		public int Level;
		public float Score;

		public LevelScoreData(int level, float score)
		{
			Level = level;
			Score = score;
		}
	}
}