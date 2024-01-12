using System;

namespace Core.Sound
{
	[Serializable]
	public class AudioSourceData
	{
		public SoundType Type;
		public string AssetName;
		public int SourceCount;
	}
}