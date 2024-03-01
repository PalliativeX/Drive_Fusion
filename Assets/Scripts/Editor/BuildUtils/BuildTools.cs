using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildUtils
{
	public class BuildTools
	{
		private const string ProjectPath = "Builds/";

		[MenuItem("Tools/Build/[Yandex] Release")]
		public static void BuildYandex()
		{
			List<string> gameScenes = new List<string>();
			for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
				gameScenes.Add(SceneUtility.GetScenePathByBuildIndex(i));

			SetYandexFlag();

			BuildReport buildReport = BuildPipeline.BuildPlayer(
				gameScenes.ToArray(),
				ProjectPath,
				BuildTarget.WebGL,
				BuildOptions.None
			);

			LogBuildSummary(buildReport);

			ModifyIndexHtml();
		}

		[MenuItem("Tools/Build/Modify Index Html")]
		public static void ModifyIndexHtmlFile()
		{
			ModifyIndexHtml();
		}

		[MenuItem("Tools/Build/Clear Builds Folder")]
		public static void ClearBuildsFolder()
		{
			var buildsPath = Path.GetDirectoryName(Application.dataPath) + "/Builds";
			if (Directory.Exists(buildsPath))
			{
				Directory.Delete(buildsPath, true);
				Directory.CreateDirectory(buildsPath);
			}
		}

		private static void LogBuildSummary(BuildReport buildReport)
		{
			BuildSummary buildSummary = buildReport.summary;
			Debug.Log($"Build for: [{buildSummary.platform}]");
			Debug.Log($"Path: [{buildSummary.outputPath}]");
			Debug.Log($"Size in KB.: [{buildSummary.totalSize / 1024}]");
			Debug.Log($"Result: [{buildSummary.result}]");
			Debug.Log($"Time: [{buildSummary.totalTime}]");
		}

		private static void ModifyIndexHtml()
		{
			string projectFolder = Path.Combine(Application.dataPath, "../");

			string indexHtmlPath = Path.Combine(projectFolder, ProjectPath);

			if (!Directory.Exists(indexHtmlPath))
			{
				Debug.LogError($"Can't find path: {indexHtmlPath}");
				return;
			}

			string[] files = Directory.GetFiles(indexHtmlPath);

			var indexHtml = files.FirstOrDefault(file => file.Contains("index.html"));
			if (indexHtml == null)
				Debug.LogError("index.html not found!");

			string[] lines = File.ReadAllLines(indexHtml);

			List<string> updatedLines = new List<string>();
			for (var i = 0; i < lines.Length; i++)
			{
				updatedLines.Add(lines[i]);
				if (lines[i].Contains("<link rel=\"stylesheet\" href=\"TemplateData/style.css\">"))
				{
					updatedLines.Add("\n");
					updatedLines.Add("    <script src=\"https://yandex.ru/games/sdk/v2\"></script>");
					updatedLines.Add("\n");

					continue;
				}
				
				if (lines[i].Contains("<div id=\"unity-fullscreen-button\" style=\"display: none;\"></div>"))
				{
					updatedLines.Add("\n");
					AppendTextFromFile(Path.Combine(Application.dataPath, "Resources/YandexText.txt"), updatedLines);
					
					updatedLines.Add("\n");

					continue;
				}

				if (i + 1 < lines.Length && lines[i + 1].Contains("script.src = loaderUrl;"))
				{
					updatedLines.Add("\n");
					updatedLines.Add("      var unityInstance = null;");
					updatedLines.Add("\n");

					continue;
				}
				
				if (i + 1 < lines.Length && lines[i + 1].Contains("if (canFullscreen) {"))
				{
					updatedLines.Add("\n");
					updatedLines.Add("          this.unityInstance = unityInstance;");
					updatedLines.Add("\n");
				}

				if (lines[i].Contains("config.devicePixelRatio = 1;"))
					lines[i] = lines[i].Replace("config.devicePixelRatio = 1;", "config.devicePixelRatio = 1.75;");
			}

			for (int i = 0; i < updatedLines.Count; i++)
			{
				if (updatedLines[i].Contains("config.devicePixelRatio = 1;"))
					updatedLines[i] = updatedLines[i].Replace("config.devicePixelRatio = 1;", "config.devicePixelRatio = 1.75;");
			}

			File.WriteAllLines(indexHtml, updatedLines);
		}

		private static void AppendTextFromFile(string path, List<string> listToAppend)
		{
			string[] lines = File.ReadAllLines(path);

			foreach (string line in lines) 
				listToAppend.Add(line);
		}

		private static void SetYandexFlag() {
			var settings = Resources.Load<GeneralSettings>("Configs/GeneralSettings");
			settings.IsYandexGames = true;
			EditorUtility.SetDirty(settings);
			AssetDatabase.SaveAssetIfDirty(settings);
		}
	}
}