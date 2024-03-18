using UnityEngine;

namespace Utils
{
	public static class VectorExtensions
	{
		public static Vector3 ToVector3NoZ(this Vector2 vec2)
		{
			return new Vector3(vec2.x, vec2.y, 0f);
		}
		
		public static Vector3 ToVector3(this Vector2 vec2)
		{
			return new Vector3(vec2.x, 0f, vec2.y);
		}
		
		public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
		{
			return Vector3.Normalize(destination - source);
		}
		
		public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
		}
        
		public static Vector3 WithAdd(this Vector3 original, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(
				x + original.x ?? original.x, 
				y + original.y ?? original.y, 
				z + original.z ?? original.z
			);
		}
		
		public static float Random(this Vector2 vector) => 
			UnityEngine.Random.Range(vector.x, vector.y);

		public static int Random(this Vector2Int vector) => 
			UnityEngine.Random.Range(vector.x, vector.y + 1);

		public static float Lerp(this Vector2 vector, float t) =>
			Mathf.Lerp(vector.x, vector.y, t);
	}
}