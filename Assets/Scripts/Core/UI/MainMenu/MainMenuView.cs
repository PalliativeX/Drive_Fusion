using TMPro;
using UnityEngine.UI;

namespace Core.UI.MainMenu
{
	public sealed class MainMenuView : BaseView
	{
		public AnimatedCountTextBehaviour CurrencyText;
		
		public Button SettingsButton;
		public Button PlayButton;
		public Button BuyVehicleButton;

		public Button PreviousVehicleButton;
		public Button NextVehicleButton;
		
		public TextMeshProUGUI VehiclePriceText;
		public TextMeshProUGUI CurrentVehicleIndex;
		public TextMeshProUGUI TotalVehicleCount;
	}
}
