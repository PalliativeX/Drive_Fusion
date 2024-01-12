using UnityEngine;

namespace Utils
{
	public static class GameObjectExtensions
	{
		public static void SetActive(this Behaviour target, bool isActive) {
			if (target.gameObject.activeSelf != isActive)
				target.gameObject.SetActive(isActive);
		}
	}
}