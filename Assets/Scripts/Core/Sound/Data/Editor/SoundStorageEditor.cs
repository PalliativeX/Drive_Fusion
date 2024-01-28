using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Core.Sound.Data.Editor
{
	[CustomEditor(typeof(SoundStorage))]
	public class SoundStorageEditor : OdinEditor
	{
		private SoundStorage _storage;
		private AudioSource _source;

		private SoundId _soundId;
		
		protected override void OnEnable()
		{
			base.OnEnable();
			_storage = target as SoundStorage;
			var sourcePrefab = AssetDatabase.LoadAssetAtPath<AudioSource>("Assets/Prefabs/Audio/SoundAudioSource.prefab");
			_source = Instantiate(sourcePrefab);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			_storage = null;
			DestroyImmediate(_source.gameObject);
			_source = null;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			var defaultGuiColor = GUI.backgroundColor;
			
			GUILayout.Space(8f);

			SirenixEditorGUI.BeginBox("Sound Editor Preview");
			
			_soundId = (SoundId) EditorGUILayout.EnumPopup("Sound Id", _soundId);
			
			GUI.backgroundColor = Color.green;
			if (!Application.isPlaying && GUILayout.Button("Play"))
			{
				SoundEntry sound = _storage.GetSound(_soundId);
				_source.clip = sound.Clip;
				_source.volume = sound.Volume;
				_source.pitch = sound.Pitch;
				_source.loop = sound.IsLooped;
				
				_source.Play();
			}
			
			GUI.backgroundColor = Color.yellow;
			if (!Application.isPlaying && GUILayout.Button("Stop"))
			{
				_source.Stop();
			}

			GUI.backgroundColor = defaultGuiColor;
			
			SirenixEditorGUI.EndBox();
		}
	}
}