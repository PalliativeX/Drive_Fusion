using System;
using Core.Integrations.SaveSystem;

namespace Core.Gameplay
{
	public class GameplayParametersProvider : ILoadable
	{
		private readonly VehiclesStorage _vehicles;
		private readonly SaveService _save;

		public float Sensitivity { get; private set; }

		public event Action<float> SensitivityChanged;
		
		public GameplayParametersProvider(VehiclesStorage vehicles, SaveService save)
		{
			_vehicles = vehicles;
			_save = save;
		}

		public void Load(SaveData data)
		{
			Sensitivity = data.IsFirstSession ? 
				_vehicles.DefaultSensitivity : 
				data.Sensitivity;
		}

		public void SetSensitivity(float newSensitivity)
		{
			Sensitivity = newSensitivity;
			SensitivityChanged?.Invoke(newSensitivity);
			
			_save.SaveData.Sensitivity = newSensitivity;
		}
	}
}