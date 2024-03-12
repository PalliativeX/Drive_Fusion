using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class RoadBlockChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private RoadBlockType _type;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new RoadBlock { Type = _type });
		}

		public override void Unlink(Entity entity) { }
	}
}