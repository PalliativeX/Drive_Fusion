using System;
using UnityEngine;

namespace Core.Sound
{
	[Serializable]
	public class SoundEntry
	{
		public SoundId Id;
		public AudioClip Clip;
		public bool IsLooped;
		public float Volume = 1f;
		public float Pitch = 1f;
	}
}