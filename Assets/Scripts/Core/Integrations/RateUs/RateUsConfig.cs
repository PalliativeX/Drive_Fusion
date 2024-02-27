using UnityEngine;

namespace Core.Integrations
{
	[CreateAssetMenu(fileName = nameof(RateUsConfig), menuName = "Configs/" + nameof(RateUsConfig))]
	public sealed class RateUsConfig : ScriptableObject
	{
		public int ShowCount;
	}
}