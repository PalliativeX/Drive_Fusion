using System.Runtime.InteropServices;
using SimpleInject;
using UnityEngine;
using Utils;

namespace Core.Localization
{
	public sealed class LocalizationService : SimpleInject.Singleton<LocalizationService>, IInitializable
	{
		private LocaleStorage _locales;
		
		public Language CurrentLanguage { get; private set; }
		
		[Inject]
		public void Construct(LocaleStorage locales) => _locales = locales;

		public void Initialize()
		{
			SetLanguage();

#if UNITY_EDITOR
			Debug.Log($"Current language: {CurrentLanguage}");
#endif
		}
		
		public string GetText(string key) => _locales.GetText(key, CurrentLanguage);
		
		public string GetTextParsed(string key, string parameter)
		{
			string text = _locales.GetText(key, CurrentLanguage);
			return string.Format(text, parameter);
		}
		
		private void SetLanguage()
		{
			if (Platform.Instance.IsYandexGames())
				SetLanguageFromYandexSDK();
			else
				SetLanguageFromSystem();
		}

		private void SetLanguageFromYandexSDK()
		{
			string language = GetLanguageExternal();
			switch (language)
			{
				case "ru":
					CurrentLanguage = Language.Russian;
					break;
				case "en":
					CurrentLanguage = Language.English;
					break;
				default:
					CurrentLanguage = Language.English;
					break;
			}
		}

		private void SetLanguageFromSystem()
		{
#if UNITY_EDITOR
			CurrentLanguage = _locales.DebugLanguage;
			return;
#endif
			switch (Application.systemLanguage)
			{
				case SystemLanguage.Russian:
					CurrentLanguage = Language.Russian;
					break;
				default:
					CurrentLanguage = Language.English;
					break;
			}
		}
		
		[DllImport("__Internal")]
		private static extern string GetLanguageExternal();
	}
}