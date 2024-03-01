using Core.Gameplay;
using SimpleInject;
using UniRx;
using UnityEngine;
using Utils;

namespace Core.UI
{
	public class GamePresenter : APresenter<GameView>, ITickable
	{
		private readonly GameModel _model;
		private readonly CurrentLevelService _currentLevelService;

		public override string Name => "Game";

		public GamePresenter(GameModel model, CurrentLevelService currentLevelService)
		{
			_model = model;
			_currentLevelService = currentLevelService;
		}

		protected override void OnShow()
		{
			foreach (var touchArea in View.TouchAreas) {
				touchArea.PointerDown += OnTouchAreaPressed;
				touchArea.PointerUp += OnTouchAreaUp;
			}

			View.SettingsButton.OnClickSubscribeDisposable(_model.OnSettings).AddTo(Disposable);
			
			_currentLevelService.ScoreChanged += UpdateScore;

			UpdateDurability();
			UpdateFuel();
			UpdateScore(_currentLevelService.Score);
		}

		protected override void OnClose()
		{
			foreach (var touchArea in View.TouchAreas) {
				touchArea.PointerDown -= OnTouchAreaPressed;
				touchArea.PointerUp -= OnTouchAreaUp;
			}
			
			_currentLevelService.ScoreChanged -= UpdateScore;
			
			OnTouchAreaUp();
		}

		public void Tick()
		{
			if (!IsActive)
				return;
			
			UpdateFuel();
		}

		public void UpdateDurability() => View.DurabilityFill.fillAmount = _model.GetCurrentDurability();

		private void UpdateFuel() => View.FuelFill.fillAmount = _model.GetCurrentFuel();

		private void OnTouchAreaPressed(MovementTouchAreaType type)
		{
			_model.SetXTouchInput(type == MovementTouchAreaType.Left ? -1f : 1f);
		}

		private void OnTouchAreaUp() => _model.SetXTouchInput(0f);
		
		private void UpdateScore(float score)
		{
			View.ScoreLabel.text = score.ToString("F0");

			float fill = 1f;
			if (!_currentLevelService.CurrentScoreRecord.IsZero())
				fill = score / _currentLevelService.CurrentScoreRecord;

			View.ScoreRecordFill.fillAmount = fill;
		}
	}
}