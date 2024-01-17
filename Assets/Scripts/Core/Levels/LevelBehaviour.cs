using System;
using System.Collections.Generic;
using Core.Gameplay;
using UnityEngine;

namespace Core.Levels
{
	public sealed class LevelBehaviour : MonoBehaviour
	{
		public List<SpawnPoint> SpawnPoints;
	}

	[Serializable]
	public class SpawnPoint
	{
		public Transform Reference;
		public PlayerType PlayerType;
	}
}