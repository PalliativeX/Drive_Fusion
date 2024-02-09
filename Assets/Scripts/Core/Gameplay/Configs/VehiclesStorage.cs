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
	}
}