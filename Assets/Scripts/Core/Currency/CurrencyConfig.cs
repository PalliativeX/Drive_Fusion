using UnityEngine;

namespace Core.Currency
{
	[CreateAssetMenu(fileName = nameof(CurrencyConfig), menuName = "Configs/" + nameof(CurrencyConfig))]
	public sealed class CurrencyConfig : ScriptableObject
	{
		public float ScoreMoneyCoefficient;
	}
}