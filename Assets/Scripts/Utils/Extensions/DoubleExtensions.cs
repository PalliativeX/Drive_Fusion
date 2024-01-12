using System;
using System.Runtime.CompilerServices;

namespace Utils
{
	public static class DoubleExtensions
	{
		public const double PreciseDoubleComparisonTolerance = 0.00001f;
		public const double Epsilon = 0.001f;

		public static bool IsEqualPrecise(this double f1, double f2) => 
			Math.Abs(f1 - f2) < PreciseDoubleComparisonTolerance;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt(this double value) => (int)value;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ToFloat(this double value) => (float)value;

		// NOTE: E.g. a.AtLeast(0f) - if a is less than 0 then 0 is returned
		public static double AtLeast(this double a, double b) => a > b ? a : b;
		
		// NOTE: E.g. a.NotMoreThan(1f) - if a is more than 1 then 1 is returned
		public static double NotMoreThan(this double a, double b) => a < b ? a : b;
		
		public static bool NotEqual(this double value, double other, double epsilon = Epsilon) =>
			Math.Abs(value - other) > epsilon;
		
		public static bool IsZero(this double value, double epsilon = Epsilon) => Math.Abs(value) < epsilon;
		public static bool NotZero(this double value, double epsilon = Epsilon) => Math.Abs(value) > epsilon;
	}
}