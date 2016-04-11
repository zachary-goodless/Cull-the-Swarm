using UnityEngine;
using System.Collections;

public class SwatterBullet : MonoBehaviour {
	//This is set up to his the closest thing to the bullet right now; should I set it to go to the closest thing to the player? 
	float speed;
	float dmg;
    float alph;

	float timer;
	float collideTime;

	public GameObject target;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		speed = 400;
		timer = 0;
		dmg = 15;
        alph = 0;
		sr = GetComponentInChildren<SpriteRenderer> ();
		sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,0);
        sr.transform.Rotate(new Vector3(0, 0, Random.Range(-30,30)));
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		Color temp = sr.color;
        if (target != null) {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        temp.a = alph;
        if (timer <= 0.2) {
            alph = timer * 5;
            transform.localScale -= new Vector3(.06f, .06f, 0);
        } else if (timer <= 0.6) {
            alph = 1;
        } else {
            transform.localScale += new Vector3(.02f, .02f, 0);
            alph = 1 - (timer - 0.6f) * 5;
        }

		sr.color = temp;
		timer += Time.deltaTime;
		if (timer >= 1) {
			Destroy (gameObject);
		}
	}
		
	void OnTriggerStay2D (Collider2D other){
		if (other.tag == "EnemyHit" && timer > 0.2 && timer < 0.6) {
			other.gameObject.GetComponentInParent<Movement> ().health -= dmg;
			Destroy (gameObject);
		} 
	}

	void findTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			if (!target) {
				target = enemy;
			} else if(Vector3.Distance(target.transform.position, transform.position) < Vector3.Distance(enemy.transform.position, transform.position)){
				target = enemy;
			}
		}
	}

}
