using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;
using Utils;

namespace Core.Gameplay.Behaviours
{
	public sealed class InteractiveVehicleChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Vector2 _speedRange;
		[SerializeField] private Transform[] _wheelModels;
		[SerializeField] private float _wheelRotationSpeed;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new InteractiveVehicleSpeed { Value = _speedRange.Random() });
		}

		public override void Unlink(Entity entity) { }

		// private void Update()
		// {
		// 	float wheelRotationSpeed = _wheelRotationSpeed * Time.deltaTime;
		// 	foreach (Transform wheel in _wheelModels)
		// 		wheel.Rotate(Vector3.right * wheelRotationSpeed, Space.Self);
		// }
	}
}