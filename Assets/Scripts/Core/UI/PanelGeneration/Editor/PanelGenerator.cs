using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Core.UI.PanelGeneration
{
	public static class PanelGenerator
	{
		public static void Generate(PanelGenerationData data)
		{
			if (DataHasErrors(data))
				return;

			string fullPath = Application.dataPath + data.Path;
			DeleteFolder(fullPath);
			CreateFolder(fullPath);

			string namespaceValue = $"Core.UI.{data.Name}";

			if (data.CreateModel)
			{
				var modelData = new ClassData(
					data.Name + "Model",
					Array.Empty<string>(),
					namespaceValue,
					new List<string>(),
					null
				);
				string text = CreateClass(modelData);
				WriteClass(fullPath, "Model", text, data);
			}

			if (data.CreatePresenter)
			{
				var parents = new[] { $"APresenter<{data.Name}View>" };
				var presenterData = new ClassData(
					data.Name + "Presenter",
					parents,
					namespaceValue,
					new List<string>(),
					$"public override string Name => \"{data.Name}\";\n"
				);
				string text = CreateClass(presenterData);
				WriteClass(fullPath, "Presenter", text, data);
			}
			
			if (data.CreateView)
			{
				var parents = new[] { "BaseView" };
				var viewData = new ClassData(
					data.Name + "View",
					parents,
					namespaceValue,
					new List<string>(),
					null
				);
				string text = CreateClass(viewData);
				WriteClass(fullPath, "View", text, data);
			}
			
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
		}

		private static string CreateClass(ClassData data)
		{
			var builder = new StringBuilderGenerator();

			StringBuilder parents = new StringBuilder(data.ParentClasses.Length > 0 ? " : " : "");
			parents.Append(string.Join(", ", data.ParentClasses));
			
			if (!data.Usings.IsNullOrEmpty())
			{
				foreach (string @using in data.Usings) 
					builder.Append($"using {@using};").NewLine();
				builder.NewLine();
			}
			
			builder.Append($"namespace {data.NamespaceValue}").NewLine();

			builder.OpenBrace();
			
			builder.Append($"public sealed class {data.ClassName}{parents}")
				.NewLine();

			builder.OpenBrace();
			
			if (data.Body != null)
				builder.AppendBody(data.Body);
				
			builder.CloseBrace();
			builder.CloseBrace();

			return builder.ToString();
		}

		private static void WriteClass(string path, string postfix, string content, PanelGenerationData data) =>
			File.WriteAllText(path + data.Name + postfix + ".cs", content);

		private static bool DataHasErrors(PanelGenerationData generationData)
		{
			if (generationData.Name.IsNullOrWhitespace())
			{
				Debug.LogError("Name is not set!");
				return true;
			}

			if (generationData.Path.IsNullOrWhitespace())
			{
				Debug.LogError("Path is not set!");
				return true;
			}

			return false;
		}

		private static void CreateFolder(string path)
		{
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		private static void DeleteFolder(string path)
		{
			if (Directory.Exists(path))
				Directory.Delete(path, true);
		}
	}
}