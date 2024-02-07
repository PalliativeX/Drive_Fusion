using Scellecs.Morpeh;

namespace Core.ECS
{
	public class GlobalWorld
	{
		public readonly World World;

		public GlobalWorld(World world) => World = world;

		public static implicit operator World(GlobalWorld globalWorld) => globalWorld.World;
	}
}