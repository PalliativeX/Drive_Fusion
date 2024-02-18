using Core.Gameplay;
using TMPro;
using UnityEngine;
using Utils;

namespace Core.UI.MainMenu
{
	public sealed class VehicleParameterUi : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _name;
		[SerializeField] private SlicedFilledImage _fill;

		public void Set(VehicleParameter parameter)
		{
			_name.text = parameter.Type.ToString();
			_fill.fillAmount = parameter.Value;
		}
	}
}