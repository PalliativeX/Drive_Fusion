using Core.Infrastructure.GameFsm;
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
	public sealed class MovePlayerWheelColliderSystem : IFixedSystem
	{
		private readonly GameStateMachine _stateMachine;
		private Filter _filter;
		
		public World World { get; set; }

		public MovePlayerWheelColliderSystem(GameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void OnAwake()
		{
			_filter = World.Filter
				.With<MovementInput>()
				.With<Wheels>()
				.With<WheelMeshes>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			if (_stateMachine.ActiveState != GameStateType.Gameplay)
				return;
			
			foreach (var entity in _filter)
			{
				var config = entity.GetComponent<VehicleConfigComponent>().Reference;

				ref var wheels = ref entity.GetComponent<Wheels>();
				ref var wheelMeshes = ref entity.GetComponent<WheelMeshes>();

				ref Vector3 input = ref entity.GetComponent<MovementInput>().Value;
				
				float currentForce = (input.x.IsZero() ? config.MotorForce : config.SteeringMotorForce);
				wheels.FrontLeftWheel.motorTorque = currentForce;
				wheels.FrontRightWheel.motorTorque = currentForce;
				// wheels.RearLeftWheel.motorTorque = currentForce;
				// wheels.RearRightWheel.motorTorque = currentForce;
				
				wheels.FrontLeftWheel.brakeTorque = 0f;
				wheels.FrontRightWheel.brakeTorque = 0f;
				// wheels.RearLeftWheel.brakeTorque = 0f;
				// wheels.RearRightWheel.brakeTorque = 0f;

				// float currentForce = input.z * (input.x.IsZero() ? config.MotorForce : config.SteeringMotorForce);
				// wheels.FrontLeftWheel.motorTorque = currentForce;
				// wheels.FrontRightWheel.motorTorque = currentForce;
				// wheels.RearLeftWheel.motorTorque = currentForce;
				// wheels.RearRightWheel.motorTorque = currentForce;
				
				float currentSteerAngle = config.MaxSteeringAngle * input.x;
				wheels.FrontLeftWheel.steerAngle = currentSteerAngle;
				wheels.FrontRightWheel.steerAngle = currentSteerAngle;

				input = Vector3.zero;
				
				UpdateWheel(wheels.FrontLeftWheel, wheelMeshes.FrontLeftWheelMesh);
				UpdateWheel(wheels.FrontRightWheel, wheelMeshes.FrontRightWheelMesh);
				UpdateWheel(wheels.RearLeftWheel, wheelMeshes.RearLeftWheelMesh);
				UpdateWheel(wheels.RearRightWheel, wheelMeshes.RearRightWheelMesh);
			}
		}
		
		public void Dispose() { }

		private void UpdateWheel(WheelCollider wheel, Transform wheelMesh)
		{
			wheel.GetWorldPose(out Vector3 position, out Quaternion rotation);
			wheelMesh.position = position;
			wheelMesh.rotation = rotation;
		}
	}
}