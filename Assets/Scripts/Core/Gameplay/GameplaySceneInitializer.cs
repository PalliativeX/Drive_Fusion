using Core.CameraLogic;
using Core.ECS;
using Core.InputLogic;
using Core.Levels;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Utils;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class GameplaySceneInitializer : IInitializer
	{
		private readonly LevelBehaviour _levelBehaviour;

		public World World { get; set; }

		public GameplaySceneInitializer(LevelBehaviour levelBehaviour)
		{
			_levelBehaviour = levelBehaviour;
		}

		public void OnAwake()
		{
			CreatePlayer(PlayerType.AI);
			Entity player = CreatePlayer(PlayerType.Human);

			CreateCameraTarget(player);
			CreateCamera();
		}


		public void Dispose() { }

		private Entity CreatePlayer(PlayerType type)
		{
			Entity player = World.CreateEntity();
			player.SetComponent(new PlayerTypeComponent { Value = type });
			player.AddPrefab("Player");

			var spawnPoint = _levelBehaviour.SpawnPoints.First(p => p.PlayerType == type).Reference;
			player.SetComponent(new Position { Value = spawnPoint.position });
			player.SetComponent(new Rotation { Value = spawnPoint.eulerAngles });
			
			player.SetComponent(new MovementInput { Value = Vector3.zero });

			if (type == PlayerType.Human)
				player.SetComponent(new HumanPlayer());

			return player;
		}

		private void CreateCameraTarget(Entity player)
		{
			Entity cameraTarget = World.CreateEntity();
			cameraTarget.AddPrefab("CameraTarget");

			cameraTarget.SetComponent(new CameraTarget());
			cameraTarget.SetComponent(new Link { Id = player.ID });
			cameraTarget.SetComponent(new Position { Value = Vector3.zero });
			cameraTarget.SetComponent(new Rotation { Value = Vector3.zero });
		}

		private void CreateCamera()
		{
			Entity cameraEntity = World.CreateEntity();
			cameraEntity.AddPrefab("Camera");
		}
	}
}