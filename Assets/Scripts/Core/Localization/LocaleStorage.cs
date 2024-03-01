using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Localization
{
	[CreateAssetMenu(fileName = nameof(LocaleStorage), menuName = "Configs/" + nameof(LocaleStorage))]
	public sealed class LocaleStorage : ScriptableObject
	{
		public LocaleDictionary Locales;

		public Language DebugLanguage;
		
		public string GetText(string id, Language language)
		{
			List<LocaleEntry> entries = Locales[id].Locales;
			foreach (LocaleEntry localeEntry in entries)
				if (localeEntry.Language == language)
					return localeEntry.Text;

			throw new Exception($"Locale not found for id '{id}' and language '{language}'!");
		}
	}
}