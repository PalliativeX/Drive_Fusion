using Core.Integrations.Ads;
using Core.Integrations.SaveSystem;
using SimpleInject;

namespace Core.Integrations
{
	/// <summary>
	/// Receives events from JS Libs and passes them to the appropriate services.
	/// </summary>
	public sealed class JsEventsReceiver : Singleton<JsEventsReceiver>
	{
		private AdsService _ads;
		private RateUsShowController _rateUs;
		private SaveService _saveService;

		[Inject]
		public void Inject(AdsService ads, RateUsShowController rateUs, SaveService saveService)
		{
			_ads = ads;
			_rateUs = rateUs;
			_saveService = saveService;
		}

		public void OnInterstitialAdOpened() => _ads.OnInterstitialAdOpened();

		public void OnInterstitialAdClosed() => _ads.OnInterstitialAdClosed();

		public void OnRewardedAdOpened()     => _ads.OnRewardedAdOpened();

		public void OnRewardedAdClosed()     => _ads.OnRewardedAdClosed();

		public void OnRewardedAdGetReward()  => _ads.OnRewardedAdGetReward();

		public void OnRewardedAdFail()       => _ads.OnRewardedAdFail();

		public void SetCanReview(int value)  => _rateUs.SetCanReview(value);
		
		public void SetPlayerProgressData(string data)
		{
			_saveService.SetPlayerProgressData(data);
		}
	}
}