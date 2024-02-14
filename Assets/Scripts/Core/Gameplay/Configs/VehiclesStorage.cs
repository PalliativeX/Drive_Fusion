using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(VehiclesStorage), menuName = "Storages/" + nameof(VehiclesStorage))]
	public sealed class VehiclesStorage : ScriptableObject
	{
		public List<VehicleConfig> Configs;

		public VehicleConfig Get(string vehicleName)
		{
			foreach (var config in Configs)
				if (config.Name == vehicleName)
					return config;

			throw new Exception("Vehicle Config not found for name: " + vehicleName);
		}

		public VehicleConfig GetNext(string vehicleName)
		{
			int index = GetIndex(vehicleName);
			return index + 1 >= Configs.Count ? Configs[0] : Configs[index + 1];
		}
		
		public VehicleConfig GetPrevious(string vehicleName)
		{
			int index = GetIndex(vehicleName);
			return index - 1 < Configs.Count ? Configs[^1] : Configs[index - 1];
		}

		private int GetIndex(string vehicleName)
		{
			for (int index = 0; index < Configs.Count; index++)
			{
				var config = Configs[index];
				if (config.Name == vehicleName)
					return index;
			}

			return -1;
		}
	}
}