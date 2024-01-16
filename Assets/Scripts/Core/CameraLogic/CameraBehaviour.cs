using Cinemachine;
using Core.ECS;
using Scellecs.Morpeh;
using UnityEngine;

namespace Core.CameraLogic
{
	public sealed class CameraBehaviour : AChildEcsBehaviour
	{
		[SerializeField] private Camera _mainCamera;
		[SerializeField] private CinemachineVirtualCamera _camera;

		public override void Link(Entity entity)
		{
			entity.SetComponent(new MainCamera { Reference = _mainCamera });
			entity.SetComponent(new ActualCamera { Reference = _camera });
		}

		public override void Unlink(Entity entity) { }
	}
}