using Core.Gameplay;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.ECS
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class SetPreviousPositionSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<Position>()
				.With<TrackPositionChange>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref Vector3 position = ref entity.GetComponent<Position>().Value;

				if (entity.Has<PreviousPosition>())
				{
					ref var previousPosition = ref entity.GetComponent<PreviousPosition>();
					previousPosition.Value = position;
				}
				else
					entity.SetComponent<PreviousPosition>(new PreviousPosition { Value = position });
			}
		}
		
		public void Dispose() { }
	}
}