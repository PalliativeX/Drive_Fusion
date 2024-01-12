using System.Collections.Generic;
using Core.AssetManagement;
using Scellecs.Morpeh;
using SimpleInject;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Sound
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public class AudioSourceCreator
	{
		private AssetProvider _assetProvider;
		private SoundStorage _storage;

		public World World { get; set; }

		[Inject]
		public void Construct(AssetProvider assetProvider, SoundStorage storage)
		{
			_storage = storage;
			_assetProvider = assetProvider;
		}

		public List<AudioSource> CreateSoundSources(SoundType type)
		{
			var data = _storage.GetSourceData(type);
			List<AudioSource> sources = new List<AudioSource>(data.SourceCount);

			for (int i = 0; i < data.SourceCount; i++)
			{
				AudioSource source = _assetProvider.LoadAsset<AudioSource>(data.AssetName).Item1;
				sources.Add(source);
			}

			return sources;
		}
	}
}