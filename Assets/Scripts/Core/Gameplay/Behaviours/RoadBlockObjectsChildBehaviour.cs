using System.Collections.Generic;
using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class RoadBlockObjectsChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private List<GameObject> _leftObjectsContainers;
		[SerializeField] private List<GameObject> _rightObjectsContainers;
		
		public override void Link(Entity entity)
		{
			ActivateRandom(_leftObjectsContainers);
			ActivateRandom(_rightObjectsContainers);
		}

		public override void Unlink(Entity entity) { }

		private void ActivateRandom(List<GameObject> containers)
		{
			if (containers.Count == 0)
				return;
			
			int randomIndex = Random.Range(0, containers.Count);
			for (int i = 0; i < containers.Count; i++)
			{
				bool active = i == randomIndex;
				if (containers[i].activeSelf != active)
					containers[i].SetActive(active);
			}
		}
	}
}