using Core.ECS;
using Core.Levels;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Sound
{
	public class MainThemePlayInitializer : IInitializable
	{
		private readonly World _world;
		private readonly LevelsHelper _levels;

		public MainThemePlayInitializer(GlobalWorld world, LevelsHelper levels)
		{
			_world = world;
			_levels = levels;
		}

		public void Initialize()
		{
			SoundId mainThemeId = _levels.GetCurrentEntry().MainThemeId;
			_world.CreateSound(mainThemeId, SoundType.Music);
		}
	}
}