using UnityEngine;

namespace Utils
{
	public static class VectorUtils
	{
		public static Vector2 ToVector2(this Vector3 vec) => new Vector2(vec.x, vec.y);
	}
}