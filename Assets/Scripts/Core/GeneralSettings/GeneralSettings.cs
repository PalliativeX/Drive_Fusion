using UnityEngine;

namespace Core
{
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Configs/GameSettings")]
	public sealed class GeneralSettings : ScriptableObject
	{
		public bool DisableVsync = true;
		public int TargetFrameRate = 60;
		
		public bool IsEnableMultitouch;
	}
}