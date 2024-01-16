using System;
using System.Collections.Generic;
using SimpleInject;
using UnityEngine;

namespace Core.Infrastructure.GameFsm
{
	public class GameStateMachine : IGameStateMachine, IInitializable, ITickable
	{
		private Dictionary<GameStateType, IGameState> _states;
		
		private IGameState _activeState;

		public GameStateType ActiveState => _activeState.Type;
		
		public event Action<GameStateType> StateChanged;

		[Inject]
		public void Construct(List<IGameState> states)
		{
			_states = new Dictionary<GameStateType, IGameState>();
			foreach (IGameState state in states)
			{
				_states[state.Type] = state;
				state.RequestNextState += OnNextStateRequested;
			}
		}

		public void Initialize() => 
			ChangeState(GameStateType.LoadProgress);

		public void Tick() => _activeState?.Update();

		public IGameState ChangeState(GameStateType stateType)
		{
			_activeState?.Exit();

			IGameState state = _states[stateType];
			_activeState = state;
			
#if DEBUG
			Debug.Log("NewState: " + $"<color=orange>{stateType}</color>");
#endif
			
			_activeState.Enter();
			
			StateChanged?.Invoke(state.Type);

			return state;
		}

		public IParameterizedGameState<T> ChangeState<T>(GameStateType type, T parameter)
		{
			var state = ChangeState(type);
			if (state is not IParameterizedGameState<T>)
				throw new Exception($"State of type '{type}' is parameterized as {typeof(T)}!");

			var parameterizedState = (IParameterizedGameState<T>)state;
			parameterizedState.Enter(parameter);
			return parameterizedState;
		}

		private void OnNextStateRequested(GameStateType type) => 
			ChangeState(type);
	}
}