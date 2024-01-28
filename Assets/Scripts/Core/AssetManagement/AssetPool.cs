using System.Collections.Generic;
using System.Diagnostics;
using SimpleInject;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Core.AssetManagement
{
	public sealed class AssetPool : IInitializable
	{
		private readonly AssetsStorage _storage;
		private readonly PoolContainer _poolContainer;

		private readonly Dictionary<string, Stack<GameObject>> _pooledObjects;
		
		public AssetPool(AssetsStorage storage, PoolContainer poolContainer)
		{
			_storage = storage;
			_poolContainer = poolContainer;

			_pooledObjects = new Dictionary<string, Stack<GameObject>>();
		}

		public void Initialize()
		{
			foreach (AssetEntry entry in _storage.Entries)
			{
				var stack = new Stack<GameObject>();

				for (int i = 0; i < entry.PooledCount; i++)
				{
					GameObject instantiated = Instantiate(entry.Name, _poolContainer.Transform);
					instantiated.SetActive(false);
					stack.Push(instantiated);
				}

				_pooledObjects[entry.Name] = stack;
			}
			
			LogContents();
		}

		public (GameObject, bool isPooled) Get(string assetName, Transform parent)
		{
			bool isPooled = false;

			if (_pooledObjects.ContainsKey(assetName))
			{
				isPooled = true;
				if (_pooledObjects[assetName].Count > 0)
				{
					var pooled = _pooledObjects[assetName].Pop();
					pooled.transform.SetParent(parent, false);
					return (pooled, true);
				}
			}

			return (Instantiate(assetName, parent), isPooled);
		}

		public void Put(GameObject asset, string assetName)
		{
			asset.SetActive(false);
			_pooledObjects[assetName].Push(asset);
		}

		public GameObject Instantiate(string assetName, Transform parent = null)
		{
			GameObject prefab = _storage.Get(assetName);
			return Object.Instantiate(prefab, parent);
		}

		[Conditional("UNITY_EDITOR")]
		private void LogContents()
		{
			foreach ((string assetName, Stack<GameObject> stack) in _pooledObjects)
				Debug.Log("Instantiating: " + $"<color=green>{assetName}</color>" + 
				          ". Pooled: " + $"<color=yellow>{stack.Count}</color>");
		}
	}
}