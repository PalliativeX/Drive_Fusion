using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleInject
{
	public sealed class ContextHierarchy : Singleton<ContextHierarchy>
	{
		private List<AContext> _contexts;

		protected override void Awake()
		{
			base.Awake();
			_contexts = new List<AContext>();

			// NOTE: Calling to automatically initialize the ProjectContext
			var projectContext = ProjectContext.Instance;
		}

		public void AddContext(AContext context)
		{
			if (_contexts.Contains(context))
				throw new Exception($"Context '{context.name}' is already in the ContextHierarchy!");

			_contexts.Add(context);
		}

		public AContext GetParentContext(ContextScope scope)
		{
			switch (scope) {
				case ContextScope.Project:
					return null;
				case ContextScope.Local:
					return _contexts.First(c => c.Scope == ContextScope.Project);
				default:
					throw new ArgumentOutOfRangeException(nameof(scope), scope, null);
			}
		}

		public int ContextCount => _contexts.Count;
	}
}