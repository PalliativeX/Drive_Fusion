using Scellecs.Morpeh;

namespace Core.ECS
{
	public static class EntityExtensions
	{
		public static void AddPrefab(this Entity entity, string name) => 
			entity.SetComponent(new Prefab { Value = name });
	}
}