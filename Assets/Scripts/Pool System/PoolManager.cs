using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public Transform root;
	public PoolConfig config;

	public static PoolManager instance;
	private void Awake()
	{
		if(instance != null)
			Destroy(instance);	
		instance = this;
		Initialize();
	}

	public List<ObjectsPool> pools;

	public void Initialize() 
    {
	    pools = new List<ObjectsPool>();
	    GameObject r = new GameObject {name = "---Pools"};
	    foreach(var item in config.pools)
		{
			ObjectsPool p = new ObjectsPool();
			GameObject g = GameObject.FindGameObjectWithTag(item.parentTag);
			if (!g)
			{
				g = new GameObject {name = item.parentTag, tag = item.parentTag};
				g.transform.parent = r.transform;
			}
			p.Initialize(item.poolName, item.prefab, item.amount, g.transform);
			pools.Add(p);
		}
	}

	public GameObject GetObject (string name, Vector3 position, Quaternion rotation) 
	{
		GameObject result = null;
		if (pools != null) 
		{
			for (int i = 0; i < pools.Count; i++) 
			{
				if(string.Compare(pools[i].poolName, name) == 0) 
				{
					result = pools[i].GetObject().gameObject;
					result.transform.position = position;
					result.transform.rotation = rotation;
					result.SetActive (true);
					return result;
				}
			}
		} 
		return result;
	}

	public ObjectsPool GetPool(string poolName)
	{
		return pools.Find(x => x.poolName == poolName);
	}
}
