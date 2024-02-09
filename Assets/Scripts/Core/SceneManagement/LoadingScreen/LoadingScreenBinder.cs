namespace Core.SceneManagement.LoadingScreen
{
	public class LoadingScreenBinder
	{
		public LoadingScreenBinder(LoadingScreenBehaviour loadingScreen, LoadingScreenProvider provider) => 
			provider.Register(loadingScreen);
	}
}