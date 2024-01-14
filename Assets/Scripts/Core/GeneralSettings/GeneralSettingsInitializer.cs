using Scellecs.Morpeh;
using SimpleInject;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Core
{
	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.DivideByZeroChecks, false)]
	public sealed class GeneralSettingsInitializer : IInitializer
	{
		private readonly GeneralSettings _settings;

		public World World { get; set; }

		public GeneralSettingsInitializer(GeneralSettings settings) => 
			_settings = settings;

		public void OnAwake()
		{
			QualitySettings.vSyncCount = _settings.DisableVsync ? 0 : 1;
			Application.targetFrameRate = _settings.TargetFrameRate;

			Input.multiTouchEnabled = _settings.IsEnableMultitouch;
		}

		public void Dispose() { }
	}
}