using System;
using System.Collections.Generic;
using Cinemachine;
using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.CameraLogic
{
	public sealed class CameraBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Camera _mainCamera;
		[SerializeField] private List<CameraEntry> _cameras;
		[SerializeField] private string _defaultCamera;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new MainCamera { Reference = _mainCamera });

			Dictionary<string, CinemachineVirtualCamera> cameras = new();
			foreach (var cameraEntry in _cameras) 
				cameras[cameraEntry.CameraName] = cameraEntry.Camera;

			entity.SetComponent(new VirtualCameras { List = cameras });
			entity.SetComponent(new CurrentVirtualCamera { Value = _defaultCamera });
			entity.SetComponent(new CurrentVirtualCameraChanged());
		}

		public override void Unlink(Entity entity) { }
	}

	[Serializable]
	public class CameraEntry
	{
		public string CameraName;
		public CinemachineVirtualCamera Camera;
	}
}