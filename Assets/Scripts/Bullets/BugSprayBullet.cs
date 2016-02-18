using UnityEngine;
using System.Collections;

public class BugSprayBullet : MonoBehaviour {

	SpriteRenderer sr;
	float speed;
	float rot;
	float slow;

	// Use this for initialization
	void Start () {
		sr = GetComponentInChildren<SpriteRenderer> ();
		rot = transform.eulerAngles.z;
		speed = 500;
		slow = 1;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.up * Time.deltaTime * slow * speed;

	}


}
