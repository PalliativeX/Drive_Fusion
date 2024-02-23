using Core.ECS;
using Scellecs.Morpeh;

namespace Core.Gameplay
{
	public class RemoveDurabilityChangedSystem : ASingleSystem<DurabilityChanged>
	{
		protected override void OnUpdate(float deltaTime, Entity entity) => 
			entity.RemoveComponent<DurabilityChanged>();
	}
}