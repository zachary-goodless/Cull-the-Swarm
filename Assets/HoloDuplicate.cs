using UnityEngine;
using System.Collections;

public class HoloDuplicate : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject g = GameObject.FindWithTag("Duplicate");
        transform.position = g.transform.position;
        Invoke("Destroy(gameObject)", 5);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
