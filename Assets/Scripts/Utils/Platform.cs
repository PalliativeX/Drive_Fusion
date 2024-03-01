using Core;
using UnityEngine;

namespace Utils
{
	public sealed class Platform : Singleton<Platform>
	{
		[SerializeField] private GeneralSettings _settings; 
		
		public bool IsYandexGames()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
			return _settings.IsYandexGames;
#else
			return false;
#endif
		}
	}
}