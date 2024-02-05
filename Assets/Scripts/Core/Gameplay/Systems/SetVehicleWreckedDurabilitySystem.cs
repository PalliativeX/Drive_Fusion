using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class SetVehicleWreckedDurabilitySystem : ISystem
	{
		private Filter _filter;
		
		public World World { get; set; }

		public void OnAwake()
		{
			_filter = World.Filter
				.With<Durability>()
				.Without<Wrecked>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				float durability = entity.GetComponent<Durability>().Value;
				if (durability <= 0f)
					entity.SetComponent(new Wrecked());
			}
		}
		
		public void Dispose() { }
	}
}