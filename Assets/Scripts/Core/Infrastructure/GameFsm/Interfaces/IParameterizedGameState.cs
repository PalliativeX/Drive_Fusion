namespace Core.Infrastructure.GameFsm
{
	public interface IParameterizedGameState<in T> : IGameState
	{
		void Enter(T parameter);
	}
}