using Core.AssetManagement;
using Core.Gameplay;
using Core.Infrastructure.GameFsm;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.ECS
{
	public sealed class PoolViewsOnDisposeHelper 
	{
		private readonly IGameStateMachine _gameFsm;
		private readonly IAssetProvider _assetProvider;
		private readonly Filter _filter;
		
		public PoolViewsOnDisposeHelper(GameStateMachine gameFsm, World world, AssetProvider assetProvider)
		{
			_gameFsm = gameFsm;
			_assetProvider = assetProvider;
			
			_filter = world.Filter.With<View>().With<Pooled>().Build();

			_gameFsm.StateChanged += OnGameStateChanged;
		}

		private void OnGameStateChanged(GameStateType state)
		{
			if (state == GameStateType.LoadLevel)
				Dispose();
		}

		public void Dispose()
		{
			_gameFsm.StateChanged -= OnGameStateChanged;
			
			foreach (var entity in _filter)
			{
				var view = entity.GetComponent<View>();
				var pooled = entity.GetComponent<Pooled>();
				
				if (!view.Reference)
				{
					Debug.LogError($"View is null for name: {pooled.AssetName}!");
					continue;
				}
				
				var baseBehaviour = view.Reference.GetComponent<BaseEcsBehaviour>();
				if (baseBehaviour) 
					baseBehaviour.Unlink();
				
				_assetProvider.Return(view.Reference, pooled.AssetName);

				entity.RemoveComponent<View>();
				entity.RemoveComponent<Pooled>();
			}
		}
	}
}