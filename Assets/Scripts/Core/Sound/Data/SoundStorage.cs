using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Core.Sound
{
	[CreateAssetMenu(fileName = "SoundStorage", menuName = "Storages/SoundStorage")]
	public sealed class SoundStorage : ScriptableObject
	{
		public List<SoundEntry> Sounds;
		public List<AudioSourceData> Sources;

		public SoundEntry GetSound(SoundId id)
		{
			SoundEntry entry = Sounds.FirstOrDefault(c => c.Id == id);
			if (entry == null)
				throw new Exception($"SoundEntry for id '{id}' not found!");

			return entry;
		}
		
		public AudioSourceData GetSourceData(SoundType type)
		{
			var entry = Sources.FirstOrDefault(c => c.Type == type);
			if (entry == null)
				throw new Exception($"SourceData for type '{type}' not found!");

			return entry;
		}
	}
}