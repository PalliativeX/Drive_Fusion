using UnityEngine;

namespace Core.Gameplay.Animations
{
	public sealed class AnimationController : MonoBehaviour
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private AnimationType _initialAnimation;

		private string _currentTrigger;

		public AnimationType InitialAnimation => _initialAnimation;

		public Animator Animator => _animator;

		public void SwitchAnimation(string trigger)
		{
			if (_currentTrigger == trigger)
				return;

			if (_currentTrigger != null)
				_animator.ResetTrigger(_currentTrigger);

			_animator.SetTrigger(trigger);

			_currentTrigger = trigger;
		}

		public void PlayDirectly(string clipName, string triggerName = null)
		{
			if (_currentTrigger != null)
				_animator.ResetTrigger(_currentTrigger);

			_animator.Play(clipName, 0);

			_currentTrigger = triggerName;
		}

		public void SetFloat(string floatName, float newValue) =>
			_animator.SetFloat(floatName, newValue);

		public void ApplyRootMotion(bool active) =>
			_animator.applyRootMotion = active;
		
		#region Auto Assign
		private void OnValidate()
		{
			_animator = GetComponentInChildren<Animator>();
		}
		#endregion
	}
}