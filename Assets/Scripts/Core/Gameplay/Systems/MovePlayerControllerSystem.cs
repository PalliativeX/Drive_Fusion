using Core.ECS;
using Core.InputLogic;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class MovePlayerControllerSystem : ISystem
	{
		private Filter _filter;
		
		public World World { get; set; }

		public void OnAwake()
		{
			_filter = World.Filter
				.With<MovementInput>()
				.With<CarController>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				if (!entity.Has<Active>())
					continue;
				
				ref var controller = ref entity.GetComponent<CarController>().Reference;

				ref Vector3 input = ref entity.GetComponent<MovementInput>().Value;

				controller.SetSteering(input.x);
				controller.SetMotor(input.z);
				
				input = Vector3.zero;
			}
		}
		
		public void Dispose() { }
	}
}