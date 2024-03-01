using TMPro;
using UnityEngine;

namespace Core.Localization
{
	public sealed class LocalizationBehaviour : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;
		[SerializeField] private LocaleId _key;

		private void Start()
		{
			_text.text = LocalizationService.Instance.GetText(_key);
		}

		private void OnValidate()
		{
			if (!_text)
				_text = GetComponent<TextMeshProUGUI>();
		}
	}
}