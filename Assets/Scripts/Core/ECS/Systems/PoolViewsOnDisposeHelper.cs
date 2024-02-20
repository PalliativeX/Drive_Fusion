using Core.AssetManagement;
using Core.Infrastructure.GameFsm;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.ECS
{
	public sealed class PoolViewsOnDisposeHelper : ISystem
	{
		private readonly IGameStateMachine _gameFsm;
		private readonly IAssetProvider _assetProvider;
		private Filter _filter;
		
		public World World { get; set; }

		public PoolViewsOnDisposeHelper(GameStateMachine gameFsm, AssetProvider assetProvider)
		{
			_gameFsm = gameFsm;
			_assetProvider = assetProvider;
			
			_gameFsm.StateChanged += OnGameStateChanged;
		}

		public void OnAwake()
		{
			_filter = World.Filter.With<View>().With<Pooled>().Build();
		}

		private void OnGameStateChanged(GameStateType state)
		{
			if (state == GameStateType.LoadLevel)
				DisposePooled();
		}

		public void OnUpdate(float deltaTime)
		{
			foreach (var entity in _filter)
				if (entity.Has<Destroyed>())
					RemoveViewsByFilter(entity);
		}

		private void DisposePooled()
		{
			_gameFsm.StateChanged -= OnGameStateChanged;

			foreach (var entity in _filter)
				RemoveViewsByFilter(entity);
		}

		private void RemoveViewsByFilter(Entity entity) {
				var view = entity.GetComponent<View>();
				var pooled = entity.GetComponent<Pooled>();

				if (!view.Reference) {
					Debug.LogError($"View is null for name: {pooled.AssetName}!");
					return;
				}

				var baseBehaviour = view.Reference.GetComponent<BaseEcsBehaviour>();
				if (baseBehaviour)
					baseBehaviour.Unlink();

				_assetProvider.Return(view.Reference, pooled.AssetName);

				entity.RemoveComponent<View>();
				entity.RemoveComponent<Pooled>();
		}

		public void Dispose() { }
	}
}