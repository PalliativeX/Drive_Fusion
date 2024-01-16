using System;

namespace Core.Infrastructure.GameFsm
{
	public interface IGameStateMachine
	{
		GameStateType ActiveState { get; }
		
		event Action<GameStateType> StateChanged;
		IGameState ChangeState(GameStateType stateType);
		IParameterizedGameState<T> ChangeState<T>(GameStateType stateType, T parameter);
	}
}