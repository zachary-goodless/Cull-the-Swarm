using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	float speed;
	float evasionSpeed;
	float evasionTime;
	float timer;
	float evasionBounds;
	float startX;
	public bool right;
	bool retreat;
	public Vector3 dest;
	public bool destReached;
	public WaveManager wm;

	// Use this for initialization
	void Start () {
		speed = 200;
		evasionSpeed = 150;
		evasionTime = 10;
		timer = 0;
		startX = transform.position.x;
		evasionBounds = 150;
		retreat = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!destReached) {
			transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * speed);
			if (transform.position == dest) {
				destReached = true;
			}
		}
		if (wm.waveSet) {
			Evasion ();
			if (!retreat) {
				if (timer < evasionTime) {
					timer += Time.deltaTime;
				} else {
					retreat = true;
				}
			}
		}
		if (retreat) {
			transform.position += new Vector3 (0, Time.deltaTime * speed, 0);
		}
	}

	void Evasion(){
		if (right) {
			if (transform.position.x < startX + evasionBounds) {
				transform.position += new Vector3 (Time.deltaTime * evasionSpeed, 0, 0);
			} else {
				right = false;
			}
		} else {
			if (transform.position.x > startX - evasionBounds) {
				transform.position += new Vector3 (Time.deltaTime * -1 * evasionSpeed, 0, 0);
			} else {
				right = true;
			}
		}
	}

}
