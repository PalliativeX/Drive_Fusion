using Core.ECS;
using Core.Gameplay;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.InputLogic
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class HandleKeyboardInputSystem : ISystem
	{
		private readonly InputHelper _helper;
		
		private const string Horizontal = "Horizontal";
		private const string Vertical = "Vertical";
		
		private Filter _filter;
		
		public World World { get; set; }

		public HandleKeyboardInputSystem(InputHelper helper) => 
			_helper = helper;

		public void OnAwake()
		{
			_filter = World.Filter
				.With<View>()
				.With<HumanPlayer>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				_helper.Set(entity, Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));
			}
		}

		public void Dispose() { }
	}
}