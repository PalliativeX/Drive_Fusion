namespace Core.Infrastructure.GameFsm
{
	public enum GameStateType
	{
		LoadProgress,
		InitializeGlobalProgress,
		LoadLevel,
		InitializeGameplayProgress,
		Menu,
		Gameplay
	}
}