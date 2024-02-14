using Core.AssetManagement;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.ECS
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class PrefabInstantiateSystem : ISystem
	{
		public World World { get; set; }
		
		private Filter _filter;

		private readonly IAssetProvider _assetProvider;

		public PrefabInstantiateSystem(AssetProvider assetProvider)
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
				if (string.IsNullOrEmpty(prefabComponent.Value))
				{
					Debug.LogError("Prefab value is empty!");
					continue;
				}

				(GameObject instantiated, bool isPooled) = _assetProvider.LoadAsset(prefabComponent.Value);
				
				entity.SetComponent(new View { Reference = instantiated });
				if (isPooled)
					entity.SetComponent(new Pooled { AssetName = prefabComponent.Value });

				SetInitialTransform(instantiated, entity);

				var baseBehaviour = instantiated.GetComponent<BaseEcsBehaviour>();
				if (baseBehaviour) 
					baseBehaviour.Link(entity);

				entity.RemoveComponent<Prefab>();
			}
		}

		public void Dispose() { }

		private void SetInitialTransform(GameObject instantiated, Entity entity) {
			Transform transform = instantiated.transform;
			if (entity.Has<Position>())
				transform.position = entity.GetComponent<Position>().Value;
			if (entity.Has<Rotation>())
				transform.rotation = entity.GetComponent<Rotation>().Quaternion;
		}
	}
}