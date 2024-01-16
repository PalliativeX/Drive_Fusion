using System;

namespace Core.Infrastructure.GameFsm
{
	public class MenuState : IGameState
	{
		public GameStateType Type => GameStateType.Menu;
		
		public event Action<GameStateType> RequestNextState;

		public void Enter()
		{
			
		}
		
		public void Update() { }
		
		public void Exit() { }
	}
}