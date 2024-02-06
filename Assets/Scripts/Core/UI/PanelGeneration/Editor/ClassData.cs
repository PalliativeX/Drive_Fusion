using System.Collections.Generic;

namespace Core.UI.PanelGeneration
{
	public sealed class ClassData
	{
		public readonly string ClassName;
		public readonly string[] ParentClasses;
		public readonly string NamespaceValue;
		public readonly List<string> Usings;
		public readonly string Body;

		public ClassData(string className, string[] parentClasses, string namespaceValue, List<string> usings, string body)
		{
			ClassName = className;
			ParentClasses = parentClasses;
			NamespaceValue = namespaceValue;
			Usings = usings;
			Body = body;
		}
	}
}