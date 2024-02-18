using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.MainMenu
{
	public sealed class MainMenuView : BaseView
	{
		public AnimatedCountTextBehaviour CurrencyText;

		public Button PlayButton;
		public Button BuyVehicleButton;

		public Button PreviousVehicleButton;
		public Button NextVehicleButton;
		
		public TextMeshProUGUI VehiclePriceText;
		public TextMeshProUGUI CurrentVehicleIndex;
		public TextMeshProUGUI TotalVehicleCount;

		[Header("Settings UI")] 
		public UiToggle SoundToggle;

		[Header("Vehicle Stats")] 
		public TextMeshProUGUI VehicleName;
		public VehicleParameterUi[] Parameters;
	}
}
