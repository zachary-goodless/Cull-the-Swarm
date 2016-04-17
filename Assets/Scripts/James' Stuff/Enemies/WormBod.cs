using UnityEngine;
using System.Collections;

public class WormBod : MonoBehaviour {

	public Movement mov;
	public GameObject head;
	public GameObject[] splat;

	MeshRenderer[] mesh;

	// Use this for initialization
	void Start () {
		mov = head.GetComponent<Movement> ();
		mesh = GetComponentsInChildren<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!head) {
			Instantiate (splat [Random.Range (0, splat.Length)], transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
	}

	public void Blink(){
		foreach (MeshRenderer m in mesh) {
			m.material.SetColor ("_Color", Color.red);
		}

		Invoke ("Reveal", .1f);
	}

	void Reveal(){
		foreach (MeshRenderer m in mesh) {
			m.material.SetColor ("_Color", Color.white);
		}
	}

}
