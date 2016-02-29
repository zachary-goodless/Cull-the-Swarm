using UnityEngine;
using System.Collections;

public class BugSprayBullet : MonoBehaviour {

	SpriteRenderer sr;
	float speed;
	//float rot;
	//float multiplier;
	float dmg;

	// Use this for initialization
	void Start () {
		sr = GetComponentInChildren<SpriteRenderer> ();
        sr.transform.Rotate(0, 0, Random.Range(0,360));
        //rot = transform.eulerAngles.z;
        speed = 2000;
		dmg = 20;
		//if (rot > 180) {
		//	multiplier = 1 + (Mathf.Abs (rot - 360) / 40);
		//} else {
		//	multiplier = 1 + (rot / 40);
		//}
	}
	
	// Update is called once per frame
	void Update () {

        sr.transform.Rotate(0, 0, 3f);

        if(speed > 1000) {
            speed -= 20;
        }

        transform.position += transform.up * Time.deltaTime * speed;
		if (!sr.isVisible) {
			Destroy (gameObject);
		}

	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "EnemyHit") {
			other.gameObject.GetComponentInParent<EnemyMovement> ().health -= dmg;
			Destroy (gameObject);
		}
	}
		
}
