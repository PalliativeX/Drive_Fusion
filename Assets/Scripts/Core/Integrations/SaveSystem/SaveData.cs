using System;
using System.Collections.Generic;

namespace Core.Integrations.SaveSystem
{
	[Serializable]
	public class SaveData
	{
		public int Money;

		public List<string> OwnedVehicles;
		public string SelectedVehicle;

		public bool Rated;
		public int RateUsShowCount;
	}
}