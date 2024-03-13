using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay
{
	[CreateAssetMenu(fileName = nameof(InteractiveItemsConfig), menuName = "Configs/" + nameof(InteractiveItemsConfig))]
	public sealed class InteractiveItemsConfig : ScriptableObject
	{
		public List<InteractiveItemEntry> Entries;
		public List<InteractiveType> ObstacleTypes;
		public List<InteractiveType> RewardTypes;
		
		public List<float> CoinOffsets;
		public float CoinYOffset;

		[Header("Generation Chances")]
		public int SkipInitialBlocksCount;
		
		[Range(0f, 1f)] public float ItemCreationChance;
		[Range(0f, 1f)] public float ObstacleGenerationChance;
	}

	[Serializable]
	public class InteractiveItemEntry
	{
		public InteractiveType Type;
		public List<string> PrefabNames;
	}
}