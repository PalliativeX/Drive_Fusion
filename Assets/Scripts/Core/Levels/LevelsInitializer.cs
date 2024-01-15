using Scellecs.Morpeh;

namespace Core.Levels
{
	public class LevelsInitializer : IInitializer
	{
		public World World { get; set; }

		public void OnAwake()
		{
			var entity = World.CreateEntity();
			entity.SetComponent(new CurrentLevel { Value = 1 });
			entity.SetComponent(new LevelsPassed { Value = 0 });
		}

		public void Dispose() { }
	}
}