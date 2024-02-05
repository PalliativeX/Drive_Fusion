using SimpleInject;

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

			View.DurabilityFill.fillAmount = _model.GetCurrentDurability();
			View.FuelFill.fillAmount = _model.GetCurrentFuel();
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
			
			View.FuelFill.fillAmount = _model.GetCurrentFuel();
		}

		private void OnTouchAreaPressed(MovementTouchAreaType type)
		{
			_model.SetXTouchInput(type == MovementTouchAreaType.Left ? -1f : 1f);
		}

		private void OnTouchAreaUp() => _model.SetXTouchInput(0f);
	}
}