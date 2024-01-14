using Core.ECS;
using Scellecs.Morpeh;
using SimpleInject;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Sound
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class PlayInitializedClipSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;
		
		private readonly MusicPlayer _musicPlayer;
		private readonly SoundPlayer _soundPlayer;

		public PlayInitializedClipSystem(MusicPlayer musicPlayer, SoundPlayer soundPlayer)
		{
			_soundPlayer = soundPlayer;
			_musicPlayer = musicPlayer;
		}

		public void OnAwake()
		{
			_filter = World.Filter
				.With<Clip>()
				.With<Initialized>()
				.Without<Destroyed>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				var soundType = entity.GetComponent<SoundTypeComponent>();
				if (soundType.Value == SoundType.Music)
					_musicPlayer.Play(entity);
				else
					_soundPlayer.Play(entity);
			}
		}
		
		public void Dispose() { }
	}
}