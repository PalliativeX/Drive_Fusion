using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class RoadBlockObjectPositionsChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Transform[] _objectPositions;
		
		public override void Link(Entity entity)
		{
			entity.SetComponent(new ObjectPositions { List = _objectPositions });
		}

		public override void Unlink(Entity entity) { }
	}
}