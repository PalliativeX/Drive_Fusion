using System;

namespace SimpleInject
{
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class InjectAttribute : Attribute
	{
		
	}
}