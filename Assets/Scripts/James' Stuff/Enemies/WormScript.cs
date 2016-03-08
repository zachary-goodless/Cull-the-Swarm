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
	float maxRot;

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
		maxRot = .8f;

		//StartCoroutine ("FollowDelay");
	}
	
	// Update is called once per frame
	void Update () {
		
		Follow ();
	}

	void Follow(){
		target = head.position + head.up*distance;
		foreach (Transform segment in segments) {
			 dir = target - segment.position;
			//angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			//segment.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);


			//segment.LookAt (target, Vector3.forward);

			float formerRot = segment.eulerAngles.z;

			segment.rotation = Quaternion.LookRotation(Vector3.forward,dir); 

			if (segment.eulerAngles.z > formerRot + maxRot) {
				segment.eulerAngles = new Vector3 (0, 0, formerRot + maxRot);
			}
			else if(segment.eulerAngles.z < formerRot - maxRot) {
				segment.eulerAngles = new Vector3 (0, 0, formerRot - maxRot);
			}

			//segment.Rotate (0, 0, 180);

			//segment.position = Vector3.MoveTowards (segment.position,target-dir,speed*Time.deltaTime);

			segment.position = Vector3.MoveTowards (segment.position,target,speed*Time.deltaTime);
			target = segment.position + segment.up *distance*-1;
		}
		/*for(int i = segments.Count - 1; i>0; i--){
			target = segments[i-1].position + segments[i-1].up *distance;
			dir = target - segments[i].position;
			//segments[i].rotation = Quaternion.LookRotation(Vector3.forward,dir);
			//segments[i].Rotate (0, 0, 180);
			segments[i].position = Vector3.MoveTowards (segments[i].position,target,speed*Time.deltaTime);
		}
		target = head.position + head.up*distance;
		dir = target - segments[0].position;
		//segments[0].rotation = Quaternion.LookRotation(Vector3.forward,dir);
		//segments[0].Rotate (0, 0, 180);
		segments[0].position = Vector3.MoveTowards (segments[0].position,target,speed*Time.deltaTime);
		*/
	}

	IEnumerator FollowDelay(){
		while (true) {
			target = head.position + head.up*distance;
			foreach (Transform segment in segments) {
				dir = target - segment.position;
				//angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
				//segment.rotation = Quaternion.AngleAxis (angle + 90, Vector3.forward);


				//segment.LookAt (target, Vector3.forward);

				//Quaternion lookRot
				segment.rotation = Quaternion.LookRotation(Vector3.forward,dir); 
				//segment.rotation = Quaternion.Lerp(segment.rotation,lookRot,Time.deltaTime*smooth);
				segment.Rotate (0, 0, 180);

				segment.position = Vector3.MoveTowards (segment.position,target,speed*Time.deltaTime);
				target = segment.position + segment.up *distance;
				yield return new WaitForSeconds (.00001f);
			}
		}
	}
}
