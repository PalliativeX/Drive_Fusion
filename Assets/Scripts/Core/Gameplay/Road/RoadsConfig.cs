using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(RoadsConfig), menuName = "Configs/" + nameof(RoadsConfig))]
	public sealed class RoadsConfig : ScriptableObject
	{
		public float BlockSize = 30f;
		public int ConcurrentBlockCount;

		public List<RoadBlockEntry> Blocks;
	}

	[Serializable]
	public class RoadBlockEntry
	{
		public RoadBlockType Type;
		public string PrefabName;
	}
}