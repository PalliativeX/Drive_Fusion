using Core.CameraLogic;
using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class GameplaySceneInitializer : IInitializer
	{
		public World World { get; set; }

		public void OnAwake()
		{
			CreatePlayer(EPlayerType.Human);
			CreatePlayer(EPlayerType.AI);

			CreateCamera();
		}

		public void Dispose() { }

		private void CreatePlayer(EPlayerType type)
		{
			Entity player = World.CreateEntity();
			player.SetComponent(new PlayerType { Value = type });
			player.AddPrefab("Player");
			
			if (type == EPlayerType.Human)
				player.SetComponent(new CameraTarget());
		}

		private void CreateCamera()
		{
			Entity cameraEntity = World.CreateEntity();
			cameraEntity.AddPrefab("Camera");
		}
	}
}