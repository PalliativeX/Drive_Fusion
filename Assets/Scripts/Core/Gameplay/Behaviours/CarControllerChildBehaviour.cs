using Core.ECS;
using DavidJalbert;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class CarControllerChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private TinyCarController _controller;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new CarController { Reference = _controller });
		}

		public override void Unlink(Entity entity) { }
	}
}