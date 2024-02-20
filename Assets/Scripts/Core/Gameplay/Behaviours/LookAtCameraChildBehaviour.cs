using Core.ECS;
using Scellecs.Morpeh;

namespace Core.Gameplay.Behaviours
{
	public sealed class LookAtCameraChildBehaviour : AChildEcsBehaviour
	{
		public override void Link(Entity entity)
		{
			entity.SetComponent(new LookAtCamera());
			entity.SetComponent(new TransformUpdatesPosition());
		}

		public override void Unlink(Entity entity) { }
	}
}