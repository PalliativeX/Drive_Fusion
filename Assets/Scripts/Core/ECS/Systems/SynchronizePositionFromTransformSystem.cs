using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.ECS
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class SynchronizePositionFromTransformSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<View>()
				.With<Position>()
				.With<Rotation>()
				.With<TransformUpdatesPosition>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref var view = ref entity.GetComponent<View>();

				ref Vector3 position = ref entity.GetComponent<Position>().Value;
				ref Vector3 eulerAngles = ref entity.GetComponent<Rotation>().Value;
				
				Transform transform = view.Reference.transform;
				position = transform.position;
				eulerAngles = transform.eulerAngles;
			}
		}
		
		public void Dispose() { }
	}
}