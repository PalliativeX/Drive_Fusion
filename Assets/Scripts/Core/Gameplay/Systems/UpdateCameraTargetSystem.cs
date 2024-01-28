using Core.CameraLogic;
using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class UpdateCameraTargetSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<View>()
				.With<CameraTarget>()
				.With<Position>()
				.With<Rotation>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				EntityId link = entity.GetComponent<Link>().Id;
				World.TryGetEntity(link, out Entity player);
				
				ref var position = ref entity.GetComponent<Position>();
				position.Value = player.GetComponent<Position>().Value;
				
				ref var rotation = ref entity.GetComponent<Rotation>();
				rotation.Value = player.GetComponent<Rotation>().Value;
			}
		}
		
		public void Dispose() { }
	}
}