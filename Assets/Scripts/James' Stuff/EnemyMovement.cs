using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	float speed;

	// Use this for initialization
	void Start () {
		speed = 200;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, Time.deltaTime * speed*-1, 0);
	}
}
