using SimpleInject;
using UnityEngine;

namespace Core.AssetManagement
{
	public sealed class AssetProvider : IAssetProvider
	{
		private readonly AssetPool _pool;

		public AssetProvider(AssetPool pool) => _pool = pool;

		public (GameObject, bool isPooled) LoadAsset(string assetName)
		{
			(GameObject instantiated, bool isPooled) = _pool.Get(assetName, null);
			instantiated.SetActive(true);
			return (instantiated, isPooled);
		}

		public (T, bool isPooled) LoadAsset<T>(string assetName)
		{
			(GameObject instantiated, bool isPooled) = LoadAsset(assetName);
			return (instantiated.GetComponent<T>(), isPooled);
		}

		public (GameObject, bool isPooled) LoadAsset(string assetName, Vector3 position, Vector3 rotationEuler)
		{
			(GameObject instantiated, bool isPooled) = LoadAsset(assetName);
			instantiated.transform.position = position;
			instantiated.transform.eulerAngles = rotationEuler;
			return (instantiated, isPooled);
		}
	}
}