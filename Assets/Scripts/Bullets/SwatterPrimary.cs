using UnityEngine;
using System.Collections;

public class SwatterPrimary : MonoBehaviour {

	float shootCool;
	float shootTimer;
	bool cooling;
	public GameObject bullet;
	private Player player;


	// Use this for initialization
	void Start () {
		shootCool = .2f;
		shootTimer = 0;
		cooling = false;
		player = GetComponent<Player> ();
		bullet = Resources.Load ("PlayerBullets/SwatterTest") as GameObject;
	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		if (Input.GetButtonDown ("Primary") && !cooling) {
			StartCoroutine ("Firing");
		}
		if(Input.GetButtonUp("Primary") && !cooling){
			cooling = true;
		}
		if (cooling) {
			if (shootTimer < shootCool) {
				shootTimer += Time.deltaTime;
			} else {
				shootTimer = 0;
				cooling = false;
			}
		}
	}

	void Shoot(){
		GameObject target = FindTarget ();
		if (target != null) {
			GameObject temp = Instantiate (bullet, target.transform.position, Quaternion.identity) as GameObject;
			temp.GetComponent<SwatterBullet> ().target = target;
		} else {
            Vector3 shipPos = transform.position;
            shipPos.y += 120;
            GameObject temp = Instantiate(bullet, shipPos, Quaternion.identity) as GameObject;
            temp.GetComponent<SwatterBullet>().target = null;
        }
	}

	IEnumerator Firing(){
		while(Input.GetButton("Primary") && !player.dead){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}

	GameObject FindTarget(){
		GameObject target = null;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemies) {
			if (target == null) {
				target = enemy;
			} else if(Vector2.Distance(transform.position, enemy.transform.position) < Vector2.Distance(transform.position, target.transform.position)) {
				target = enemy;
			}
		}
		return target;
	}

}
