using Core.Currency;
using Core.Gameplay;
using DG.Tweening;
using UniRx;
using Utils;

namespace Core.UI.MainMenu
{
	public sealed class MainMenuPresenter : APresenter<MainMenuView>
	{
		private readonly MainMenuModel _model;
		private readonly MoneyManager _moneyManager;

		private Tween _updateMoneyTween;
		
		public override string Name => "MainMenu";

		public MainMenuPresenter(MainMenuModel model, MoneyManager moneyManager)
		{
			_model = model;
			_moneyManager = moneyManager;
		}

		protected override void OnShow()
		{
			base.OnShow();

			View.SettingsButton.OnClickSubscribeDisposable(_model.OnSettings).AddTo(Disposable);
			View.PlayButton.OnClickSubscribeDisposable(_model.OnPlay).AddTo(Disposable);
			View.BuyVehicleButton.OnClickSubscribeDisposable(OnBuy).AddTo(Disposable);
			View.PreviousVehicleButton.OnClickSubscribeDisposable(() =>
				{
					_model.SelectPreviousVehicle();
					OnSelectNewVehicle();
				}
			).AddTo(Disposable);
			View.NextVehicleButton.OnClickSubscribeDisposable(() =>
				{
					_model.SelectNextVehicle();
					OnSelectNewVehicle();
				}
			).AddTo(Disposable);
			_moneyManager.MoneyChanged += OnMoneyChanged;
			
			View.CurrencyText.SetCount(_moneyManager.Money, false);
			OnSelectNewVehicle();
		}

		protected override void OnClose()
		{
			base.OnClose();
			
			_moneyManager.MoneyChanged -= OnMoneyChanged;
		}

		private void OnMoneyChanged(int newMoney) => 
			View.CurrencyText.SetCount(_moneyManager.Money, true);

		private void OnSelectNewVehicle()
		{
			(VehicleConfig config, bool isOwned) = _model.GetVehicleData();
			View.PlayButton.SetActive(isOwned);
			View.BuyVehicleButton.SetActive(!isOwned);

			if (!isOwned)
			{
				View.VehiclePriceText.SetText(config.Price.ToString());
			}

			SetVehicleIndex();
		}

		private void OnBuy()
		{
			if (!_model.Buy())
				return;
			
			OnSelectNewVehicle();
		}

		private void SetVehicleIndex() {
			(int currentIndex, int totalCount) = _model.GetVehicleIndex();
			View.CurrentVehicleIndex.SetText(currentIndex.ToString());
			View.TotalVehicleCount.SetText(totalCount.ToString());
		}
	}
}
