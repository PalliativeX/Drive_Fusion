using Core.Currency;
using Core.ECS;
using Core.Gameplay;
using Core.Integrations.SaveSystem;
using Scellecs.Morpeh;

namespace Core.Menu
{
	public class VehicleSelectionService
	{
		private readonly World _world;
		private readonly VehiclesStorage _vehicles;
		private readonly MoneyManager _moneyManager;
		private readonly SaveService _save;

		private readonly Filter _vehiclesFilter;

		public VehicleSelectionService(GlobalWorld world, VehiclesStorage vehicles, MoneyManager moneyManager, SaveService save)
		{
			_world = world;
			_vehicles = vehicles;
			_moneyManager = moneyManager;
			_save = save;

			_vehiclesFilter = _world.Filter
				.With<OwnedVehicles>()
				.With<SelectedVehicle>()
				.Build();
		}

		public void Initialize()
		{
			var entity = _vehiclesFilter.First();
			entity.SetComponent(new TotalVehicleCount { Value = _vehicles.Configs.Count });
			entity.SetComponent(new SelectedVehicleIndex { Value = 0 });
		}

		public void Select(string vehicleName)
		{
			VehicleConfig config = _vehicles.Get(vehicleName);
			OnSelect(config);
		}

		public void SelectNext()
		{
			string name = GetSelectedName();
			VehicleConfig nextConfig = _vehicles.GetNext(name);
			OnSelect(nextConfig);
		}

		public void SelectPrevious()
		{
			string name = GetSelectedName();
			VehicleConfig previousConfig = _vehicles.GetPrevious(name);
			OnSelect(previousConfig);
		}

		public (VehicleConfig, bool) GetVehicleData()
		{
			var entity = _vehiclesFilter.First();
			var selectedVehicle = entity.GetComponent<SelectedVehicle>();
			return (entity.GetComponent<VehicleConfigComponent>().Reference, IsOwned(selectedVehicle.Value));
		}
		
		public (int, int) GetVehicleIndex()
		{
			var entity = _vehiclesFilter.First();
			var selectedVehicleIndex = entity.GetComponent<SelectedVehicleIndex>();
			var totalVehicleCount = entity.GetComponent<TotalVehicleCount>();
			return (selectedVehicleIndex.Value, totalVehicleCount.Value);
		}

		public bool IsOwned(string vehicleName)
		{
			var ownedVehicles = _vehiclesFilter.First().GetComponent<OwnedVehicles>();
			return ownedVehicles.List.Contains(vehicleName);
		}

		public string GetSelectedName() => _vehiclesFilter.First().GetComponent<SelectedVehicle>().Value;


		public bool BuySelected()
		{
			(VehicleConfig config, bool isOwned) = GetVehicleData();

			if (!_moneyManager.HasEnoughMoney(config.Price))
				return false;
			
			_moneyManager.RemoveMoney(config.Price);
			
			ref var ownedVehicles = ref _vehiclesFilter.First().GetComponent<OwnedVehicles>();
			ownedVehicles.List.Add(config.Name);
			
			_save.SaveData.OwnedVehicles = ownedVehicles.List;
			_save.Save();
			
			return true;
		}

		private void OnSelect(VehicleConfig newConfig) {
			var entity = _vehiclesFilter.First();
			ref var selectedVehicle = ref entity.GetComponent<SelectedVehicle>();
			ref var selectedVehicleIndex = ref entity.GetComponent<SelectedVehicleIndex>();
			ref var vehicleConfig = ref entity.GetComponent<VehicleConfigComponent>();

			string newSelected = newConfig.Name;
			selectedVehicle.Value = newSelected;
			vehicleConfig.Reference = newConfig;
			selectedVehicleIndex.Value = _vehicles.Configs.IndexOf(newConfig);
			
			entity.SetComponent(new Changed());

			if (entity.GetComponent<OwnedVehicles>().List.Contains(newSelected) &&
			    newSelected != _save.SaveData.SelectedVehicle)
			{
				_save.SaveData.SelectedVehicle = newSelected;
				_save.Save();
			}
		}
	}
}