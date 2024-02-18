using System;
using UnityEngine;

namespace Core.Gameplay
{
	[Serializable]
	public class VehicleParameter
	{
		public VehicleParameterType Type;
		[Range(0f, 1f)]
		public float Value;

		public VehicleParameter(VehicleParameterType type, float value)
		{
			Type = type;
			Value = value;
		}
	}
}