using System;

namespace Core.Localization
{
	[Serializable]
	public class LocaleEntry
	{
		public Language Language;
		public string Text;

		public LocaleEntry(Language language, string text)
		{
			Language = language;
			Text = text;
		}

		public LocaleEntry() { }
	}
}