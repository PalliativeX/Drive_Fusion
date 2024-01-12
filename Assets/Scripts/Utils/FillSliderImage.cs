using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
	public sealed class FillSliderImage : MonoBehaviour
	{
		[SerializeField] private Image _fillImage;
		[SerializeField] private bool _isEmptyAtStart = true;
		[SerializeField] private float _updateFullDuration;

		private Tween _updateFillTween;

		public Image FillImage => _fillImage;

		public void SetDefaultFill()
		{
			KillUpdateTween();
			SetDefault();
		}

		public void SetFill(float newFill, bool animate)
		{
			KillUpdateTween();

			if (animate)
			{
				float currentFill = _fillImage.fillAmount;
				float fillDuration = Mathf.Abs(newFill - currentFill) * _updateFullDuration;

				if (fillDuration >= 2.5f)
					fillDuration = 2.5f;

				_updateFillTween = DOTween.To(fill => _fillImage.fillAmount = fill,
					currentFill, newFill, fillDuration);
			}
			else
			{
				_fillImage.fillAmount = newFill;
			}
		}

		private void SetDefault() => 
			_fillImage.fillAmount = _isEmptyAtStart ? 0f : 1f;

		private void KillUpdateTween()
		{
			if (_updateFillTween != null)
			{
				_updateFillTween?.Kill();
				_updateFillTween = null;
			}
		}
	}
}