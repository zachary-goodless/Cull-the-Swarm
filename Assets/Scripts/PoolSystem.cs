using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolSystem : MonoBehaviour {
    List<GameObject> list;

    public void Initialize(int size, GameObject prefab)
    {
        Debug.Log("Init pool!");
        list = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.SetActive(false);
            list.Add(obj);
        }
    }

    // Returns an object that is ready to use.
    public GameObject GetRecycledObject()
    {
        if (list.Count > 0)
        {
            GameObject obj = list[0];
            list.RemoveAt(0);
            return obj;
        }
        Debug.Log("Hit pool limit, no usable objects found.");
        return null;
    }

    // Returns an object to the pool.
    public void Recycle(GameObject obj)
    {
        list.Add(obj);
        obj.SetActive(false);
    }
}
