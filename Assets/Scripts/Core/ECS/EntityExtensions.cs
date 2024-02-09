using Core.Gameplay;
using Scellecs.Morpeh;

namespace Core.ECS
{
	public static class EntityExtensions
	{
		public static void TryRemove<T>(this Entity entity) where T : struct, IComponent
		{
			if (entity.Has<T>())
				entity.RemoveComponent<T>();
		}

		public static Entity GetPlayer(this World world) => 
			world.Filter.With<HumanPlayer>().Build().First();

		public static void AddPrefab(this Entity entity, string name) => 
			entity.SetComponent(new Prefab { Value = name });
	}
}