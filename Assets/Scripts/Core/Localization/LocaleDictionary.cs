using System;

namespace Core.Localization
{
	[Serializable]
	public class LocaleDictionary : SerializableDictionary<string, LocaleEntries> { }
}