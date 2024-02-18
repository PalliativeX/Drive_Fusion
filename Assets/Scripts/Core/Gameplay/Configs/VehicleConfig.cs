using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(VehicleConfig), menuName = "Configs/" + nameof(VehicleConfig))]
	public sealed class VehicleConfig : ScriptableObject
	{
		public string Name;

		public int Price;
		
		[Range(0f, 1f)]
		public float FuelConsumptionPerSecond;

		[Header("Visuals")] 
		public string DisplayedName;
		public List<VehicleParameter> Parameters;

		private void OnValidate()
		{
			if (Parameters == null || Parameters.Count == 0)
			{
				Parameters = new List<VehicleParameter>();

				foreach (VehicleParameterType value in Enum.GetValues(typeof(VehicleParameterType)))
					Parameters.Add(new VehicleParameter(value, 0f));
				
				EditorUtility.SetDirty(this);
			}
		}
	}
}