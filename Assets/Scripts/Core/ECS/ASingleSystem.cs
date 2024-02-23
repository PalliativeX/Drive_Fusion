using Scellecs.Morpeh;

namespace Core.ECS
{
	public abstract class ASingleSystem<T> : ISystem where T : struct, IComponent
	{
		private Filter _filter;
		
		public World World { get; set; }

		public void OnAwake() => 
			_filter = World.Filter.With<T>().Build();

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter) 
				OnUpdate(deltaTime, entity);
		}

		protected abstract void OnUpdate(float deltaTime, Entity entity);
		
		public virtual void Dispose() { }
	}
}