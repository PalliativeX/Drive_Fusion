using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(VehicleConfig), menuName = "Configs/" + nameof(VehicleConfig))]
	public sealed class VehicleConfig : ScriptableObject
	{
		public float MotorForce;
		public float SteeringMotorForce;
		
		public float MaxSteeringAngle;
	}
}