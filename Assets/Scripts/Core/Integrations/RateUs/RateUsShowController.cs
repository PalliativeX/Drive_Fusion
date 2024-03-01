using System.Runtime.InteropServices;
using Core.Integrations.SaveSystem;
using Debugging;
using SimpleInject;
using UnityEngine;
using Utils;

namespace Core.Integrations
{
	public sealed class RateUsShowController : IInitializable, ILoadable
	{
		private readonly RateUsConfig _config;
		private readonly SaveService _save;

		private int _currentShowCount;

		public bool CanReview { get; set; }

		public RateUsShowController(RateUsConfig config, SaveService save)
		{
			_config = config;
			_save = save;
		}

		public void Initialize()
		{
			_save.Initialized += SetCanReview;
		}

		public void Load(SaveData data)
		{
			_currentShowCount = data.RateUsShowCount;
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
			
			if (Platform.Instance.IsYandexGames())
				OpenRateWindowExternal();
		}

		public void IncrementShowCount()
		{
			_currentShowCount++;
			_save.SaveData.RateUsShowCount = _currentShowCount;
			_save.Save();
		}

		public void SetCanReview(int value)
		{
			CanReview = value == 1;
			
#if DEBUG
			ConsoleLogger.Instance.Log("Can Review Unity: " + CanReview);
#endif
		}

		private void SetCanReview() {
			if (Platform.Instance.IsYandexGames())
				CanReviewExternal();
			else
				CanReview = true;
		}

		[DllImport("__Internal")]
		private static extern void OpenRateWindowExternal();

		[DllImport("__Internal")]
		private static extern void CanReviewExternal();
	}
}