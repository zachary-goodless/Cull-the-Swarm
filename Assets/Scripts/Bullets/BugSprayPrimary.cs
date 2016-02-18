using UnityEngine;
using System.Collections;

public class BugSprayPrimary : MonoBehaviour {

	public Transform gunR;
	public Transform gunL;
	float shootCool;
	float shootTimer;
	bool cooling;
	public GameObject bullet;
	private Player player;

	float rot;
	float dir;
	float incAmount;

	// Use this for initialization
	void Start () {
		rot = 0;
		incAmount = 5;
		shootCool = .075f;
		shootTimer = 0;
		cooling = false;
		player = GetComponent<Player> ();
		bullet = Resources.Load ("PlayerBullets/SwatterTest") as GameObject;
		gunR = transform.Find ("GunR");
		gunL = transform.Find ("GunL");
	}
	
	// Update is called once per frame
	void Update () {
		dir = Input.GetAxis ("Horizontal");
		if (dir != 0 && Mathf.Abs (rot) < 45){
			rot += dir * incAmount;
		}
		else{
			rot = 0;
		}
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
		GameObject temp = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
	}

	IEnumerator Firing(){
		while(Input.GetButton("Primary") && !player.dead){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}

}
