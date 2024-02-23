using Core.ECS;
using DavidJalbert;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class HandleCollisionEventsSystem : ISystem
	{
		private readonly VehiclesStorage _vehicles;
		private readonly TriggerHandler _triggerHandler;
		private Filter _filter;
		
		public World World { get; set; }

		public HandleCollisionEventsSystem(VehiclesStorage vehicles) => 
			_vehicles = vehicles;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<CollisionEnterEvents>()
				.With<Durability>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter)
			{
				ref var collisionEvents = ref entity.GetComponent<CollisionEnterEvents>();

				for (int i = collisionEvents.List.Count - 1; i >= 0; i--)
				{
					CollisionEventData collisionData = collisionEvents.List[i];
					Debug.Log(collisionData.CollisionForce + ", HasHitSide: " + collisionData.CollisionDot);
					
					var durability = entity.GetComponent<Durability>();
					float durabilityChange = _vehicles.CollisionCurve.Evaluate(collisionData.CollisionDot);
					entity.ChangeDurability(durability.Value - durabilityChange);
					
					collisionEvents.List.RemoveAt(i);
				}
			}
		}
		
		public void Dispose() { }
	}
}