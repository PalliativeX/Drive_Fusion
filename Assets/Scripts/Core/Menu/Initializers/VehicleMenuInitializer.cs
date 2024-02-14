// using System.Collections.Generic;
// using Core.Gameplay;
// using Scellecs.Morpeh;
//
// namespace Core.Menu
// {
// 	public class VehicleMenuInitializer : IInitializer
// 	{
// 		private const string DefaultVehicle = "Default";
// 		
// 		private readonly VehicleSelectionService _selectionService;
// 		
// 		public World World { get; set; }
// 		
// 		public VehicleMenuInitializer(VehicleSelectionService selectionService) => 
// 			_selectionService = selectionService;
//
// 		public void OnAwake()
// 		{
// 			_selectionService.Initialize();
// 			_selectionService.Select(DefaultVehicle);
// 		}
//
// 		public void Dispose() { }
// 	}
// }