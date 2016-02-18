using UnityEngine;
using System.Collections;

public class SwatterBullet : MonoBehaviour {
	//This is set up to his the closest thing to the bullet right now; should I set it to go to the closest thing to the player? 
	float speed;
	float dmg;

	float timer;
	float collideTime;

	public GameObject target;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		speed = 400;
		timer = 0;
		dmg = 5;
		sr = GetComponentInChildren<SpriteRenderer> ();
		sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0);
	}
	
	// Update is called once per frame
	void Update () {
		Color temp = sr.color;
		if(target != null){
			transform.position = Vector2.MoveTowards (transform.position, target.transform.position, speed *  Time.deltaTime);

			transform.localScale += new Vector3 (.02f, .02f, 0);
			if (timer <= 1) {
				temp.a = timer;
			} else {
				temp.a = 1;
			}
		}
		else{
			transform.localScale -= new Vector3 (.02f, .02f, 0);
			if (temp.a - Time.deltaTime >= 0) {
				temp.a -= Time.deltaTime;
			} else {
				temp.a = 0;
			}
		}
		sr.color = temp;
		timer += Time.deltaTime;
		if (timer >= 5) {
			Destroy (gameObject);
		}
	}

<<<<<<< HEAD


	void OnTriggerStay2D (Collider2D other){
		if (other.tag == "EnemyHit" && timer  > 1) {
			other.gameObject.GetComponentInParent<EnemyHits> ().health -= dmg;
			Destroy (gameObject);
=======
	void findTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			if(false);
>>>>>>> origin/master
		}
	}

}
