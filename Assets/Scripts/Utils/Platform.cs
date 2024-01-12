namespace Utils
{
	public static class Platform
	{
		// TODO
		public static bool IsYandexGames()
		{
// #if !UNITY_EDITOR && UNITY_WEBGL
			// return !SettingsController.Instance.IsTestBuild;
// #else
			return false;
// #endif
		}
	}
}