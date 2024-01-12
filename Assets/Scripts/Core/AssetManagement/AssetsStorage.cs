using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AssetManagement
{
	[CreateAssetMenu(fileName = "AssetsStorage", menuName = "Configs/AssetsStorage")]
	public sealed class AssetsStorage : ScriptableObject
	{
		public List<AssetEntry> Entries;
		
		public GameObject Get(string name)
		{
			foreach (AssetEntry asset in Entries)
				if (asset.Name == name)
					return asset.Reference;

			throw new Exception($"Asset with name '{name}' not found!");
		}
	}
}