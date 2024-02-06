using UnityEngine;

namespace Core.UI.Revive
{
	public sealed class ReviveModel
	{
		public void SwitchPause(bool paused) => 
			Time.timeScale = paused ? 0f : 1f;
	}
}
