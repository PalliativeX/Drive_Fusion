using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
	public sealed class UiToggle : MonoBehaviour
	{
		[SerializeField] private Image _image;
		[SerializeField] private Button _button;
		[SerializeField] private Sprite _activeIcon;
		[SerializeField] private Sprite _inactiveIcon;
		
		public bool IsActive { get; private set; }

		public Button Button => _button;

		public void SwitchActive(bool active)
		{
			if (IsActive == active)
				return;

			IsActive = active;
			_image.sprite = GetSprite();
		}

		public void Toggle()
		{
			IsActive = !IsActive;
			_image.sprite = GetSprite();
		}

		private Sprite GetSprite() => IsActive ? _activeIcon : _inactiveIcon;
	}
}