using System.Runtime.InteropServices;
using Utils;

namespace Core.Integrations
{
	public class LeaderboardService
	{
		public void SetNewScore(int score)
		{
			if (Platform.Instance.IsYandexGames())
				SetLeaderboardValueExternal(score);
		}

		[DllImport("__Internal")]
		private static extern void SetLeaderboardValueExternal(int value);
	}
}