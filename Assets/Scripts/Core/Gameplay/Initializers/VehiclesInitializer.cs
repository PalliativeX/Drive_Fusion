using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Menu;
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

		// TODO: Add loading!
		public async void OnAwake()
		{
			Entity entity = World.CreateEntity();
			entity.SetComponent(new OwnedVehicles { List = new List<string> { DefaultVehicle }});
			entity.SetComponent(new SelectedVehicle { Value = DefaultVehicle });
			entity.SetComponent(new VehicleConfigComponent());

			await Task.Yield();
			_selectionService.Initialize();
		}

		public void Dispose() { }
	}
}