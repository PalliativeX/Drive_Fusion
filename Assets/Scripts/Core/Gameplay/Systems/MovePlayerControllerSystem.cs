using Core.Infrastructure.GameFsm;
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
		private readonly GameStateMachine _stateMachine;
		private Filter _filter;
		
		public World World { get; set; }

		public MovePlayerControllerSystem(GameStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
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
			if (_stateMachine.ActiveState != GameStateType.Gameplay)
				return;
			
			foreach (var entity in _filter)
			{
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