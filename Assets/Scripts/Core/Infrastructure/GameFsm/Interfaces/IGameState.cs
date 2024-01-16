using System;

namespace Core.Infrastructure.GameFsm
{
	public interface IGameState
	{
		GameStateType Type { get; }
		
		event Action<GameStateType> RequestNextState;
		
		void Enter();
		void Update();
		void Exit();
	}
}