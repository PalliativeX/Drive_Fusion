using System;
using System.Collections.Generic;
using Scellecs.Morpeh;
using SimpleInject;

namespace Core.Infrastructure
{
	public class Bootstrap : IInitializable, ITickable, IDisposable
	{
		private static int Index;
		
		private World _world;

		[Inject]
		public void Construct(
			[InjectScope(ContextScope.Local)] World world,
			[InjectScope(ContextScope.Local)] List<IInitializer> initializers,
			[InjectScope(ContextScope.Local)] List<ISystem> systems)
		{
			_world = world;
			_world.UpdateByUnity = true;
			
			var group = world.CreateSystemsGroup();

			foreach (IInitializer initializer in initializers) 
				group.AddInitializer(initializer);
			
			foreach (ISystem system in systems) 
				group.AddSystem(system);

			world.AddSystemsGroup(order: Index, group);
			Index++;
		}

		public void Initialize() { }

		public void Tick() { }

		public void Dispose() => _world = null;
	}
}