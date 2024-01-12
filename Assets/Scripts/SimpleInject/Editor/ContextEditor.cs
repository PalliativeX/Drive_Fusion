using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimpleInject
{
	[CustomEditor(typeof(AContext), true)]
	public sealed class ContextEditor : UnityEditor.Editor
	{
		private AContext _context;
		
		private void OnEnable()
		{
			_context = target as AContext;
		}

		private  void OnDisable()
		{
			_context = null;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			var defaultGuiColor = GUI.backgroundColor;
			GUI.backgroundColor = Color.green;
			
			EditorGUILayout.Space(8f);

			if (GUILayout.Button("Collect Installers"))
			{
				var installers = _context.GetComponents<MonoInstaller>();
				typeof(AContext)
					.GetField("_monoInstallers", BindingFlags.NonPublic | BindingFlags.Instance)
					.SetValue(_context, installers.ToList());
				
				EditorUtility.SetDirty(_context);
			}
			
			GUI.backgroundColor = defaultGuiColor;
			
		}
	}
}