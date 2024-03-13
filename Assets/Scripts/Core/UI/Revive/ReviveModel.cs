using Core.ECS;
using Core.Gameplay;
using Core.Integrations.Ads;
using Core.Levels;
using Core.UI.Result;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.UI.Revive
{
	public sealed class ReviveModel
	{
		private readonly PanelController _panelController;
		private readonly World _world;
		private readonly GamePauser _pauser;
		private readonly AdsService _ads;
		private readonly RoadCreator _roadCreator;

		public ReviveModel(
			PanelController panelController,
			World world,
			GamePauser pauser,
			AdsService ads,
			RoadCreator roadCreator
		)
		{
			_panelController = panelController;
			_world = world;
			_pauser = pauser;
			_ads = ads;
			_roadCreator = roadCreator;
		}

		public void SwitchPause(bool paused) =>
			_pauser.SwitchPause(paused);

		public void OnRevive() => _ads.ShowRewardedAd(Revive);

		public void OnRefuse()
		{
			SwitchPause(false);
			_panelController.Close<RevivePresenter>();
			_panelController.Open<ResultPresenter>();
		}

		private void Revive(bool result)
		{
			if (!result)
			{
				OnRefuse();
				return;
			}

			Entity player = _world.GetPlayer();
			ref var fuel = ref player.GetComponent<Fuel>();
			fuel.Value = 1f;

			player.ChangeDurability(1f);

			player.TryRemove<Wrecked>();
			player.RemoveComponent<Stopped>();

			player.SetComponent(new ReviveUnavailable());

			var blockId = player.GetComponent<CurrentRoadBlock>().Value;
			_world.TryGetEntity(blockId, out Entity block);
			
			_roadCreator.DestroyBlockObjects(blockId);

			var carController = player.GetComponent<CarController>().Reference;

			carController.Clear(
				block.GetComponent<Position>().Value,
				Quaternion.LookRotation(block.GetComponent<RoadBlockDirection>().Forward)
			);

			_panelController.Close<RevivePresenter>();
		}
	}
}