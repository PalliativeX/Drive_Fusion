using UnityEngine;

namespace Utils
{
    // NOTE(vladimir): Space pauses/unpauses the game,
    // PageUp/PageDown speeds up or slows down the time scale in the provided min-max range
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] private int _minTimeScale;
        [SerializeField] private int _maxTimeScale;
        [SerializeField] private float _slowMotionRatio;

        private const int DefaultTimeScale = 1;

        public bool Paused { get; set; }

        private int _currentTimeScale;

        private bool _isSlowMotionActive;

        private void Awake()
        { 
            _currentTimeScale = DefaultTimeScale;
        }

        private void Update()
        {
            #region Debugging

#if UNITY_EDITOR
            // NOTE(vladimir): For debugging on PC
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SwitchPaused(!Paused);
            }
            else if (Input.GetKeyDown(KeyCode.PageUp))
            {
                ChangeTimeScale(1);
            }
            else if (Input.GetKeyDown(KeyCode.PageDown))
            {
                ChangeTimeScale(-1);
            }
#endif

            #endregion
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

        public void SwitchSlowMotionActive(bool active)
        {
            if (_isSlowMotionActive && !active)
            {
                _isSlowMotionActive = false;
                Time.timeScale /= _slowMotionRatio;
            }
            else if (!_isSlowMotionActive && active)
            {
                _isSlowMotionActive = true;
                Time.timeScale *= _slowMotionRatio;
            }
        }
    }
}