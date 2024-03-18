using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;
using Utils;

namespace Core.Gameplay
{
	public class InteractiveItemsCreator
	{
		private readonly InteractiveItemsConfig _interactiveItems;
		private readonly World _world;
		private readonly CurrentLevelService _currentLevel;

		public InteractiveItemsCreator(InteractiveItemsConfig interactiveItems, World world, CurrentLevelService currentLevel)
		{
			_interactiveItems = interactiveItems;
			_world = world;
			_currentLevel = currentLevel;
		}

		public void CreateRandom(InteractiveType type, Vector3 position, Vector3 rotation, EntityId roadBlockId, Vector3 roadForward)
		{
			InteractiveItemEntry interactiveEntry = _interactiveItems.Entries.First(e => e.Type == type);

			if (type != InteractiveType.Coins)
			{
				Vector3 endRotation = type == InteractiveType.Vehicle ? Quaternion.LookRotation(-roadForward).eulerAngles : rotation;
				var entity = CreateEntity(position, endRotation, interactiveEntry, roadBlockId);
				return;
			}

			float currentYOffset = 0f;
			foreach (float offset in _interactiveItems.CoinOffsets)
			{
				var coin = CreateEntity(position + roadForward * offset, rotation, interactiveEntry, roadBlockId);
				coin.SetComponent(new Offset { Value = currentYOffset });
				currentYOffset += _interactiveItems.CoinYOffset;
			}
		}

		public InteractiveType GetRandomReward() => _interactiveItems.RewardTypes.GetRandom();
		public InteractiveType GetRandomObstacle() => _interactiveItems.ObstacleTypes.GetRandom();

		public int GetSkipInitialBlockCount() => _interactiveItems.SkipInitialBlocksCount;

		public bool ShouldCreateReward()
		{
			float difficultyPercent = _currentLevel.GetDifficultyPercent();
			return Random.value <= _interactiveItems.RewardCreationChance.Lerp(difficultyPercent);
		}

		public bool ShouldCreateObstacle()
		{
			float difficultyPercent = _currentLevel.GetDifficultyPercent();
			return Random.value <= _interactiveItems.ObstacleGenerationChance.Lerp(difficultyPercent);
		}

		private Entity CreateEntity(Vector3 position, Vector3 rotation, InteractiveItemEntry interactiveEntry, EntityId roadBlockId) {
			Entity interactiveItem = _world.CreateEntity();
			interactiveItem.SetComponent(new Prefab { Value = interactiveEntry.PrefabNames.GetRandom() });
			interactiveItem.SetComponent(new Position { Value = position });
			interactiveItem.SetComponent(new Rotation { Value = rotation });
			
			interactiveItem.SetComponent(new Link { Id = roadBlockId });
			
			return interactiveItem;
		}
	}
}