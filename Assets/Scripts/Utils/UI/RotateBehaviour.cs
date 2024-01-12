using UnityEngine;

namespace Utils
{
	public sealed class RotateBehaviour : MonoBehaviour
	{
		[SerializeField] private float _rotationSpeed;

		private void Update()
		{
			transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
		}
	}
}