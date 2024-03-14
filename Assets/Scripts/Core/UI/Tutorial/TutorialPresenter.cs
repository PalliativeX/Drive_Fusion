using SimpleInject;
using UniRx;
using UnityEngine;
using Utils;

namespace Core.UI.Tutorial
{
	public sealed class TutorialPresenter : APresenter<TutorialView>, ITickable
	{
		private readonly TutorialModel _model;

		private bool _active;
		private int _clickedCount;
		
		public override string Name => "Tutorial";
		
		public TutorialPresenter(TutorialModel model) => _model = model;

		public void Tick()
		{
			if (!_active)
				return;
			
			CheckKeyboardInputPart(KeyCode.A);
			CheckKeyboardInputPart(KeyCode.D);
		}

		protected override void OnShow()
		{
			base.OnShow();

			Time.timeScale = 0;

			_clickedCount = 0;

			bool isMobile = _model.IsMobileDevice();
			_active = !isMobile;
			foreach (TutorialPartBehaviour tutorialPart in View.Parts)
			{
				tutorialPart.Button.OnClickSubscribeDisposable(() => OnTutorialPartClicked(tutorialPart))
					.AddTo(Disposable);
				tutorialPart.KeyboardInputContainer.SetActive(!isMobile);
			}
		}

		protected override void OnClose()
		{
			base.OnClose();
			
			_active = false;
			Time.timeScale = 1;
		}

		private void OnTutorialPartClicked(TutorialPartBehaviour part)
		{
			part.SetActive(false);

			_clickedCount++;

			if (_clickedCount >= View.Parts.Count)
			{
				_model.OnTutorialComplete();
			}
		}

		private void CheckKeyboardInputPart(KeyCode key)
		{
			if (!Input.GetKeyDown(key))
				return;
			
			var leftPart = View.Parts.First(p => p.KeyCode == key);
			if (leftPart.gameObject.activeSelf)
				OnTutorialPartClicked(leftPart);
		}
	}
}
