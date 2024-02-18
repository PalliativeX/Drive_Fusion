using Core.ECS;
using Core.Gameplay;
using Core.Levels;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.UI.Revive
{
	public sealed class ReviveModel
	{
		private readonly LevelsHelper _levelsHelper;
		private readonly PanelController _panelController;
		private readonly World _world;
		private readonly GamePauser _pauser;

		public ReviveModel(LevelsHelper levelsHelper, PanelController panelController, World world, GamePauser pauser)
		{
			_levelsHelper = levelsHelper;
			_panelController = panelController;
			_world = world;
			_pauser = pauser;
		}

		public void SwitchPause(bool paused) =>
			_pauser.SwitchPause(paused);

		// TODO: For Ads!
		public void OnRevive()
		{
			Entity player = _world.GetPlayer();
			ref var fuel = ref player.GetComponent<Fuel>();
			fuel.Value = 1f;
			
			ref var durability = ref player.GetComponent<Durability>();
			durability.Value = 1f;

			player.TryRemove<Wrecked>();
			player.RemoveComponent<Stopped>();

			_panelController.Close<RevivePresenter>();
		}

		public void OnRefuse()
		{
			SwitchPause(false);
			_levelsHelper.LoadMenu();
		}
	}
}