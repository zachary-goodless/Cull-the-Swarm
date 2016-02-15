using UnityEngine;
using System.Collections;

public class SwatterBullet : MonoBehaviour {

	float speed;
	float dmg;

	GameObject target;

	// Use this for initialization
	void Start () {
		speed = 400;
		dmg = 15;
		findTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			findTarget ();
		}
	
	}

	void findTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			if(false);
		}
	}
}
