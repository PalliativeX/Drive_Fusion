using Core.ECS;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.CameraLogic
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class UpdateCurrentVirtualCameraSystem : ISystem
	{
		public World World { get; set; }

		private Filter _cameraFilter;

		public void OnAwake()
		{
			_cameraFilter = World.Filter.With<VirtualCameras>()
				.With<CurrentVirtualCamera>()
				.With<CurrentVirtualCameraChanged>()
				.Build();
		}

		public void OnUpdate(float deltaTime)
		{
			Entity camera = _cameraFilter.FirstOrDefault();
			if (camera == null)
				return;
			
			string currentCamera = camera.GetComponent<CurrentVirtualCamera>().Value;

			foreach ((string key, var virtualCamera) in camera.GetComponent<VirtualCameras>().List)
				virtualCamera.Priority = key == currentCamera ? 100 : 10;

			camera.RemoveComponent<CurrentVirtualCameraChanged>();
		}

		public void Dispose() { }
	}
}