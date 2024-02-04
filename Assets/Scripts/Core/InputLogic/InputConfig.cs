using UnityEngine;

namespace Core.InputLogic
{
	[CreateAssetMenu(fileName = nameof(InputConfig), menuName = "Configs/Input/" + nameof(InputConfig))]
	public sealed class InputConfig : ScriptableObject
	{
		[Header("Debug")] 
		public bool SetZManually;
	}
}