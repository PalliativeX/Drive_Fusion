using System;

namespace SimpleInject
{
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class InjectScopeAttribute : Attribute
	{
		public ContextScope Scope;

		public InjectScopeAttribute(ContextScope scope)
		{
			Scope = scope;
		}
	}
}