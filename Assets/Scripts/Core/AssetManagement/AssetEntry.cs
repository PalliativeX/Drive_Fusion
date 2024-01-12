using System;
using UnityEngine;

namespace Core.AssetManagement
{
	[Serializable]
	public class AssetEntry
	{
		public string Name;
		public GameObject Reference;
		[Min(0)]
		public int PooledCount;
	}
}