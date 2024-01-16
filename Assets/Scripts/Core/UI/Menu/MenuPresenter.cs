using Core.Currency;

namespace Core.UI.Menu
{
	public class MenuPresenter : APresenter<MenuView>
	{
		private readonly MenuModel _model;
		private readonly MoneyManager _moneyManager;

		public override string Name => "Menu";

		public MenuPresenter(MenuModel model, MoneyManager moneyManager)
		{
			_model = model;
			_moneyManager = moneyManager;
		}

		protected override void OnShow()
		{
			View.SoundToggle.SwitchActive(_model.IsSoundActive());
			View.SoundToggle.Button.onClick.AddListener(ToggleSoundActive);
			
			View.StartGameplayButton.onClick.AddListener(_model.StartPlaying);
			
			UpdateMoney(_moneyManager.Money);
			_moneyManager.MoneyChanged += UpdateMoney;
			
			View.LevelText.SetText($"Level {_model.GetCurrentLevel()}");
		}

		protected override void OnClose()
		{
			View.SoundToggle.Button.onClick.RemoveListener(ToggleSoundActive);
			
			View.StartGameplayButton.onClick.RemoveListener(_model.StartPlaying);
			
			_moneyManager.MoneyChanged -= UpdateMoney;
		}

		private void ToggleSoundActive()
		{
			_model.ToggleSoundActive();
			View.SoundToggle.Toggle();
		}

		private void UpdateMoney(int money)
		{
			View.MoneyCountText.SetText(money.ToString());
		}
	}
}