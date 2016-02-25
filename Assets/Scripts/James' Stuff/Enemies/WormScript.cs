using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WormScript : MonoBehaviour {

	public List<Transform> segments;
	Transform head;
	Vector3 target;
	float speed;
	float smooth;
	float distance;

	EnemyMovement em;

	//-------------
	Vector3 dir;
	float angle;
	//-------------

	// Use this for initialization
	void Start () {
		head = transform;
		speed = 600;
		distance = 70;
		smooth = .02f;
	}
	
	// Update is called once per frame
	void Update () {
		target = head.position + head.up*distance;
		foreach (Transform segment in segments) {
			 dir = target - segment.position;
			/*angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			segment.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);
			*/

			//segment.LookAt (target, Vector3.forward);

			//Quaternion lookRot
			segment.rotation = Quaternion.LookRotation(Vector3.forward,dir); 
			//segment.rotation = Quaternion.Lerp(segment.rotation,lookRot,Time.deltaTime*smooth);
			segment.Rotate (0, 0, 180);

			segment.position = Vector3.MoveTowards (segment.position,target,speed*Time.deltaTime);
			target = segment.position + segment.up *distance;
		}
	}
}
