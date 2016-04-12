using UnityEngine;
using System.Collections;

public class WormBod : MonoBehaviour {

	public Movement mov;
	public GameObject head;

	// Use this for initialization
	void Start () {
		mov = head.GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!head) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
	}

}
