using UnityEngine;
using System.Collections;

public class StandardPrimary : MonoBehaviour {

	public Transform gunR;
	public Transform gunL;
	float shootCool;
	float shootTimer;
	bool cooling;
	public GameObject bullet;
	private Player player;

	// Use this for initialization
	void Start () {
		shootCool = .05f;
		shootTimer = 0;
		cooling = false;
		player = GetComponent<Player> ();
		bullet = Resources.Load ("PlayerBullets/BulletPlaceholder") as GameObject;
		gunR = transform.Find ("GunR");
		gunL = transform.Find ("GunL");
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		if ((Input.GetButtonDown ("Primary") || Input.GetButtonDown("XBOX_RB") || Input.GetButtonDown("XBOX_A")) && !cooling) {
			StartCoroutine ("Firing");
		}
		if((Input.GetButtonUp("Primary") || (Input.GetButtonUp("XBOX_RB") && !Input.GetButton("XBOX_A")) || (Input.GetButtonUp("XBOX_A") && !Input.GetButton("XBOX_RB"))) && !cooling){
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
		Instantiate (bullet, gunL.position, Quaternion.identity);
		Instantiate (bullet, gunR.position, Quaternion.identity);
	}

	IEnumerator Firing(){
		while((Input.GetButton("Primary") || Input.GetButton("XBOX_RB") || Input.GetButton("XBOX_A")) && !player.dead){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}
}
