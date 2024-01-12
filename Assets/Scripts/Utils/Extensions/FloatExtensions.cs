using System;
using System.Runtime.CompilerServices;

namespace Utils
{
	public static class FloatExtensions
	{
		public const float PreciseFloatComparisonTolerance = 0.00001f;
		public const float Epsilon = 0.001f;

		public static bool IsEqualPrecise(this float f1, float f2) => 
			Math.Abs(f1 - f2) < PreciseFloatComparisonTolerance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt(this float value) => (int)value;

		// NOTE: E.g. a.AtLeast(0f) - if a is less than 0 then 0 is returned
		public static float AtLeast(this float a, float b) => a > b ? a : b;
		
		// NOTE: E.g. a.NotMoreThan(1f) - if a is more than 1 then 1 is returned
		public static float NotMoreThan(this float a, float b) => a < b ? a : b;
		
		public static bool NotEqual(this float value, float other, float epsilon = Epsilon) =>
			Math.Abs(value - other) > epsilon;
		
		public static bool IsZero(this float value, float epsilon = Epsilon) => Math.Abs(value) < epsilon;
		public static bool NotZero(this float value, float epsilon = Epsilon) => Math.Abs(value) > epsilon;
		
		public static float LinearRemap(this float value,
			float valueRangeMin, float valueRangeMax,
			float newRangeMin, float newRangeMax)
		{
			return (value - valueRangeMin) / (valueRangeMax - valueRangeMin) * (newRangeMax - newRangeMin) + newRangeMin;
		}
	}
}