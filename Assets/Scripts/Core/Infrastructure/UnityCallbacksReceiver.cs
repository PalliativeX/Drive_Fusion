using System;
using UnityEngine;

namespace Core.Infrastructure
{
	public sealed class UnityCallbacksReceiver : MonoBehaviour
	{
		public event Action<bool> ApplicationPaused;
		public event Action ApplicationQuit;
		public event Action<bool> ApplicationFocus;
        
		private void OnApplicationPause(bool paused)
		{
			ApplicationPaused?.Invoke(paused);
		}

		private void OnApplicationQuit()
		{
			ApplicationQuit?.Invoke();
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			ApplicationFocus?.Invoke(hasFocus);
		}
	}
}