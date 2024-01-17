using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.InputLogic
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class MovePlayerSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<MovementInput>()
				.With<Position>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref Vector3 input = ref entity.GetComponent<MovementInput>().Value;
				if (input == Vector3.zero)
					continue;
				
				ref var position = ref entity.GetComponent<Position>();
				position.Value += input * Time.deltaTime * 3f;

				input = Vector3.zero;
			}
		}
		
		public void Dispose() { }
	}
}