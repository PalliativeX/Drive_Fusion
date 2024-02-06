using Sirenix.Utilities;
using UnityEngine;

namespace Core.UI.PanelGeneration
{
	public sealed class PanelGenerationBehaviour : MonoBehaviour
	{
		public string Name;
		public string Path;

		public bool CreateModel = true;
		public bool CreatePresenter = true;
		public bool CreateView = true;
		
#if UNITY_EDITOR
		private void OnValidate()
		{
			if (Name.IsNullOrWhitespace())
				Name = name;

			if (Path.IsNullOrWhitespace())
			{
				Path = $"/Scripts/Core/UI/{Name}/";
			}
		}
#endif
	}
}