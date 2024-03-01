using UnityEditor;

namespace Core.Localization
{
	[CustomPropertyDrawer(typeof(LocaleDictionary))]
	public class LocaleDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
}