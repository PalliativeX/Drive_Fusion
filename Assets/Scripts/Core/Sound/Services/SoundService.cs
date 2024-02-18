namespace Core.Sound
{
	public class SoundService
	{
		private readonly MusicPlayer _musicPlayer;
		private readonly SoundPlayer _soundPlayer;

		public bool IsSoundActive { get; private set; } = true;

		public SoundService(MusicPlayer musicPlayer, SoundPlayer soundPlayer)
		{
			_musicPlayer = musicPlayer;
			_soundPlayer = soundPlayer;
		}

		public void SwitchActive(bool active)
		{
			IsSoundActive = active;
			_musicPlayer.SwitchSourcesActive(active);
			_soundPlayer.SwitchSourcesActive(active);
		}
		
		public void ToggleActive() => SwitchActive(!IsSoundActive);
	}
}