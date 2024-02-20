using System.Collections.Generic;
using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class TriggerChildBehaviour : AChildEcsBehaviour
	{
		private Entity _entity;

		public override void Link(Entity entity)
		{
			_entity = entity;
			_entity.SetComponent(new ObjectsEnteredTrigger { List = new List<int>() });
			_entity.SetComponent(new ObjectsInTrigger { List = new List<int>() });
			_entity.SetComponent(new ObjectsExitedTrigger { List = new List<int>() });
		}

		public override void Unlink(Entity entity) => 
			_entity = null;

		private void OnTriggerEnter(Collider other)
		{
			ref var targets = ref _entity.GetComponent<ObjectsEnteredTrigger>();
			targets.List.Add(other.gameObject.GetInstanceID());
		}

		private void OnTriggerExit(Collider other)
		{
			ref var targets = ref _entity.GetComponent<ObjectsExitedTrigger>();
			targets.List.Add(other.gameObject.GetInstanceID());
		}
	}
}