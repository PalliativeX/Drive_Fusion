using System.Runtime.InteropServices;
using Debugging;
using SimpleInject;
using UnityEngine;
using Utils;

namespace Core.Integrations
{
	public sealed class RateUsShowController : IInitializable
	{
		private readonly RateUsConfig _config;
		
		private int _currentShowCount;

		public bool CanReview { get; set; }

		public RateUsShowController(RateUsConfig config) => 
			_config = config;

		public void Initialize()
		{
			if (Platform.IsYandexGames())
				CanReviewExternal();
			else
				CanReview = true;
		}

		public bool ShouldShowRateWindow()
		{
			return CanReview && _currentShowCount < _config.ShowCount;
		}

		public void Rate()
		{
#if DEBUG
			Debug.Log("Rated!");
#endif

			CanReview = false;
			
			if (Platform.IsYandexGames())
				OpenRateWindowExternal();
		}

		public void IncrementShowCount() => _currentShowCount++;


		[DllImport("__Internal")]
		private static extern void OpenRateWindowExternal();

		public void SetCanReview(int value)
		{
			CanReview = value == 1;
			
#if DEBUG
			ConsoleLogger.Instance.Log("Can Review Unity: " + CanReview);
#endif
		}

		[DllImport("__Internal")]
		private static extern void CanReviewExternal();
	}
}