using UnityEngine;
using System.Collections;

public class SnailScript : MonoBehaviour {

	Movement mov;
	public GameObject shell;
	bool shelled;
	float targetHealth;

	// Use this for initialization
	void Start () {
		mov = GetComponent<Movement> ();
		shelled = true;
		targetHealth = mov.health / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (mov.health <= targetHealth && shelled) {
			DeShell ();
		}
	}

	void DeShell(){
		mov.speed *= 1.8f;
		mov.diveSpeed *= 1.8f;
		Destroy (shell);
		shelled = false;
	}
}
