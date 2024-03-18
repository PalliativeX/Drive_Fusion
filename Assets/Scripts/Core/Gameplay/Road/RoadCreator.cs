using System.Collections.Generic;
using Core.ECS;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.Pool;
using Utils;

namespace Core.Gameplay
{
	public class RoadCreator : IInitializer
	{
		private readonly RoadsConfig _roads;

		private Filter _roadFilter;

		private int _turnGenerationPause;
		private int _fuelStationGenerationPause;
		private int _carRepairGenerationPause;

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
			road.SetComponent(new RoadsDirection { Value = Vector3.forward});

			await UniTask.Yield();

			for (int i = 0; i < _roads.ConcurrentBlockCount; i++) 
				CreateNextBlock(true);
		}

		public void CreateNextBlock(bool isStraightRoad)
		{
			Entity roadEntity = _roadFilter.First();
			ref var road = ref roadEntity.GetComponent<Road>();

			Vector3 blockPosition = Vector3.zero;

			ref var roadsDirection = ref roadEntity.GetComponent<RoadsDirection>();

			RoadDirection lastBlockDirection = RoadDirection.Straight;
			Vector3 newForward = roadsDirection.Value;

			if (road.Blocks.Count > 0)
			{
				var lastBlockId = road.Blocks.Last.Value;
				if (World.TryGetEntity(lastBlockId, out Entity lastBlock))
				{
					blockPosition = lastBlock.GetComponent<Position>().Value +
					                roadEntity.GetComponent<RoadsDirection>().Value * 
					                _roads.Blocks.First(b => b.Type == lastBlock.GetComponent<RoadBlock>().Type).BlockSize;
					Debug.Log(_roads.Blocks.First(b => b.Type == lastBlock.GetComponent<RoadBlock>().Type).BlockSize);
					Debug.Log(_roads.Blocks.First(b => b.Type == lastBlock.GetComponent<RoadBlock>().Type));
					lastBlockDirection = lastBlock.GetComponent<RoadBlockDirection>().Value;
				}
			}

			var entry = GetRoadBlockEntry(isStraightRoad, lastBlockDirection);

			Entity newBlock = World.CreateEntity();
			newBlock.SetComponent(new Prefab { Value = entry.PrefabName });
			newBlock.SetComponent(new Position { Value = blockPosition });
			newBlock.SetComponent(new Rotation { Value = Quaternion.LookRotation(newForward).eulerAngles });

			if (entry.Direction != RoadDirection.Straight)
			{
				_turnGenerationPause = _roads.TurnGenerationInterval.Random();
				
				if (entry.Direction == RoadDirection.Left)
					roadsDirection.Value = RotateByDegrees(newForward, -90f);
				else if (entry.Direction == RoadDirection.Right)
					roadsDirection.Value = RotateByDegrees(newForward, 90f);
			}
			else if (entry.Type == RoadBlockType.CarFixRoadLeft || entry.Type == RoadBlockType.CarFixRoadRight)
			{
				_carRepairGenerationPause = _roads.CarRepairGenerationInterval.Random();
			}
			else if (entry.Type == RoadBlockType.FuelStationRoadLeft || entry.Type == RoadBlockType.FuelStationRoadRight)
			{
				_fuelStationGenerationPause = _roads.FuelStationGenerationInterval.Random();
			}

			newBlock.SetComponent(new RoadBlockDirection { Value = entry.Direction, Forward = roadsDirection.Value });
			newBlock.SetComponent(new RoadBlock { Type = entry.Type });
			
			if (lastBlockDirection != RoadDirection.Straight)
				newBlock.SetComponent(new IsAfterTurn());
			
			road.Blocks.AddLast(newBlock.ID);

			if (!isStraightRoad)
				_turnGenerationPause--;

			_carRepairGenerationPause--;
			_fuelStationGenerationPause--;
		}

		private RoadBlockEntry GetRoadBlockEntry(bool isStraightRoad, RoadDirection lastBlockDirection)
		{
			if (isStraightRoad)
				return _roads.Blocks[0];

			RoadBlockType type = _roads.StraightBlocks.GetRandom();
			if (_carRepairGenerationPause <= 0)
				type = Random.value < 0.5f ? RoadBlockType.CarFixRoadLeft : RoadBlockType.CarFixRoadRight;
			else if (_fuelStationGenerationPause <= 0)
				type = Random.value < 0.5f ? RoadBlockType.FuelStationRoadLeft : RoadBlockType.FuelStationRoadRight;
			else if (lastBlockDirection == RoadDirection.Straight && _turnGenerationPause <= 0 && Random.value < 0.5f)
				type = Random.value < 0.5f ? RoadBlockType.TurnLeft : RoadBlockType.TurnRight;

			return _roads.Blocks.First(b => b.Type == type);
		}

		public void RemoveFirstBlock()
		{
			Entity roadEntity = _roadFilter.First();
			ref var road = ref roadEntity.GetComponent<Road>();

			EntityId blockId = road.Blocks.First.Value;
			World.TryGetEntity(blockId, out Entity block);
			block.SetComponent(new Destroyed());
			
			road.Blocks.RemoveFirst();

			DestroyBlockObjects(blockId);
		}

		public void DestroyBlockObjects(EntityId blockId) {
			var pool = ListPool<Entity>.Get();

			World.FillEntitiesWithLink(blockId, ref pool);
			foreach (var entity in pool) 
				entity.SetComponent(new Destroyed());

			ListPool<Entity>.Release(pool);
		}

		public bool HandleRoadCreation(Entity roadBlock)
		{
			Entity roadEntity = _roadFilter.First();
			ref var road = ref roadEntity.GetComponent<Road>();

			var blocks = road.Blocks;
			if (blocks.First.Value == roadBlock.ID || (blocks.First.Next != null && blocks.First.Next.Value == roadBlock.ID))
				return false;
			
			RemoveFirstBlock();
			CreateNextBlock(false);
			return true;
		}

		public Vector3 GetRoadsDirection() => 
			_roadFilter.First().GetComponent<RoadsDirection>().Value;

		private Vector3 RotateByDegrees(Vector3 vector, float angleDegrees)
		{
			Quaternion rotation = Quaternion.AngleAxis(angleDegrees, Vector3.up);
			return rotation * vector;
		}

		public void Dispose() { }
	}
}