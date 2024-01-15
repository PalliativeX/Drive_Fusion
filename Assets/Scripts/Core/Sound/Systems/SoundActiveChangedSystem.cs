using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Sound
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class SoundActiveChangedSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;
		
		private readonly MusicPlayer _musicPlayer;
		private readonly SoundPlayer _soundPlayer;

		public SoundActiveChangedSystem(MusicPlayer musicPlayer, SoundPlayer soundPlayer)
		{
			_soundPlayer = soundPlayer;
			_musicPlayer = musicPlayer;
		}

		public void OnAwake()
		{
			_filter = World.Filter
				.With<SoundActive>()
				.With<Changed>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				bool soundActive = entity.GetComponent<SoundActive>().Value;
				_musicPlayer.SwitchSourcesActive(soundActive);
				_soundPlayer.SwitchSourcesActive(soundActive);

				entity.RemoveComponent<Changed>();
			}
		}
		
		public void Dispose() { }
	}
}