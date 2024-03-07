using Core.Infrastructure.GameFsm;
using Scellecs.Morpeh;

namespace Core.Gameplay
{
	public class SetSteeringSensitivityListener : IInitializer
	{
		private readonly GameplayParametersProvider _gameplayParameters;
		private readonly GameStateMachine _gameFsm;

		public World World { get; set; }
		
		public SetSteeringSensitivityListener(GameplayParametersProvider gameplayParameters, GameStateMachine gameFsm)
		{
			_gameplayParameters = gameplayParameters;
			_gameFsm = gameFsm;
		}

		public void OnAwake()
		{
			_gameplayParameters.SensitivityChanged += UpdateVehicleSensitivity;
			_gameFsm.StateChanged += OnStateChanged;
		}

		public void Dispose()
		{
			_gameplayParameters.SensitivityChanged -= UpdateVehicleSensitivity;
			_gameFsm.StateChanged -= OnStateChanged;
		}

		private void UpdateVehicleSensitivity(float newSensitivity)
		{
			Entity vehicle = World.Filter.With<CarController>().Build().First();
			ref var controller = ref vehicle.GetComponent<CarController>();
			controller.Reference.SteeringSensitivity = newSensitivity;
		}

		private void OnStateChanged(GameStateType state)
		{
			if (state == GameStateType.Gameplay)
				UpdateVehicleSensitivity(_gameplayParameters.Sensitivity);
		}
	}
}