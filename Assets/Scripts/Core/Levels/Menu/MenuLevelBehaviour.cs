using UnityEngine;

namespace Core.Levels
{
	public sealed class MenuLevelBehaviour : MonoBehaviour
	{
		[SerializeField] private Transform _vehicleSpawnPoint;

		public Transform VehicleSpawnPoint => _vehicleSpawnPoint;
	}
}