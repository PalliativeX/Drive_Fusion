using System.Text;

namespace Core.UI.PanelGeneration
{
	public sealed class StringBuilderGenerator
	{
		public const string IndentStep = "	";
		
		public string CurrentIndent = "";

		public readonly StringBuilder Builder;

		public StringBuilderGenerator() => 
			Builder = new StringBuilder();

		public StringBuilderGenerator AddTab()
		{
			CurrentIndent += IndentStep;
			return this;
		}

		public StringBuilderGenerator RemoveTab()
		{
			if (CurrentIndent.Length >= 1)
				CurrentIndent = CurrentIndent.Substring(0, CurrentIndent.Length - 1);
			return this;
		}

		public StringBuilderGenerator Append(string value)
		{
			Builder.Append(CurrentIndent);
			Builder.Append(value);
			return this;
		}
		
		public StringBuilderGenerator NewLine()
		{
			Builder.AppendLine();
			return this;
		}

		public StringBuilderGenerator AppendBody(string value)
		{
			StringBuilder body = new StringBuilder();
			body.Append(CurrentIndent);
			for (int i = 0; i < value.Length; i++)
			{
				body.Append(value[i]);
				if (value[i] == '\n' && i != value.Length-1) 
					body.Append(CurrentIndent);
			}

			Builder.Append(body);
			
			return this;
		}

		public StringBuilderGenerator OpenBrace()
		{
			return Append("{")
				.NewLine()
				.AddTab();
		}
		
		public StringBuilderGenerator CloseBrace()
		{
			return RemoveTab()
				.Append("}")
				.NewLine();
		}

		public override string ToString() => Builder.ToString();
	}
}