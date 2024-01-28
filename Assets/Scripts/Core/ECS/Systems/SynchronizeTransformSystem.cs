using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.ECS
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class SynchronizeTransformSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<View>()
				.With<Position>()
				.With<Rotation>()
				.Without<TransformUpdatesPosition>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref var view = ref entity.GetComponent<View>();

				Vector3 position = entity.GetComponent<Position>().Value;
				Vector3 eulerAngles = entity.GetComponent<Rotation>().Value;

				Transform transformAspect = view.Reference.transform;
				if (transformAspect.position != position)
					transformAspect.position = position;

				if (transformAspect.eulerAngles != eulerAngles)
					transformAspect.eulerAngles = eulerAngles;
			}
		}
		
		public void Dispose() { }
	}
}