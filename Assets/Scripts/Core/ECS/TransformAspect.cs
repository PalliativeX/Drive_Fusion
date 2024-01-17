using Scellecs.Morpeh;

namespace Core.ECS
{
	public struct TransformAspect : IAspect, IFilterExtension
	{
		public Entity Entity { get; set; }

		public ref Position Position => ref _position.Get(Entity);
		public ref Rotation Rotation => ref _rotation.Get(Entity);

		private Stash<Position> _position;
		private Stash<Rotation> _rotation;

		public void OnGetAspectFactory(World world)
		{
			_position = world.GetStash<Position>();
			_rotation = world.GetStash<Rotation>();
		}
		
		public FilterBuilder Extend(FilterBuilder rootFilter) => 
			rootFilter.With<Position>().With<Rotation>();
	}
}