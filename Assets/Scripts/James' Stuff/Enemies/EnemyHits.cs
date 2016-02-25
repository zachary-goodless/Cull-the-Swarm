using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHits : MonoBehaviour {

	public float health = 50;

	float lifeTime;
	float timer;
	WaveManager wm;
	public KillCount kc;

	// Use this for initialization
	void Start () {
		lifeTime = 25;
		timer = 0;
		kc = GameObject.Find("KillCount").GetComponent<KillCount>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!wm) {
			wm = GetComponent<EnemyMovement> ().wm;
		}
		if (health <= 0) {
			kc.score++;
			wm.enemies.Remove (gameObject);
			Destroy (gameObject);
		}
		if (timer < lifeTime) {
			timer += Time.deltaTime;
		} else {
			wm.enemies.Remove (gameObject);
			Destroy (gameObject);
		}
	}
}
