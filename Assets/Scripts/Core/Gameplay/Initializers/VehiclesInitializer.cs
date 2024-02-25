using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Menu;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;

namespace Core.Gameplay
{
	public class VehiclesInitializer : IInitializer
	{
		private const string DefaultVehicle = "Default";
		
		private readonly VehicleSelectionService _selectionService;
		
		public World World { get; set; }
		
		public VehiclesInitializer(VehicleSelectionService selectionService) => 
			_selectionService = selectionService;
		
		public void OnAwake() => Initialize();

		// TODO: Add loading!
		private async UniTaskVoid Initialize()
		{
			Entity entity = World.CreateEntity();
			entity.SetComponent(new OwnedVehicles { List = new List<string> { DefaultVehicle }});
			entity.SetComponent(new SelectedVehicle { Value = DefaultVehicle });
			entity.SetComponent(new VehicleConfigComponent());

			await UniTask.Yield();
			_selectionService.Initialize();
			_selectionService.Select(DefaultVehicle);
		}

		public void Dispose() { }
	}
}