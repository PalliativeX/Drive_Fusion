using Core.Gameplay;
using Core.Levels;
using Core.Menu;
using Core.UI.Settings;

namespace Core.UI.MainMenu
{
	public sealed class MainMenuModel
	{
		private readonly LevelsHelper _levelsHelper;
		private readonly PanelController _panelController;
		private readonly VehicleSelectionService _vehicleSelection;

		public MainMenuModel(LevelsHelper levelsHelper, PanelController panelController, VehicleSelectionService vehicleSelection)
		{
			_levelsHelper = levelsHelper;
			_panelController = panelController;
			_vehicleSelection = vehicleSelection;
		}

		public void OnPlay() => _levelsHelper.Play(1);

		public void OnSettings() => _panelController.Open<SettingsPresenter>();

		public void SelectPreviousVehicle() => _vehicleSelection.SelectPrevious();
		public void SelectNextVehicle() => _vehicleSelection.SelectNext();
		public bool IsOwned() => _vehicleSelection.IsOwned(_vehicleSelection.GetSelectedName());
		public (VehicleConfig, bool) GetVehicleData() => _vehicleSelection.GetVehicleData();
		public bool Buy() => _vehicleSelection.BuySelected();
		public (int, int) GetVehicleIndex()
		{
			(int, int) data = _vehicleSelection.GetVehicleIndex();
			data.Item1 += 1;
			return data;
		}
	}
}
