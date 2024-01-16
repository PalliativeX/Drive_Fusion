using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.ECS
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class DestroyEntitySystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter.With<Destroyed>().Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				if (entity.Has<View>())
				{
					GameObject obj = entity.GetComponent<View>().Reference;
					var baseBehaviour = obj.GetComponent<BaseEcsBehaviour>();
					if (baseBehaviour)
						baseBehaviour.Unlink();
				}

				World.RemoveEntity(entity);
			}
		}
		
		public void Dispose() { }
	}
}