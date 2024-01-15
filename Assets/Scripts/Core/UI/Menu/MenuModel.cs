using Core.ECS;
using Core.Sound;
using Scellecs.Morpeh;

namespace Core.UI.Menu
{
	public class MenuModel
	{
		private readonly World _world;

		private readonly Filter _hasSoundFilter;
		
		public MenuModel(World world)
		{
			_world = world;

			_hasSoundFilter = world.Filter.With<SoundActive>().Build();
		}

		public bool IsSoundActive() => 
			_hasSoundFilter.First().GetComponent<SoundActive>().Value;

		public void ToggleSoundActive()
		{
			var entity = _hasSoundFilter.First();
			ref SoundActive soundActive = ref entity.GetComponent<SoundActive>();
			soundActive.Value = !soundActive.Value;
			entity.SetComponent(new Changed());
		}
	}
}