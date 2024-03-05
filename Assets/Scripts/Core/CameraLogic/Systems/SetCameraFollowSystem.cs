using Core.ECS;
using Core.Gameplay;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core.CameraLogic
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class SetCameraFollowSystem : ISystem
	{
		public World World { get; set; }

		private Filter _cameraFilter;
		private Filter _cameraTargetFilter;

		public void OnAwake()
		{
			_cameraFilter = World.Filter.With<VirtualCameras>().Without<Link>().Build();
			_cameraTargetFilter = World.Filter.With<CameraTarget>().With<CameraTargetTransform>().With<View>().Build();
		}

		public void OnUpdate(float deltaTime)
		{
			var camera = _cameraFilter.FirstOrDefault();
			if (camera == null)
				return;

			var cameraTarget = _cameraTargetFilter.FirstOrDefault();
			if (cameraTarget == null)
				return;

			Transform targetTransform = cameraTarget.GetComponent<CameraTargetTransform>().Reference.transform;

			foreach ((string key, var virtualCamera) in camera.GetComponent<VirtualCameras>().List) {
				virtualCamera.Follow = targetTransform;
				virtualCamera.LookAt = targetTransform;
			}

			camera.SetComponent(new Link { Id = cameraTarget.ID });
		}

		public void Dispose() { }
	}
}