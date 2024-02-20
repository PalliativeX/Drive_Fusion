using Core.ECS;
using Scellecs.Morpeh;
using SimpleInject;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Sound
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class InitializeClipSystem : ISystem
	{
		private readonly SoundStorage _storage;
		
		private Filter _filter;
		
		public World World { get; set; }

		public InitializeClipSystem(SoundStorage storage)
		{
			_storage = storage;
		}

		public void OnAwake()
		{
			_filter = World.Filter.With<Clip>().Without<Initialized>().Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref var clipComponent = ref entity.GetComponent<Clip>();

				SoundEntry entry = _storage.GetSound(clipComponent.Value);
				entity.SetComponent(new AudioClipComponent { Value = entry.Clip });
				entity.SetComponent(new Volume { Value = entry.Volume });
				entity.SetComponent(new Pitch { Value = entry.Pitch });
				if (entry.IsLooped)
					entity.SetComponent(new Looped());
				
				entity.SetComponent(new Initialized());
			}
		}
		
		public void Dispose() { }
	}
}