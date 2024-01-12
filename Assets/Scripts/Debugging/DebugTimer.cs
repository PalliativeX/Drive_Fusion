using UnityEngine;

namespace Debugging
{
#if UNITY_EDITOR
	// NOTE(vladimir): Space pauses/unpauses the game,
	// PageUp/PageDown speeds up or slows down the time scale in the provided min-max range
	public sealed class DebugTimer : MonoBehaviour
	{
		[SerializeField] private int _minTimeScale;
		[SerializeField] private int _maxTimeScale;

		private const int DefaultTimeScale = 1;

		private int _currentTimeScale;
		
		public bool Paused { get; set; }

		private void Awake() => _currentTimeScale = DefaultTimeScale;

		private void Update()
		{
			// NOTE(vladimir): For debugging on PC
			if (Input.GetKeyDown(KeyCode.Space))
				SwitchPaused(!Paused);
			else if (Input.GetKeyDown(KeyCode.PageUp))
				ChangeTimeScale(1);
			else if (Input.GetKeyDown(KeyCode.PageDown)) 
				ChangeTimeScale(-1);
		}

		public void SwitchPaused(bool paused)
		{
			Time.timeScale = paused ? 0 : _currentTimeScale;
			Paused = paused;
		}

		public void ChangeTimeScale(int change)
		{
			_currentTimeScale = Mathf.Clamp(_currentTimeScale + change, _minTimeScale, _maxTimeScale);
			Time.timeScale = _currentTimeScale;
		}
	}
#else
    public class DebugTimer : MonoBehaviour { }
#endif
}