using Core.Integrations.Ads;
using Scellecs.Morpeh;

namespace Core.Menu
{
	public class MenuInitializer : IInitializer
	{
		private readonly AdsService _ads;
		
		public World World { get; set; }

		public MenuInitializer(AdsService ads) => _ads = ads;

		public void OnAwake()
		{
			_ads.ShowInterstitialAd();
		}

		public void Dispose() { }
	}
}