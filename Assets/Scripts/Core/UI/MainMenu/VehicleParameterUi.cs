using TMPro;
using UnityEngine;
using Utils;

namespace Core.UI.MainMenu
{
	public sealed class VehicleParameterUi : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _name;
		[SerializeField] private SlicedFilledImage _fill;

		public void Set(string name, float value)
		{
			_name.text = name;
			_fill.fillAmount = value;
		}
	}
}