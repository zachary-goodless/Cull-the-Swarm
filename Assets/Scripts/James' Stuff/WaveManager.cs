using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	public bool waveSet;
	bool statsSet;
	public List<GameObject> enemies; 
	public float right;

	// Use this for initialization
	void Start () {
		statsSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!waveSet) {
			waveSet = checkSet ();
		}
		if (waveSet && !statsSet) {
			right = Random.Range (-1f, 1f);
			foreach (GameObject enemy in enemies) {
				EnemyMovement em = enemy.GetComponent<EnemyMovement> ();
				if (right > 0) {
					em.right = true;
				} else {
					em.right = false;
				}

			}
			statsSet = true;
		}
		if (enemies.Count <= 0) {
			Debug.Log("Destroying");
			Destroy (gameObject);
		}
	}

	bool checkSet(){
		foreach (GameObject enemy in enemies) {
			if (!enemy.GetComponent<EnemyMovement> ().destReached) {
				return false;
			}
		}
		return true;
	}
}
