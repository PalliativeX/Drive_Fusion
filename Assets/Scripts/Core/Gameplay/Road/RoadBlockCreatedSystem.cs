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
		private readonly RoadCreator _roadCreator;

		private Filter _filter;
		private int _skipInitialBlockCount;
		
		public World World { get; set; }

		public RoadBlockCreatedSystem(InteractiveItemsCreator itemsCreator, RoadCreator roadCreator)
		{
			_itemsCreator = itemsCreator;
			_roadCreator = roadCreator;
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
				var interactiveType = (InteractiveType) Random.Range(0, 4);

				int randomPositionIndex;
				do
					randomPositionIndex = Random.Range(0, positions.Length);
				while ((interactiveType == InteractiveType.Obstacle || interactiveType == InteractiveType.Vehicle) &&
				       randomPositionIndex == 0);
				
				Transform randomPosition = positions[randomPositionIndex];

				_itemsCreator.Create(
					interactiveType, randomPosition.position, randomPosition.eulerAngles, entity.ID,
					_roadCreator.GetRoadsDirection()
				);
			}
		}

		public void Dispose() { }
	}
}