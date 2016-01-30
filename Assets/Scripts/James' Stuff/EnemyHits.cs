using UnityEngine;
using System.Collections;

public class EnemyHits : MonoBehaviour {

	public float health = 50;

	float lifeTime;
	float timer;

	// Use this for initialization
	void Start () {
		lifeTime = 15;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			Destroy (gameObject);
		}
		if (timer < lifeTime) {
			timer += Time.deltaTime;
		} else {
			Destroy (gameObject);
		}
	}
}
