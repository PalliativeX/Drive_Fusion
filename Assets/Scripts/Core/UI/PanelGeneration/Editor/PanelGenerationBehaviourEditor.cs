using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Core.UI.PanelGeneration
{
	[CustomEditor(typeof(PanelGenerationBehaviour))]
	public sealed class PanelGenerationBehaviourEditor : OdinEditor
	{
		private PanelGenerationBehaviour _generator;

		protected override void OnEnable()
		{
			base.OnEnable();
			_generator = target as PanelGenerationBehaviour;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_generator = null;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var defaultGuiColor = GUI.backgroundColor;

			GUI.backgroundColor = Color.green;
			if (GUILayout.Button("Generate"))
			{
				PanelGenerator.Generate(
					new PanelGenerationData(
						_generator.Name,
						_generator.Path,
						_generator.CreateModel,
						_generator.CreatePresenter,
						_generator.CreateView
					)
				);
				Debug.Log($"Panel {_generator.Name}Panel generated!");
			}

			GUI.backgroundColor = defaultGuiColor;
		}
	}
}