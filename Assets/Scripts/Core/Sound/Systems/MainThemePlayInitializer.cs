using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Sound
{
	public class MainThemePlayInitializer : IInitializable
	{
		private readonly World _world;

		public MainThemePlayInitializer(World world) => _world = world;

		public void Initialize()
		{
			_world.CreateSound(SoundId.MainTheme, SoundType.Music);
		}
	}
}