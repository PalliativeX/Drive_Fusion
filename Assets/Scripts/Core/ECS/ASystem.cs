using Scellecs.Morpeh;

namespace Core.ECS
{
	public abstract class ASystem<T1, T2> : ISystem 
		where T1 : struct, IComponent
		where T2 : struct, IComponent
	{
		private Filter _filter;
		
		public World World { get; set; }

		public void OnAwake() => 
			_filter = World.Filter.With<T1>().With<T2>().Build();

		public void OnUpdate(float deltaTime)
		{
			foreach (Entity entity in _filter) 
				OnUpdate(deltaTime, entity);
		}

		protected abstract void OnUpdate(float deltaTime, Entity entity);
		
		public virtual void Dispose() { }
	}
}