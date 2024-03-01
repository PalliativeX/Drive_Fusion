using System;
using System.Collections.Generic;

namespace Core.Localization
{
	[Serializable]
	public class LocaleEntries
	{
		public List<LocaleEntry> Locales = new List<LocaleEntry>
		{
			new LocaleEntry(Language.English, ""),
			new LocaleEntry(Language.Russian, "")
		};
		
		public LocaleEntries() { }

		public LocaleEntries(List<LocaleEntry> locales)
		{
			Locales = locales;
		}
	}
}