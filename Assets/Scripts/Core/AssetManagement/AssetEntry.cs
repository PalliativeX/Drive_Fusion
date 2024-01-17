using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.AssetManagement
{
	[Serializable]
	public class AssetEntry
	{
		public string Name;
		[AssetsOnly]
		public GameObject Reference;
		[Min(0)]
		public int PooledCount;
	}
}