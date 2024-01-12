using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
	public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField] private T pooledPrefab;
		[SerializeField] private int objectCountToPool;
		[SerializeField] private Transform _parent;

		private Queue<T> pooledObjects;
        
		private void Awake()
		{
			pooledObjects = new Queue<T>();

			for (int i = 0; i < objectCountToPool; i++)
			{
				T obj = Instantiate(pooledPrefab, transform);
				obj.gameObject.SetActive(false);
				pooledObjects.Enqueue(obj);
			}
		}

		public void PutToPool(T obj)
		{
			obj.gameObject.SetActive(false);
			pooledObjects.Enqueue(obj);
		}

		public T Get(Vector3 pos, Quaternion rot)
		{
			T obj;
            
			if (pooledObjects.Count > 0)
			{
				obj = pooledObjects.Dequeue();
				obj.transform.SetParent(_parent, false);
				obj.transform.position = pos;
				obj.transform.rotation = rot;
				obj.gameObject.SetActive(true);
			}
			else
			{
				obj = Instantiate(pooledPrefab, pos, rot, _parent);
			}

			return obj;
		}
        
	}
}