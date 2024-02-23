using System.Collections.Generic;
using Core.ECS;
using DavidJalbert;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class VehicleCollisionsChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private TinyCarController _carController;
		
		private Entity _entity;

		public override void Link(Entity entity)
		{
			_entity = entity;
			_entity.SetComponent(new CollisionEnterEvents { List = new List<CollisionEventData>() });
			
			_carController.OnCollision += OnCollision;
		}

		public override void Unlink(Entity entity)
		{
			_entity = null;
			
			_carController.OnCollision -= OnCollision;
		}

		private void OnCollision(CollisionEventData data)
		{
			if (_entity == null)
				return;
			
			ref var collisionEvents = ref _entity.GetComponent<CollisionEnterEvents>();
			collisionEvents.List.Add(data);
		}
	}
}