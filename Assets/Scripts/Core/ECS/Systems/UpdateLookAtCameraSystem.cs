using Core.CameraLogic;
using Scellecs.Morpeh;

namespace Core.ECS
{
	public class UpdateLookAtCameraSystem : ISystem
	{
		public World World { get; set; }

		private Filter _cameraFilter;
		private Filter _lookAtCameraFilter;

		public void OnAwake()
		{
			_cameraFilter = World.Filter.With<MainCamera>().With<View>().Build();
			_lookAtCameraFilter = World.Filter.With<LookAtCamera>().With<View>().Build();
		}

		public void OnUpdate(float deltaTime)
		{
			Entity camera = _cameraFilter.FirstOrDefault();
			if (camera == null)
				return;

			var cameraTransform = camera.GetComponent<View>().Reference.transform;
			
			foreach (Entity lookAtCamera in _lookAtCameraFilter)
			{
				ref var view = ref lookAtCamera.GetComponent<View>();
				view.Reference.transform.forward = cameraTransform.forward;
			}
		}
		
		public void Dispose() { }
	}
}