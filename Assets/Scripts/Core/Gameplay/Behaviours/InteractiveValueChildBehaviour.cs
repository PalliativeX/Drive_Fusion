using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class InteractiveValueChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private float _amount;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new InteractiveValue { Value = _amount });
		}

		public override void Unlink(Entity entity) { }
	}
}