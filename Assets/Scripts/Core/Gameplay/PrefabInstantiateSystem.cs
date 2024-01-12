using Core.AssetManagement;
using Core.ECS;
using Core.Infrastructure;
using Scellecs.Morpeh;
using SimpleInject;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.Gameplay
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class PrefabInstantiateSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		private IAssetProvider _assetProvider;

		[Inject]
		public void Construct(AssetProvider assetProvider)
		{
			_assetProvider = assetProvider;
		}

		public void OnAwake()
		{
			_filter = World.Filter.With<Prefab>().Build();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
			{
				ref var prefabComponent = ref entity.GetComponent<Prefab>();

				(GameObject instantiated, bool isPooled) = _assetProvider.LoadAsset(prefabComponent.Value);
				
				entity.SetComponent(new View { Reference = instantiated });
				if (isPooled)
					entity.SetComponent(new Pooled());

				entity.RemoveComponent<Prefab>();
			}
		}
		
		public void Dispose() { }
	}
}