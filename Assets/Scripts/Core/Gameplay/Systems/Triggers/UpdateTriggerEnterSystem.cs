using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class UpdateTriggerEnterSystem : ISystem
	{
		private readonly TriggerHandler _triggerHandler;
		private Filter _filter;
		
		public World World { get; set; }

		public UpdateTriggerEnterSystem(TriggerHandler triggerHandler) => 
			_triggerHandler = triggerHandler;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<ObjectsEnteredTrigger>()
				.With<ObjectsInTrigger>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref var entered = ref entity.GetComponent<ObjectsEnteredTrigger>();
				ref var insideTrigger = ref entity.GetComponent<ObjectsInTrigger>();

				for (int i = entered.List.Count - 1; i >= 0; i--)
				{
					int enteredInstanceId = entered.List[i];
					
					entered.List.RemoveAt(i);
					
					insideTrigger.List.Add(enteredInstanceId);
					
					_triggerHandler.OnEnter(entity, enteredInstanceId);
				}
			}
		}
		
		public void Dispose() { }
	}
}