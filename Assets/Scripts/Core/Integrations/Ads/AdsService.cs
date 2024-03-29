﻿using System;
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
		private Action _interstitialCallback;

		[DllImport("__Internal")]
		private static extern void ShowInterstitialAdExternal();

		[DllImport("__Internal")]
		private static extern void ShowRewardedAdExternal();

		public event Action OnRewardedAdClosedCallback;
		public event Action OnRewardedAdRewarded;

		public void ShowInterstitialAd(Action interstitialCallback = null)
		{
			if (Time.realtimeSinceStartup - _time > TimeBetweenAds)
			{
#if DEBUG
				Debug.Log("TimePassed:" + (Time.realtimeSinceStartup - _time) + "	Show Ad");
#endif

				if (Platform.Instance.IsYandexGames())
				{
					_interstitialCallback = interstitialCallback;
					SwitchAudioActive(false);
					ShowInterstitialAdExternal();
				}
				else
				{
					_time = Time.realtimeSinceStartup;
					interstitialCallback?.Invoke();
				}
			}
			else
			{
#if DEBUG
				Debug.Log("TimePassed: " + (Time.realtimeSinceStartup - _time).ToString("F1") + "	Dont Show Ad");
#endif
				interstitialCallback?.Invoke();
			}
		}

		public void ShowRewardedAd(Action<bool> callback)
		{
			if (!Platform.Instance.IsYandexGames())
			{
				callback?.Invoke(true);
				OnRewardedAdClosedCallback?.Invoke();
				OnRewardedAdRewarded?.Invoke();
				return;
			}

			_rewardedCallback = callback;
			SwitchAudioActive(false);
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
			_interstitialCallback?.Invoke();
			_interstitialCallback = null;
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

		private void SwitchAudioActive(bool active) => AudioListener.volume = active ? 1f : 0f;
	}
}