using DG.Tweening;
using UnityEngine;

namespace Core.SceneManagement.LoadingScreen
{
	public sealed class LoadingScreenBehaviour : MonoBehaviour
	{
		[Header("Fade")] 
		[SerializeField] private CanvasGroup _group;
		[SerializeField] private float _hideDuration;
		[SerializeField] private Ease _ease;

		public void Show()
		{
			_group.alpha = 1f;
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			_group.DOFade(0f, _hideDuration)
				.From(1f)
				.SetEase(_ease)
				.OnComplete(() => gameObject.SetActive(false));
		}
	}
}