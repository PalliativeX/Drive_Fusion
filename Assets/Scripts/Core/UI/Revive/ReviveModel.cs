using Core.ECS;
using Core.Gameplay;
using Core.Levels;
using Core.UI.Result;
using Scellecs.Morpeh;

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
			
			player.ChangeDurability(1f);

			player.TryRemove<Wrecked>();
			player.RemoveComponent<Stopped>();
			
			player.SetComponent(new ReviveUnavailable());

			_panelController.Close<RevivePresenter>();
		}

		public void OnRefuse()
		{
			SwitchPause(false);
			_panelController.Close<RevivePresenter>();
			_panelController.Open<ResultPresenter>();
		}
	}
}