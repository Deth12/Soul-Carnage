using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectsPool
{
	public string poolName;
    public GameObject obj;
    public Transform parent;

    List<GameObject> objects;

	public void Initialize (string name, GameObject prefab, int amount, Transform p) 
    {		
		poolName = name;
		obj = prefab;
		parent = p;
		objects = new List<GameObject>();
        for(int i = 0; i < amount - 1; i++)
        {
            AddObject();
        }
	}

    private void AddObject() 
    {
		GameObject temp = GameObject.Instantiate(obj.gameObject);
		temp.name = obj.name;
		temp.transform.SetParent(parent);
		objects.Add(temp);
		temp.SetActive(false);
    }

	public GameObject GetObject () 
    {
		for (int i = 0; i < objects.Count; i++) 
        {
			if (objects[i].gameObject.activeSelf == false) 
			    return objects[i];
		}
		GameObject g = objects[0];
		objects.RemoveAt(0);
		objects.Add(g);
		return objects[objects.Count - 1];
	}
}
