using UnityEngine;
using System.Collections;

public class HoloDuplicate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = GameObject.FindObjectWithTag("Player").transform.position;
        Invoke("Destroy(gameObject)",5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
