using System;
using Core.Currency;
using Core.Integrations;
using Core.Levels;
using SimpleInject;
using Utils;

namespace Core.Gameplay
{
	public class CurrentLevelService : IInitializable
	{
		private readonly LevelsHelper _levels;
		private readonly LeaderboardService _leaderboard;
		private readonly MoneyManager _moneyManager;

		public float Score { get; private set; }
		public float CurrentScoreRecord { get; private set; }

		public int EarnedMoney { get; private set; }
		
		public event Action<float> ScoreChanged;

		public CurrentLevelService(LevelsHelper levels, LeaderboardService leaderboard, MoneyManager moneyManager)
		{
			_levels = levels;
			_leaderboard = leaderboard;
			_moneyManager = moneyManager;
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

		public void AddEarnings(int money) => EarnedMoney += money;

		public void AddTotalEarnings(int earnings)
		{
			_moneyManager.AddMoney(earnings);
			EarnedMoney = 0;
		}
	}
}