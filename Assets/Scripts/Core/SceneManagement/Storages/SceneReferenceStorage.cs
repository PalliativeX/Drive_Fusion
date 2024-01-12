using UnityEngine;

namespace Core.SceneManagement.Storages
{
	[CreateAssetMenu(fileName = "SceneReferenceStorage", menuName = "Storages/SceneReferenceStorage")]
	public sealed class SceneReferenceStorage : ScriptableObject
	{
		public SceneReference Gameplay;
	}
}