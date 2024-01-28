using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.ECS
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct Position : IComponent
	{
		public Vector3 Value;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct Rotation : IComponent
	{
		public Vector3 Value;
		
		public Quaternion Quaternion => Quaternion.Euler(Value);
		public Vector3 Direction => Quaternion.Euler(Value) * Vector3.forward;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct TransformUpdatesPosition : IComponent { }
}