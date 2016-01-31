using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHits : MonoBehaviour {

	public float health = 50;

	float lifeTime;
	float timer;
	WaveManager wm;

	// Use this for initialization
	void Start () {
		lifeTime = 25;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!wm) {
			wm = GetComponent<EnemyMovement> ().wm;
		}
		if (health <= 0) {
			wm.enemies.Remove (gameObject);
			Debug.Log (wm.enemies.Count);
			Destroy (gameObject);
		}
		if (timer < lifeTime) {
			timer += Time.deltaTime;
		} else {
			wm.enemies.Remove (gameObject);
			Debug.Log (wm.enemies.Count);
			Destroy (gameObject);
		}
	}
}
