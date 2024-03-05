using Cysharp.Threading.Tasks;
using Utils;

namespace Core.UI.Menu
{
	public class MenuPresenter : APresenter<MenuView>
	{
		private readonly MenuModel _model;

		public override string Name => "Menu";

		public MenuPresenter(MenuModel model)
		{
			_model = model;
		}

		protected override void OnShow()
		{
			// View.LevelText.SetText($"Level {_model.GetCurrentLevel()}");

			PlayAnimation();
		}

		protected override void OnClose() { }

		private async UniTaskVoid PlayAnimation()
		{
			await _model.Initialize(View.AnimationSpeed);

			await UniTask.Delay((1000 * View.InitialWaitDuration).ToInt());

			_model.SetMainCamera();

			await UniTask.Delay((1000 * View.AnimationDuration).ToInt());
			
			_model.StartPlaying();
		}
	}
}