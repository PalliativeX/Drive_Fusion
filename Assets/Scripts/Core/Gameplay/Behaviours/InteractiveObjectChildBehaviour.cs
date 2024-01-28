using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class InteractiveObjectChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private InteractiveType _type;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new InteractiveTypeComponent { Value = _type });
		}

		public override void Unlink(Entity entity) { }
	}
}