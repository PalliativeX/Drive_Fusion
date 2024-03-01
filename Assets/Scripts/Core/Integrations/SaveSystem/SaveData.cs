using System;
using System.Collections.Generic;

namespace Core.Integrations.SaveSystem
{
	[Serializable]
	public class SaveData
	{
		private const string DefaultVehicle = "Default";
		
		public bool IsFirstSession { get; set; }
		
		public int Money;

		public List<string> OwnedVehicles;
		public string SelectedVehicle;

		public bool Rated;
		public int RateUsShowCount;
		
		public List<LevelScoreData> HighestScores;

		public SaveData()
		{
			OwnedVehicles = new List<string> {
				DefaultVehicle
			};
			SelectedVehicle = DefaultVehicle;
			HighestScores = new List<LevelScoreData>();
		}
	}
}