using UnityEngine;
using System.Collections;

public class BugSprayPrimary : MonoBehaviour {

	public Transform gunR;
	public Transform gunL;
	float shootCool;
	float shootTimer;
    float shotRange;
    float intertia;
	bool cooling;
	public GameObject bullet;
	private Player player;

	float rot;
	float dir;

	// Use this for initialization
	void Start () {
		rot = 0;
        shotRange = 40;
        intertia = 5;
		shootCool = .15f;
		shootTimer = 0;
		cooling = false;
		player = GetComponent<Player> ();
		bullet = Resources.Load ("PlayerBullets/GasTest") as GameObject;
		gunR = transform.Find ("GunR");
		gunL = transform.Find ("GunL");
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		if(!Boss.isOnBossStart)
		{
			if(!Input.GetButton("Precision") && !Input.GetButton("XBOX_LB")){
				dir = Input.GetAxis ("Horizontal");
					
				if (dir == 0) {
					dir = Input.GetAxis ("XBOX_LS_X");
				} 

				if (dir == 0) {
					dir = Input.GetAxis ("XBOX_DP_X");
				}
					
        	    rot = (rot * intertia + shotRange * -dir) / (intertia + 1);
			}
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
	}

	void Shoot(){
		Instantiate (bullet, new Vector3(gunL.position.x, gunL.position.y, 0f), Quaternion.Euler(0f,0f,rot));
		Instantiate (bullet, new Vector3(gunR.position.x, gunL.position.y, 0f), Quaternion.Euler(0f,0f,rot));
	}

	IEnumerator Firing(){
		while((Input.GetButton("Primary") || Input.GetButton("XBOX_RB") || Input.GetButton("XBOX_A")) && !player.dead){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}

}
