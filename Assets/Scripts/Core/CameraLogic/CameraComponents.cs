using System;
using Cinemachine;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.CameraLogic
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct MainCamera : IComponent, IDisposable
	{
		public Camera Reference;

		public void Dispose() => Reference = null;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct ActualCamera : IComponent, IDisposable
	{
		public CinemachineVirtualCamera Reference;

		public void Dispose() => Reference = null;
	}
	
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	[Serializable]
	public struct CameraTarget : IComponent { }
}