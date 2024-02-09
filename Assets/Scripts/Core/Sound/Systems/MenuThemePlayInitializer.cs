using Core.ECS;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Sound
{
	public class MenuThemePlayInitializer : IInitializable
	{
		private readonly World _world;

		public MenuThemePlayInitializer(GlobalWorld world) => _world = world;

		public void Initialize()
		{
			_world.CreateSound(SoundId.MenuTheme, SoundType.Music);
		}
	}
}

	