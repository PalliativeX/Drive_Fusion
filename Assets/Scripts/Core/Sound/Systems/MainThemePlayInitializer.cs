using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Sound
{
	public class MainThemePlayInitializer : IInitializable
	{
		private World _world;

		[Inject]
		public void Construct(World world) => _world = world;

		public void Initialize()
		{
			_world.CreateSound(SoundId.MainTheme, SoundType.Music);
		}
	}
}