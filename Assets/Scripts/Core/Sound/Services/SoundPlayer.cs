using Core.ECS;
using Core.Infrastructure;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Sound
{
	public class SoundPlayer : AudioPlayer
	{
		protected override SoundType Type => SoundType.Sound;
		
		public SoundPlayer(UnityCallbacksReceiver callbacksReceiver) : base(callbacksReceiver) { }
		
		[Inject]
		public void Construct(AudioSourceCreator audioSourceCreator) => 
			AudioSourceCreator = audioSourceCreator;

		public override void Play(Entity entity)
		{
			var freeSource = FindFree();
			if (freeSource == null)
			{
				entity.SetComponent(new Destroyed());
				return;
			}
			
			Play(entity, freeSource);
		}
	}
}