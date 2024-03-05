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
	public sealed class MovePlayerControllerSystem : ISystem
	{
		private readonly VehiclesStorage _vehicles;
		private Filter _filter;
		
		public World World { get; set; }

		public MovePlayerControllerSystem(VehiclesStorage vehicles)
		{
			_vehicles = vehicles;
		}

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
				ref Vector3 input = ref entity.GetComponent<MovementInput>().Value;

				if (!entity.Has<Active>())
				{
					input = Vector3.zero;
					continue;
				}

				ref var controller = ref entity.GetComponent<CarController>().Reference;

				float steeringFactor = EvaluateSteeringFactor(entity, input);

				controller.SetSteering(input.x * steeringFactor);
				controller.SetMotor(input.z);
				
				input = Vector3.zero;
			}
		}

		private float EvaluateSteeringFactor(Entity entity, Vector3 input) {
			ref var steeringFactor = ref entity.GetComponent<CurrentSteeringFactor>();
			if (input.x.IsZero()) {
				steeringFactor.Value = 0f;
				steeringFactor.Direction = SteeringDirection.None;
			}
			else {
				if (input.x < 0 && steeringFactor.Direction != SteeringDirection.Left) {
					steeringFactor.Value = 0f;
					steeringFactor.Direction = SteeringDirection.Left;
				}
				else if (input.x > 0 && steeringFactor.Direction != SteeringDirection.Right) {
					steeringFactor.Value = 0f;
					steeringFactor.Direction = SteeringDirection.Right;
				}

				steeringFactor.Value += Time.deltaTime * _vehicles.SteeringFactorMultiplier;
			}

			return _vehicles.SteeringFactorCurve.Evaluate(steeringFactor.Value);
		}

		public void Dispose() { }
	}
}