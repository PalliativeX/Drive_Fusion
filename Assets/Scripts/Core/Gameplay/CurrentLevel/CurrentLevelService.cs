using System;
using Core.Integrations;
using Core.Integrations.SaveSystem;
using Core.Levels;
using SimpleInject;
using Utils;

namespace Core.Gameplay
{
	public class CurrentLevelService : IInitializable
	{
		private readonly LevelsHelper _levels;
		private readonly LeaderboardService _leaderboard;
		private readonly SaveService _save;

		public float Score { get; private set; }
		public float CurrentScoreRecord { get; private set; }
		
		public event Action<float> ScoreChanged;

		public CurrentLevelService(LevelsHelper levels, LeaderboardService leaderboard, SaveService save)
		{
			_levels = levels;
			_leaderboard = leaderboard;
			_save = save;
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
			if (Score <= CurrentScoreRecord)
				return;
			
			_levels.SetCurrentLevelScoreRecord(Score);
			
			_leaderboard.SetNewScore(Score.ToInt());
		}
	}
}