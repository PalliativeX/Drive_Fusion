using SimpleInject;
using UniRx;

namespace Core.UI
{
	public class GamePresenter : APresenter<GameView>, ITickable
	{
		private readonly GameModel _model;
		
		public override string Name => "Game";

		public GamePresenter(GameModel model)
		{
			_model = model;
		}

		protected override void OnShow()
		{
			foreach (var touchArea in View.TouchAreas) {
				touchArea.PointerDown += OnTouchAreaPressed;
				touchArea.PointerUp += OnTouchAreaUp;
			}

			View.SettingsButton.OnClickSubscribeDisposable(_model.OnSettings).AddTo(Disposable);

			UpdateDurability();
			UpdateFuel();
		}

		protected override void OnClose()
		{
			foreach (var touchArea in View.TouchAreas) {
				touchArea.PointerDown -= OnTouchAreaPressed;
				touchArea.PointerUp -= OnTouchAreaUp;
			}
			
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
	}
}