using System;
using Core.Levels;
using SimpleInject;

namespace Core.Gameplay
{
	public class CurrentLevelService : IInitializable
	{
		private readonly LevelsHelper _levels;
		
		public float Score { get; private set; }
		public float CurrentScoreRecord { get; private set; }
		
		public event Action<float> ScoreChanged;

		public CurrentLevelService(LevelsHelper levels)
		{
			_levels = levels;
		}

		public void Initialize()
		{
			CurrentScoreRecord = _levels.GetCurrentLevelScoreRecord();
		}

		public void AddScore(float score) => 
			SetScore(Score + score);

		public void SetScore(float score)
		{
			Score = score;
			ScoreChanged?.Invoke(score);
		}

		public void TryUpdateRecord()
		{
			if (Score > CurrentScoreRecord)
                _levels.SetCurrentLevelScoreRecord(Score);
		}
	}
}