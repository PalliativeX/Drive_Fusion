using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class UpdateTriggerExitSystem : ISystem
	{
		private readonly TriggerHandler _triggerHandler;
		private Filter _filter;
		
		public World World { get; set; }

		public UpdateTriggerExitSystem(TriggerHandler triggerHandler)
		{
			_triggerHandler = triggerHandler;
		}

		public void OnAwake()
		{
			_filter = World.Filter
				.With<ObjectsExitedTrigger>()
				.With<ObjectsInTrigger>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref var exited = ref entity.GetComponent<ObjectsExitedTrigger>();
				ref var insideTrigger = ref entity.GetComponent<ObjectsInTrigger>();

				for (int i = exited.List.Count - 1; i >= 0; i--)
				{
					int exitedInstanceId = exited.List[i];
					
					exited.List.RemoveAt(i);
					
					insideTrigger.List.Remove(exitedInstanceId);
					
					_triggerHandler.OnExit(entity, exitedInstanceId);
				}
			}
		}
		
		public void Dispose() { }
	}
}