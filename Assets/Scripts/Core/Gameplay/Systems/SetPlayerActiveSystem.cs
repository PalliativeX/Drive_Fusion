using Core.ECS;
using Core.Infrastructure.GameFsm;
using Scellecs.Morpeh;

namespace Core.Gameplay
{
	public class SetPlayerActiveSystem : IInitializer
	{
		private readonly IGameStateMachine _gameFsm;

		public World World { get; set; }

		public SetPlayerActiveSystem(GameStateMachine gameFsm) => 
			_gameFsm = gameFsm;

		public void OnAwake() => _gameFsm.StateChanged += OnStateChanged;
		public void Dispose() => _gameFsm.StateChanged -= OnStateChanged;

		private void OnStateChanged(GameStateType state)
		{
			var player = World.Filter.With<HumanPlayer>().Build().First();
			if (state != GameStateType.Gameplay)
			{
				player.TryRemove<Active>();
				return;
			}

			player.SetComponent(new Active());
		}
	}
}