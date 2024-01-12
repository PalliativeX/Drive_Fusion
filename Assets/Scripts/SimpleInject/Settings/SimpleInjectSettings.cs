using UnityEngine;

namespace SimpleInject
{
	[CreateAssetMenu(fileName = nameof(SimpleInjectSettings), menuName = "SimpleInject/SimpleInjectSettings")]
	public sealed class SimpleInjectSettings : ScriptableObject
	{
		public bool UseReflectionBaking;
		public ReflectionBakingSettings ReflectionBakingSettings;
	}
}