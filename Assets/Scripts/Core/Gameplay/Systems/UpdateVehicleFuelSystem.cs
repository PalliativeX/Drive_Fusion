using Core.ECS;
using Core.InputLogic;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class UpdateVehicleFuelSystem : ISystem
	{
		private Filter _filter;
		
		public World World { get; set; }

		public void OnAwake()
		{
			_filter = World.Filter
				.With<Fuel>()
				.Without<MovementInputBlocked>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				if (!entity.Has<Active>())
					continue;
				
				var config = entity.GetComponent<VehicleConfigComponent>().Reference;

				ref var fuel = ref entity.GetComponent<Fuel>();
				fuel.Value -= deltaTime * config.FuelConsumptionPerSecond;
				
				if (fuel.Value <= 0f && !entity.Has<Stopped>())
					entity.SetComponent(new StopRequested());
			}
		}
		
		public void Dispose() { }
	}
}