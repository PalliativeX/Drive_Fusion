using Core.Integrations.SaveSystem;
using Core.Menu;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.Gameplay
{
	public class VehiclesInitializer : IInitializer, ILoadable
	{
		private readonly VehicleSelectionService _selectionService;
		
		public World World { get; set; }
		
		public VehiclesInitializer(VehicleSelectionService selectionService) => 
			_selectionService = selectionService;

		public void Load(SaveData data) => Initialize(data);

		public void OnAwake() { }

		private async UniTaskVoid Initialize(SaveData saveData)
		{
			Entity entity = World.CreateEntity();

			entity.SetComponent(new OwnedVehicles { List = saveData.OwnedVehicles });
			entity.SetComponent(new SelectedVehicle { Value = saveData.SelectedVehicle });
			entity.SetComponent(new VehicleConfigComponent());

			await UniTask.DelayFrame(2);
			_selectionService.Initialize();
			_selectionService.Select(entity.GetComponent<SelectedVehicle>().Value);
		}

		public void Dispose() { }
	}
}