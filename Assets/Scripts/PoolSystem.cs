using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    List<GameObject> list;
    public PoolSystem(int size, GameObject prefab)
    {
        list = new List<GameObject>();
        for (int i = 0; i < size; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.SetActive(false);
            list.Add(obj);
        }
    }

    // Returns
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

    public void Recycle(GameObject obj)
    {
        list.Add(obj);
        obj.SetActive(false);
    }
}
