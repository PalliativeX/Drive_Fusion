using UnityEngine;

namespace Core.AssetManagement
{
	public interface IAssetProvider
	{
		(GameObject, bool isPooled) LoadAsset(string assetName);
		(GameObject, bool isPooled) LoadAsset(string assetName, Vector3 position, Vector3 rotationEuler);
		(T, bool isPooled) LoadAsset<T>(string assetName);
	}
}