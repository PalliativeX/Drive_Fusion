using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
	public static class Util
	{
		public static Vector3 Bezier(Vector3 s, Vector3 p, Vector3 e, float t)
		{
			float rt = 1f-t;
			return rt*rt * s + 2f * rt * t * p + t*t* e;
		}

		public static Vector3 CalculateCubicBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float u = 1f-t;
			float tt = t*t;
			float uu = u*u;
			float uuu = uu * u;
			float ttt = tt * t;
            
			Vector3 p = uuu * p0; //first term
			p += 3 * uu * t * p1; //second term
			p += 3 * u * tt * p2; //third term
			p += ttt * p3;   //fourth term
            
			return p;
		}
		
		public static IEnumerable<T> GetEnumValues<T>() where T : Enum => 
			Enum.GetValues(typeof(T)).Cast<T>();
	}
}