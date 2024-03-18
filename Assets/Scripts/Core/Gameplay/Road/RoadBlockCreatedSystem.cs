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

				var roadBlockType = entity.GetComponent<RoadBlock>().Type;
				var isBoostBlock = roadBlockType != RoadBlockType.Straight && roadBlockType != RoadBlockType.TurnLeft &&
				                   roadBlockType != RoadBlockType.TurnRight;
				
				bool shouldCreateObstacle = _itemsCreator.ShouldCreateObstacle();
				bool shouldCreateReward = _itemsCreator.ShouldCreateReward();
				if (!shouldCreateObstacle && !shouldCreateReward && !isBoostBlock)
					continue;
				
				Transform[] positions = entity.GetComponent<ObjectPositions>().List;

				if (isBoostBlock)
				{
					Transform randomPosition = GetRandomPosition(positions);

					InteractiveType type;
					if (roadBlockType == RoadBlockType.CarFixRoadLeft || roadBlockType == RoadBlockType.CarFixRoadRight)
						type = InteractiveType.Repair;
					else
						type = InteractiveType.Fuel;

					Create(randomPosition, type, entity.ID);
					
					continue;
				}

				int ignoredPositionIndex = -1;

				if (shouldCreateReward)
				{
					Transform randomPosition = GetRandomPosition(positions);
					ignoredPositionIndex = positions.IndexOf(randomPosition);
					Create(randomPosition, _itemsCreator.GetRandomReward(), entity.ID);
				}

				if (shouldCreateObstacle && !entity.Has<IsAfterTurn>())
				{
					Transform randomPosition = GetRandomPosition(positions, ignoredPositionIndex);
					Create(randomPosition, _itemsCreator.GetRandomObstacle(), entity.ID);
				}
			}
		}

		private static Transform GetRandomPosition(Transform[] positions, int ignoredPositionIndex = -1)
		{
			return ignoredPositionIndex == -1 
				? positions.GetRandom() 
				: positions.GetRandomExcept(ignoredPositionIndex);
		}

		private void Create(Transform randomPosition, InteractiveType type, EntityId entityId)
		{
			_itemsCreator.CreateRandom(
				type, randomPosition.position, randomPosition.eulerAngles, entityId,
				_roadCreator.GetRoadsDirection()
			);
		}

		public void Dispose() { }
	}
}