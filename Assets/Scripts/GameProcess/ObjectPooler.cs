using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{

	public GameObject objectToPool;
	public int amountToPool;
	public bool shouldExpand = true;

	public ObjectPoolItem(GameObject obj, int amt, bool exp = true)
	{
		objectToPool = obj;
		amountToPool = Mathf.Max(amt, 2);
		shouldExpand = exp;
	}
}

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler SharedInstance;

	public List<ObjectPoolItem> itemsToPool;
	public List<GameObject> pooledObjects;

	public List<List<GameObject>> pooledObjectsList;
	private List<int> positions;


	public void Awake()
	{
		SharedInstance = this;

		pooledObjectsList = new List<List<GameObject>>();
		pooledObjects = new List<GameObject>();
		positions = new List<int>();

		for (int i = 0; i < itemsToPool.Count; i++)
		{
			ObjectPoolItemToPooledObject(i);
		}
	}

	public GameObject GetReadyToUsePoolObject(int index, Vector3 position, Quaternion rotation)
    {
		var obj = GetPooledObject(index);
		obj.transform.position = position;
		obj.transform.rotation = rotation;
		obj.SetActive(true);
		return obj;
    }

	public GameObject GetPooledObject(int index)
	{
		int curSize = pooledObjectsList[index].Count;
		for (int i = positions[index] + 1; i < positions[index] + pooledObjectsList[index].Count; i++)
		{

			if (!pooledObjectsList[index][i % curSize].activeInHierarchy)
			{
				positions[index] = i % curSize;
				return pooledObjectsList[index][i % curSize];
			}
		}

		if (itemsToPool[index].shouldExpand)
		{
			GameObject obj = (GameObject)Instantiate(itemsToPool[index].objectToPool);
			obj.SetActive(false);
			obj.transform.parent = this.transform;
			pooledObjectsList[index].Add(obj);
			return obj;

		}
		return null;
	}

	public List<GameObject> GetAllPooledObjects(int index)
	{
		return pooledObjectsList[index];
	}

	public int AddObject(GameObject GO, int amt = 3, bool exp = true)
	{
		ObjectPoolItem item = new ObjectPoolItem(GO, amt, exp);
		int currLen = itemsToPool.Count;
		itemsToPool.Add(item);
		ObjectPoolItemToPooledObject(currLen);
		return currLen;
	}

	private void ObjectPoolItemToPooledObject(int index)
	{
		ObjectPoolItem item = itemsToPool[index];

		pooledObjects = new List<GameObject>();
		for (int i = 0; i < item.amountToPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(item.objectToPool);
			obj.SetActive(false);
			obj.transform.parent = this.transform;
			pooledObjects.Add(obj);
		}
		pooledObjectsList.Add(pooledObjects);
		positions.Add(0);

	}
}