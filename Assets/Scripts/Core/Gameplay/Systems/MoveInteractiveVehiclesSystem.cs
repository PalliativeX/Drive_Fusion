using Core.ECS;
using Core.InputLogic;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Utils;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class MoveInteractiveVehiclesSystem : ISystem
	{
		private Filter _filter;
		
		public World World { get; set; }

		public void OnAwake()
		{
			_filter = World.Filter
				.With<InteractiveVehicleSpeed>()
				.With<View>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				float speed = entity.GetComponent<InteractiveVehicleSpeed>().Value;

				ref Position position = ref entity.GetComponent<Position>();
				Rotation rotation = entity.GetComponent<Rotation>();

				position.Value += rotation.Direction * speed * deltaTime;
			}
		}

		public void Dispose() { }
	}
}