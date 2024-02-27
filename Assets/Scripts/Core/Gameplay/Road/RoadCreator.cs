using System.Collections.Generic;
using System.Linq;
using Core.ECS;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay
{
	public class RoadCreator : IInitializer
	{
		private readonly RoadsConfig _roads;

		private Filter _roadFilter;

		public World World { get; set; }

		public RoadCreator(RoadsConfig roads)
		{
			_roads = roads;
		}

		public void OnAwake() => Initialize();

		private async UniTaskVoid Initialize()
		{
			_roadFilter = World.Filter.With<Road>().Build();
			
			Entity road = World.CreateEntity();
			road.SetComponent(new Road { Blocks = new LinkedList<EntityId>() });

			await UniTask.Yield();

			for (int i = 0; i < _roads.ConcurrentBlockCount; i++) 
				CreateNextBlock();
		}

		public void CreateNextBlock()
		{
			Entity roadEntity = _roadFilter.First();
			ref var road = ref roadEntity.GetComponent<Road>();

			Vector3 blockPosition = Vector3.zero;

			if (road.Blocks.Count > 0)
			{
				var lastBlockId = road.Blocks.Last.Value;
				if (World.TryGetEntity(lastBlockId, out Entity lastBlock))
					blockPosition = lastBlock.GetComponent<Position>().Value + Vector3.forward * _roads.BlockSize;
			}


			Entity newBlock = World.CreateEntity();
			newBlock.SetComponent(new Prefab { Value = _roads.Blocks.First().PrefabName }); // TODO: Add randomness
			newBlock.SetComponent(new Position { Value = blockPosition });

			road.Blocks.AddLast(newBlock.ID);
		}

		public void RemoveFirstBlock()
		{
			Entity roadEntity = _roadFilter.First();
			ref var road = ref roadEntity.GetComponent<Road>();

			EntityId blockId = road.Blocks.First.Value;
			World.TryGetEntity(blockId, out Entity block);
			block.SetComponent(new Destroyed());
			
			road.Blocks.RemoveFirst();
		}

		public bool HandleRoadCreation(Entity roadBlock)
		{
			Entity roadEntity = _roadFilter.First();
			ref var road = ref roadEntity.GetComponent<Road>();

			var blocks = road.Blocks;
			if (blocks.First.Value == roadBlock.ID)
				return false;
			
			RemoveFirstBlock();
			CreateNextBlock();
			return true;
		}

		public void Dispose() { }
	}
}