using System.Runtime.InteropServices;
using SimpleInject;
using UnityEngine;

namespace Core
{
	public sealed class GeneralSettingsService : IInitializable
	{
		private readonly GeneralSettings _settings;
		
		public bool IsMobileDevice { get; private set; }

		public GeneralSettingsService(GeneralSettings settings) => 
			_settings = settings;

		public void Initialize()
		{
			QualitySettings.vSyncCount = _settings.DisableVsync ? 0 : 1;
			Application.targetFrameRate = _settings.TargetFrameRate;

			Input.multiTouchEnabled = _settings.IsEnableMultitouch;
			
			IsMobileDevice = CheckMobileDevice();
			if (!IsMobileDevice)
				QualitySettings.SetQualityLevel(1, true);
		}
		
		private bool CheckMobileDevice()
		{
#if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
#endif
			return false;
		}

		[DllImport("__Internal")]
		private static extern bool IsMobile();
	}
}