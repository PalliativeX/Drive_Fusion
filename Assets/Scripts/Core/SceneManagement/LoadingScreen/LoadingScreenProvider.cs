namespace Core.SceneManagement.LoadingScreen
{
	public class LoadingScreenProvider
	{
		public LoadingScreenBehaviour LoadingScreen { get; private set; }
		
		public void Register(LoadingScreenBehaviour loadingScreen) => 
			LoadingScreen = loadingScreen;
	}
}