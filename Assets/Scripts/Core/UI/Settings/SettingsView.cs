using UnityEngine.UI;

namespace Core.UI.Settings
{
	public sealed class SettingsView : BaseView
	{
		public Button MenuButton;
		public Button RestartButton;
		public Button ContinueButton;

		public UiToggle SoundToggle;
		public Slider SensitivitySlider;
	}
}
