using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Utils;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class RoadBlockCreatedSystem : ISystem
	{
		private readonly InteractiveItemsCreator _itemsCreator;
		
		private Filter _filter;
		private int _skipInitialBlockCount;
		
		public World World { get; set; }

		public RoadBlockCreatedSystem(InteractiveItemsCreator itemsCreator)
		{
			_itemsCreator = itemsCreator;
		}

		public void OnAwake()
		{
			_filter = World.Filter
				.With<RoadBlock>()
				.Without<Initialized>()
				.Build();

			_skipInitialBlockCount = _itemsCreator.GetSkipInitialBlockCount();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				entity.SetComponent(new Initialized());

				if (!entity.Has<ObjectPositions>())
					continue;

				if (_skipInitialBlockCount > 0)
				{
					_skipInitialBlockCount--;
					continue;
				}
				
				// TODO: Rework
				if (Random.value > 0.5f)
					continue;

				Transform[] positions = entity.GetComponent<ObjectPositions>().List;
				Transform randomPosition = positions.GetRandom();

				_itemsCreator.Create(
					(InteractiveType) Random.Range(0, 3), randomPosition.position, randomPosition.eulerAngles
				);
			}
		}

		public void Dispose() { }
	}
}