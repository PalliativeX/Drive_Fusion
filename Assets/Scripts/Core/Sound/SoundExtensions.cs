using Scellecs.Morpeh;

namespace Core.Sound
{
	public static class SoundExtensions
	{
		public static void CreateSound(this World world, SoundId id, SoundType type)
		{
			var entity = world.CreateEntity();
			entity.SetComponent(new Clip {Value = id});
			entity.SetComponent(new SoundTypeComponent {Value = type});
		}
	}
}