using DG.Tweening;
using UnityEngine;

namespace Utils
{
	public sealed class PulsateBehaviour : MonoBehaviour
	{
		[SerializeField] private float _endScale;
		[SerializeField] private float _duration;

		private Tween _animationTween;
		
		public void Pulsate()
		{
			_animationTween?.Kill();

			_animationTween = transform.DOScale(_endScale, _duration)
				.From(1f)
				.SetLoops(-1, LoopType.Yoyo);
		}

		public void Stop()
		{
			_animationTween?.Kill();
			_animationTween = null;
		}
	}
}