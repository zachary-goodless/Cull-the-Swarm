using UnityEngine;
using System.Collections;

public class LaserPrimary : MonoBehaviour {

	public Transform gunF;
	public Transform gunB;
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
		bullet = Resources.Load ("PlayerBullets/LaserTest") as GameObject;
		gunF = transform.Find ("GunF");
		gunB = transform.Find ("GunB");
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Primary") && !cooling) {
			Shoot ();
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
		GameObject temp = Instantiate (bullet, gunF.position, Quaternion.identity) as GameObject;
		temp.transform.parent = gunF;
		LaserBullet lb = temp.GetComponent<LaserBullet> ();
		lb.front = true;
		temp = Instantiate (bullet, gunB.position, Quaternion.identity) as GameObject;
		temp.transform.parent = gunB;
		lb = temp.GetComponent<LaserBullet> ();
		lb.front = false;
	}
}
