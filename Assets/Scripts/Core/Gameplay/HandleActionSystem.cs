using Core.InputLogic;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class HandleActionSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<ActionInput>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ActionType input = entity.GetComponent<ActionInput>().Value;
				
				if (!entity.Has<MovementInputBlocked>())
					entity.SetComponent(new MovementInputBlocked());
				
				entity.RemoveComponent<ActionInput>();
			}
		}
		
		public void Dispose() { }
	}
}