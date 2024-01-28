using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class InteractiveCoinsChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private float _amount;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new InteractiveCoins { Value = _amount });
		}

		public override void Unlink(Entity entity) { }
	}
}