using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(VehicleConfig), menuName = "Configs/" + nameof(VehicleConfig))]
	public sealed class VehicleConfig : ScriptableObject
	{
		public string Name;
		
		[Range(0f, 1f)]
		public float FuelConsumptionPerSecond;
	}
}