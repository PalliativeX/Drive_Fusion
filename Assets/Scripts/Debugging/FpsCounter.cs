using UnityEngine;

namespace Debugging {
#if DEBUG
	public sealed class FpsCounter : MonoBehaviour {
		private float _currentFps;
		private int _frames;
		private float _accumulator;
		private float _timeLeft;

		public float Fps => _currentFps;

		public void Update() {
			Refresh();
			if (_timeLeft <= 0f)
				Evaluate();
		}

		private void Refresh() {
			var unscaledDeltaTime = Time.unscaledDeltaTime;
			_frames++;
			_accumulator += unscaledDeltaTime;
			_timeLeft -= unscaledDeltaTime;
		}

		private void Evaluate() {
			_currentFps = _accumulator > 0f ? _frames / _accumulator : -1f;
			_frames = 0;
			_accumulator = 0f;
			_timeLeft += 1f;
		}
	}
#else
	public sealed class FpsCounter : MonoBehaviour { }
#endif
}