using UnityEngine;

namespace Core.Tutorial
{
	[CreateAssetMenu(fileName = nameof(TutorialConfig), menuName = "Configs/" + nameof(TutorialConfig))]
	public sealed class TutorialConfig : ScriptableObject
	{
		public bool IsTutorialActive;
	}
}