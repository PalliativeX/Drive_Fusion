using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(InteractiveItemsConfig), menuName = "Configs/" + nameof(InteractiveItemsConfig))]
	public sealed class InteractiveItemsConfig : ScriptableObject
	{
		public List<InteractiveItemEntry> Entries;
		public List<float> CoinOffsets;
		public float CoinYOffset;

		public int SkipInitialBlocksCount;
	}

	[Serializable]
	public class InteractiveItemEntry
	{
		public InteractiveType Type;
		public List<string> PrefabNames;
	}
}