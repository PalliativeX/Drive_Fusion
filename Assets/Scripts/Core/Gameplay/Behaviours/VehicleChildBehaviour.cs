using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.Gameplay.Behaviours
{
	public sealed class VehicleChildBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private VehicleConfig _config;
		[Header("Wheels")]
		[SerializeField] private WheelCollider _frontLeftWheel, _frontRightWheel;
		[SerializeField] private WheelCollider _rearLeftWheel, _rearRightWheel;
		[SerializeField] private Transform _frontLeftWheelMesh, _frontRightWheelMesh;
		[SerializeField] private Transform _rearLeftWheelMesh, _rearRightWheelMesh;
		
		[Header("Rigidbody")]
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _centerOfGravityOffset = -1f;
		
		public override void Link(Entity entity)
		{
			entity.SetComponent(new VehicleConfigComponent {Reference = _config});
			entity.SetComponent(new Wheels
			{
				FrontLeftWheel = _frontLeftWheel,
				FrontRightWheel = _frontRightWheel,
				RearLeftWheel = _rearLeftWheel,
				RearRightWheel = _rearRightWheel,
			});
			entity.SetComponent(new WheelMeshes
			{
				FrontLeftWheelMesh = _frontLeftWheelMesh,
				FrontRightWheelMesh = _frontRightWheelMesh,
				RearLeftWheelMesh = _rearLeftWheelMesh,
				RearRightWheelMesh = _rearRightWheelMesh,
			});
			
			_rigidbody.centerOfMass += Vector3.up * _centerOfGravityOffset;
		}

		public override void Unlink(Entity entity)
		{
			
		}
	}
}