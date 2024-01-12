using System.IO;

namespace Core.SceneManagement
{
	public static class SceneReferenceExtensions
	{
		public static string GetSceneName(this SceneReference sceneReference) 
			=> Path.GetFileNameWithoutExtension(sceneReference.ScenePath);
	}
}