using Core.CameraLogic;
using Core.ECS;
using Core.InputLogic;
using Core.Levels;
using Core.Menu;
using Cysharp.Threading.Tasks;
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
		private readonly VehicleSelectionService _vehicleSelectionService;
		private readonly GameplayParametersProvider _gameplayParameters;
		private readonly RoadCreator _roadCreator;

		public World World { get; set; }

		public GameplaySceneInitializer(
			LevelBehaviour levelBehaviour,
			VehicleSelectionService vehicleSelectionService,
			GameplayParametersProvider gameplayParameters,
			RoadCreator roadCreator
		)
		{
			_levelBehaviour = levelBehaviour;
			_vehicleSelectionService = vehicleSelectionService;
			_gameplayParameters = gameplayParameters;
			_roadCreator = roadCreator;
		}

		public void OnAwake() => Initialize();

		private async UniTaskVoid Initialize()
		{
			await _roadCreator.Initialize();
			
			Entity player = CreatePlayer();

			// CreateCameraTarget(player);
			CreateCamera();

			// TestInteractive();
		}

		public void Dispose() { }

		private Entity CreatePlayer()
		{
			Entity player = World.CreateEntity();
			player.SetComponent(new HumanPlayer());
			player.AddPrefab(_vehicleSelectionService.GetSelectedName());

			Transform spawnPoint = _levelBehaviour.SpawnPoint;
			player.SetComponent(new Position { Value = spawnPoint.position });
			player.SetComponent(new Rotation { Value = spawnPoint.eulerAngles });
			player.SetComponent(new TransformUpdatesPosition());
			player.SetComponent(new TrackPositionChange());

			player.SetComponent(new MovementInput { Value = Vector3.zero });

			player.SetComponent(new CameraTarget());

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

		// private void TestInteractive() {
		// 	// TODO: Test, delete
		// 	for (int i = 0; i < 4; i++) {
		// 		Entity coin = World.CreateEntity();
		// 		coin.AddPrefab("Coin");
		// 		coin.SetComponent(new Position { Value = new Vector3(i * 2, 0f, i * 2) });
		// 		coin.SetComponent(new Rotation { Value = Vector3.zero });
		// 	}
		//
		// 	for (int i = 0; i < 4; i++) {
		// 		Entity coin = World.CreateEntity();
		// 		coin.AddPrefab("Fuel");
		// 		coin.SetComponent(new Position { Value = new Vector3(5 + i * 5, 0f, 0f) });
		// 		coin.SetComponent(new Rotation { Value = Vector3.zero });
		// 	}
		// }
	}
}