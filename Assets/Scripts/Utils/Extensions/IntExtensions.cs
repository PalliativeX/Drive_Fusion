namespace Utils
{
	public static class IntExtensions
	{
		// NOTE: E.g. a.AtLeast(0f) - if a is less than 0 then 0 is returned
		public static int AtLeast(this int a, int b) => a > b ? a : b;
		
		// NOTE: E.g. a.NotMoreThan(1f) - if a is more than 1 then 1 is returned
		public static int NotMoreThan(this int a, int b) => a < b ? a : b;
	}
}