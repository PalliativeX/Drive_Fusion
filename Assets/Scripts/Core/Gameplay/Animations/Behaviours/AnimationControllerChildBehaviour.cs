using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Animations
{
	public sealed class AnimationControllerChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private AnimationController _controller;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new AnimatorComponent { Reference = _controller });
		}

		public override void Unlink(Entity entity) { }
	}
}