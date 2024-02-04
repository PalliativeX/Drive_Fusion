using System;
using Core.Infrastructure.GameFsm;
using Core.UI.Menu;
using SimpleInject;

namespace Core.UI.Systems
{
	public class PanelByStateInitializer : IInitializable, IDisposable
	{
		private readonly PanelController _panelController;
		private readonly IGameStateMachine _gameFsm;

		public PanelByStateInitializer(PanelController panelController, GameStateMachine gameFsm)
		{
			_panelController = panelController;
			_gameFsm = gameFsm;
		}

		public void Initialize()
		{
			_gameFsm.StateChanged += OnStateChanged;

			_panelController.Open<MenuPresenter>();
		}

		private void OnStateChanged(GameStateType state)
		{
			if (state == GameStateType.Gameplay)
			{
				_panelController.Close<MenuPresenter>();
				_panelController.Open<GamePresenter>();
			}
		}

		public void Dispose() => _gameFsm.StateChanged -= OnStateChanged;
	}
}