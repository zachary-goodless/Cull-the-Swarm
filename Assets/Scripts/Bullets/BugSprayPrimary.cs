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
		incAmount = 1;
		shootCool = .075f;
		shootTimer = 0;
		cooling = false;
		player = GetComponent<Player> ();
		bullet = Resources.Load ("PlayerBullets/GasTest") as GameObject;
		gunR = transform.Find ("GunR");
		gunL = transform.Find ("GunL");
	}
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetButton("Precision")){
			dir = Input.GetAxis ("Horizontal");
			if (dir != 0){
				if (Mathf.Abs (rot) < 40) {
					rot += dir * -1 * incAmount;
				} else {
					rot = dir * -1 * 40;
				}
			}
			else{
				rot = 0;
			}
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
		Instantiate (bullet, new Vector3(gunL.position.x - 10, gunL.position.y, 0f), Quaternion.Euler(0f,0f,rot));
		Instantiate (bullet, new Vector3(gunL.position.x + 10, gunL.position.y, 0f), Quaternion.Euler(0f,0f,rot));
		Instantiate (bullet, new Vector3(gunR.position.x - 10, gunL.position.y, 0f), Quaternion.Euler(0f,0f,rot));
		Instantiate (bullet, new Vector3(gunR.position.x + 10, gunL.position.y, 0f), Quaternion.Euler(0f,0f,rot));
	}

	IEnumerator Firing(){
		while(Input.GetButton("Primary") && !player.dead){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}

}
