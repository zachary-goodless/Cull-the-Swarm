using UnityEngine;
using System.Collections;

public class Drones_1_1 : MonoBehaviour {
	//We'll need to assign this with .AddComponent in the spawns. 

	public string pattern;
	bool ongoing;

	// Use this for initialization
	void Start () {
		ongoing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ongoing){
			StartCoroutine (pattern);
		}
	}
	
	//Sput your coroutines here.
	
}
