using Core.AssetManagement;
using Core.ECS;
using Core.Gameplay;
using Core.Levels;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Menu
{
	public class VehicleSelectionSpawnSystem : ISystem
	{
		private readonly World _globalWorld;
		private readonly IAssetProvider _assetProvider;
		private readonly MenuLevelBehaviour _menuLevel;

		private Filter _selectedVehicleFilter;
		private Filter _vehicleBehaviourFilter;

		public World World { get; set; }

		public VehicleSelectionSpawnSystem(GlobalWorld globalWorld, AssetProvider assetProvider, MenuLevelBehaviour menuLevel)
		{
			_globalWorld = globalWorld;
			_assetProvider = assetProvider;
			_menuLevel = menuLevel;
		}

		public void OnAwake()
		{
			_selectedVehicleFilter = _globalWorld.Filter
				.With<SelectedVehicle>()
				.With<Changed>()
				.Build();

			var entity = World.CreateEntity();
			entity.SetComponent(new SelectedVehicleMenuBehaviour());
			entity.SetComponent(new View());
			entity.SetComponent(new Pooled());
			
			_vehicleBehaviourFilter = World.Filter
				.With<SelectedVehicleMenuBehaviour>()
				.Build();

			var selected = _globalWorld.Filter.With<SelectedVehicle>().Build().First();
			if (!selected.Has<Changed>())
				selected.SetComponent(new Changed());
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _selectedVehicleFilter)
			{
				var name = entity.GetComponent<SelectedVehicle>().Value;

				var behaviourEntity = _vehicleBehaviourFilter.First();
				
				ref var view = ref behaviourEntity.GetComponent<View>();
				ref var pooled = ref behaviourEntity.GetComponent<Pooled>();
				if (view.Reference)
					_assetProvider.Return(view.Reference, pooled.AssetName);
				
				(var vehicle, bool isPooled) = _assetProvider.LoadAsset(name);
				view.Reference = vehicle;
				pooled.AssetName = name;
				
				vehicle.transform.SetParent(_menuLevel.VehicleSpawnPoint, false);

				entity.RemoveComponent<Changed>();
			}
		}

		public void Dispose() { }
	}
}