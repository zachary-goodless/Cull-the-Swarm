using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	float speed;

	float lifeTimer;

	// Use this for initialization
	void Start () {
		speed = 500;
		lifeTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, Time.deltaTime * speed, 0);
		lifeTimer += Time.deltaTime;
		if (lifeTimer > 8) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate(){

	}

	void OnTriggerEnter2D (){

	}
}
