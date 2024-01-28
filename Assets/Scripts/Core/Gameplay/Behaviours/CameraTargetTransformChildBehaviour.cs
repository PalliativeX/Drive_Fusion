using Core.ECS;
using DavidJalbert;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class CameraTargetTransformChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Transform _target;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new CameraTargetTransform { Reference = _target });
		}

		public override void Unlink(Entity entity) { }
	}
}