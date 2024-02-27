using Core.Integrations;

namespace Core.UI.RateUs
{
	public sealed class RateUsModel
	{
		private readonly PanelController _panelController;
		private readonly RateUsShowController _rateUsController;

		public RateUsModel(PanelController panelController, RateUsShowController rateUsController)
		{
			_panelController = panelController;
			_rateUsController = rateUsController;
		}

		public void OnShow() => _rateUsController.IncrementShowCount();

		public bool ShouldShowRateWindow() => _rateUsController.ShouldShowRateWindow();

		public void Show() => _panelController.Open<RateUsPresenter>();

		public void OnRate()
		{
			_rateUsController.Rate();

			OnClose();
		}

		public void OnClose() => _panelController.Close<RateUsPresenter>();
	}
}
