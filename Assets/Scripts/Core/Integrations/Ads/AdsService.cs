using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Utils;

namespace Core.Integrations.Ads
{
	public sealed class AdsService
	{
		private const float TimeBetweenAds = 60f;
		
		private float _time = 0;
		private Action<bool> _rewardedCallback;

		[DllImport("__Internal")]
		private static extern void ShowInterstitialAdExternal();

		[DllImport("__Internal")]
		private static extern void ShowRewardedAdExternal();
		
		public event Action OnRewardedAdClosedCallback;
		public event Action OnRewardedAdRewarded;
		
		public void ShowInterstitialAd()
		{
			if (Time.realtimeSinceStartup - _time > TimeBetweenAds)
			{
#if DEBUG
				Debug.Log("TimePassed:" + (Time.realtimeSinceStartup - _time) + "	Show Ad");
#endif

				if (Platform.IsYandexGames())
					ShowInterstitialAdExternal();
				else
					_time = Time.realtimeSinceStartup;
			}
#if DEBUG
			else
			{
				Debug.Log("TimePassed: " + (Time.realtimeSinceStartup - _time).ToString("F1") + "	Dont Show Ad");
			}
#endif
		}
		
		public void ShowRewardedAd(Action<bool> callback)
		{
			if (!Platform.IsYandexGames())
			{
				callback?.Invoke(true);
				OnRewardedAdClosedCallback?.Invoke();
				OnRewardedAdRewarded?.Invoke();
				return;
			}

			_rewardedCallback = callback;
			ShowRewardedAdExternal();
		}
		
		// NOTE: -------------- CALLBACKS CALLED FROM JSLIB! --------------------------------------------
		
		public void OnInterstitialAdOpened()
		{
			SwitchAudioActive(false);
		}

		public void OnInterstitialAdClosed()
		{
			SwitchAudioActive(true);
			_time = Time.realtimeSinceStartup;
		}

		public void OnRewardedAdOpened()
		{
			SwitchAudioActive(false);
		}

		public void OnRewardedAdClosed()
		{
			SwitchAudioActive(true);
			OnRewardedAdClosedCallback?.Invoke();
			
			_rewardedCallback?.Invoke(true);
			_rewardedCallback = null;
		}

		public void OnRewardedAdGetReward()
		{
			OnRewardedAdRewarded?.Invoke();
		}
		
		public void OnRewardedAdFail()
		{
			SwitchAudioActive(true);
			OnRewardedAdClosedCallback?.Invoke();
			
			_rewardedCallback?.Invoke(false);
			_rewardedCallback = null;
		}

		private void SwitchAudioActive(bool active) => AudioListener.pause = !active;
	}
}