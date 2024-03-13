using System;
using Core.Infrastructure.GameFsm;
using Core.Tutorial;
using Core.UI.Menu;
using Core.UI.Tutorial;
using SimpleInject;

namespace Core.UI.Systems
{
	public class PanelByStateInitializer : IInitializable, IDisposable
	{
		private readonly PanelController _panelController;
		private readonly IGameStateMachine _gameFsm;
		private readonly TutorialService _tutorial;

		public PanelByStateInitializer(PanelController panelController, GameStateMachine gameFsm, TutorialService tutorial)
		{
			_panelController = panelController;
			_gameFsm = gameFsm;
			_tutorial = tutorial;
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
				if (_tutorial.HasTutorial())
					_panelController.Open<TutorialPresenter>();
			}
		}

		public void Dispose() => _gameFsm.StateChanged -= OnStateChanged;
	}
}