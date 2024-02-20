using Core.ECS;
using Core.Gameplay;
using Core.Infrastructure.GameFsm;
using Scellecs.Morpeh;

namespace Core.UI
{
	public class PanelsDisposeHandler : IInitializer
	{
		private readonly IGameStateMachine _gameFsm;
		private readonly PanelController _panelController;

		public World World { get; set; }

		public PanelsDisposeHandler(GameStateMachine gameFsm, PanelController panelController)
		{
			_gameFsm = gameFsm;
			_panelController = panelController;
		}

		public void OnAwake() => _gameFsm.StateChanged += OnStateChanged;
		public void Dispose() => _gameFsm.StateChanged -= OnStateChanged;

		private void OnStateChanged(GameStateType state)
		{
			if (state == GameStateType.LoadLevel)
				_panelController.Dispose();
		}
	}
}