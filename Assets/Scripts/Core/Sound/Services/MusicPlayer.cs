using Core.ECS;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Sound
{
	public class MusicPlayer : AudioPlayer
	{
		protected override SoundType Type => SoundType.Music;
		
		[Inject]
		public void Construct(AudioSourceCreator audioSourceCreator) => 
			AudioSourceCreator = audioSourceCreator;

		public override void Play(Entity entity)
		{
			Stop();

			var freeSource = FindFree();
			Play(entity, freeSource);
		}
	}
}