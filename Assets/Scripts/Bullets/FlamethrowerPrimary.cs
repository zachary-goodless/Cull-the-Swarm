using UnityEngine;
using System.Collections;

public class FlamethrowerPrimary : MonoBehaviour {

	public Transform gunF;
	float shootCool;
	float shootTimer;
	bool cooling;
	public GameObject[] bullet = new GameObject[2];
	private Player player;
	float degrees;
	bool rotRight;

	// Use this for initialization
	void Start () {
		shootCool = .015f;
		shootTimer = 0;
		cooling = false;
		player = GetComponent<Player> ();

		bullet[0] = Resources.Load ("PlayerBullets/FireBullet") as GameObject;
		bullet[1] = Resources.Load ("PlayerBullets/FireBullet2") as GameObject;
		gunF = transform.Find ("GunF");

		degrees = 0;
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

		GameObject temp = Instantiate (bullet[Random.Range(0,bullet.Length)], gunF.position, Quaternion.identity) as GameObject;
		temp.GetComponent<FlamethrowerBullet>().degrees = degrees;
		degrees = (degrees + 15) % 360;


	}

	IEnumerator Firing(){
		while((Input.GetButton("Primary") || Input.GetButton("XBOX_RB") || Input.GetButton("XBOX_A")) && !player.dead){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}
}
