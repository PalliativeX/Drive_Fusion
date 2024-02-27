using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class CustomTransformChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Transform _target;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new CustomTransform { Reference = _target });
		}

		public override void Unlink(Entity entity) { }
	}
}