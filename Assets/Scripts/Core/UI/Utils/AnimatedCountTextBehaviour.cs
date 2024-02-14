using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;

namespace Core.UI
{
	public sealed class AnimatedCountTextBehaviour : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _text;

		public void SetCount(int newCount, bool animate)
		{
			transform.DOKill();

			if (!animate)
			{
				_text.SetText(newCount.ToString());
				return;
			}
				
			int initialCount = int.Parse(_text.text);

			TextAnimationUtils.AnimateTextAndGetTween(
				_text,
				initialCount,
				newCount,
				TextAnimationUtils.DEFAULT_TEXT_ANIMATION_SPEED,
				true
			);
		}
	}
}