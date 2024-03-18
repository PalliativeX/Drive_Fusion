using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(GameDifficultyConfig), menuName = "Configs/" + nameof(GameDifficultyConfig))]
	public sealed class GameDifficultyConfig : ScriptableObject
	{
		public AnimationCurve DifficultyCurve;
		public float MaxDifficulty;
	}
}