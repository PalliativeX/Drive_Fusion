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

		public InteractiveItemsCreator(InteractiveItemsConfig interactiveItems, World world)
		{
			_interactiveItems = interactiveItems;
			_world = world;
		}

		public void Create(InteractiveType type, Vector3 position, Vector3 rotation)
		{
			InteractiveItemEntry interactiveEntry = _interactiveItems.Entries.First(e => e.Type == type);

			if (type != InteractiveType.Coins)
				CreateEntity(position, rotation, interactiveEntry);
			else
			{
				float currentYOffset = 0f;
				foreach (float offset in _interactiveItems.CoinOffsets)
				{
					var coin = CreateEntity(position + Vector3.forward * offset, rotation, interactiveEntry);
					coin.SetComponent(new Offset { Value = currentYOffset });
					currentYOffset += _interactiveItems.CoinYOffset;
				}
			}
		}

		public int GetSkipInitialBlockCount() => _interactiveItems.SkipInitialBlocksCount;

		private Entity CreateEntity(Vector3 position, Vector3 rotation, InteractiveItemEntry interactiveEntry) {
			Entity interactiveItem = _world.CreateEntity();
			interactiveItem.SetComponent(new Prefab { Value = interactiveEntry.PrefabName });
			interactiveItem.SetComponent(new Position { Value = position });
			interactiveItem.SetComponent(new Rotation { Value = rotation });
			
			return interactiveItem;
		}
	}
}