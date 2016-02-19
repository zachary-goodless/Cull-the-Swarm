using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	public bool waveSet;
	public bool hasDest;
	public bool right;
	public float rightNum;
	bool statsSet;
	public List<GameObject> enemies; 

	// Use this for initialization
	void Start () {
		statsSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!waveSet && hasDest) {
			waveSet = checkSet ();
		}
		if (waveSet && !statsSet) {
			if (hasDest) {
				rightNum = Random.Range (-1f, 1f);
				foreach (GameObject enemy in enemies) {
					EnemyMovement em = enemy.GetComponent<EnemyMovement> ();
					if (rightNum > 0) {
						em.right = true;
					} else {
						em.right = false;
					}
					em.lazy = false;

				}
			} else {
				foreach (GameObject enemy in enemies) {
					EnemyMovement em = enemy.GetComponent<EnemyMovement> ();
						em.right = right;
						em.lazy = false;
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
