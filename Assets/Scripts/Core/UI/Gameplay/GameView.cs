using TMPro;
using UnityEngine.UI;
using Utils;

namespace Core.UI
{
	public sealed class GameView : BaseView
	{
		public GameMovementTouchArea[] TouchAreas;

		public Button SettingsButton;

		public TextMeshProUGUI ScoreLabel;
		public SlicedFilledImage DurabilityFill;
		public SlicedFilledImage FuelFill;
	}
}