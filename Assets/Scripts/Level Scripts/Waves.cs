using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour{
	public GameObject drone;
	public GameObject turret;
	public GameObject wormHead;
	public GameObject WormBod;

	//Either need to get rid of this or update it.
	public GameObject waveManager;
	public WaveManager wm;

	//So far, these waves are specific to a certain kind fo enemy. Need to figure out how to fix that.

	//Bods is # of segments including head, Pos is the position of the head, rot is the rotation of the head
	GameObject MakeWorm(int bods, Vector3 pos, Vector3 rot){
		//Puts the segments behind the proper place;
		Vector3 backPush;
		if (rot.z % 360 <= 45 || rot.z % 360 > 315) {
			backPush = new Vector3 (0, 70, 0);
		} else if (rot.z % 360 <= 135 || rot.z % 360 > 45) {
			backPush = new Vector3 (-70, 0, 0);
		} else if (rot.z % 360 <= 225 || rot.z % 360 > 135) {
			backPush = new Vector3 (0, -70, 0);
		} else {
			backPush = new Vector3 (70, 0, 0);
		}

		GameObject head = Instantiate (wormHead, pos, Quaternion.Euler (rot)) as GameObject;
		WormScript ws = head.GetComponent <WormScript>();
		for (int i = 1; i < bods; i++) {
			GameObject bod = Instantiate(WormBod, pos + backPush*i, Quaternion.Euler(rot)) as GameObject;
			ws.segments.Add (bod.transform);
		}
		return head;
	}

	IEnumerator TestGround(){
		//initialize stuff
		GameObject temp;
		EnemyMovement em;

		temp = MakeWorm (6, new Vector3 (1000, 0, 0),new Vector3(0,0,270));
		em = temp.GetComponent<EnemyMovement> ();
		em.followNose = true;
		em.hasDest = false;
		em.hasRange = true;
		em.rotInc = .5f;
		em.rotRange = 40;
		em.dirY = 1;

		yield break;
	}

	public void StartTest(){
		StartCoroutine ("TestGround");
	}

	//Space invaders-style wave 
	IEnumerator WaveOne(){

		GameObject temp;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;

		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		EnemyMovement em;

		//Set up wavemanager
		wm.hasDest = true;
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;

		//Start spawns
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		em.flightTime = 2;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		em.flightTime = 2;

		temp = Instantiate (drone, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		em.flightTime = 2;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		em.flightTime = 2;

		temp = Instantiate (drone, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		em.flightTime = 2;

		yield break;
	}

	public void StartOne(){
		StartCoroutine ("WaveOne");
	}

	//Left side sloping wave that travels down while moving left and right
	IEnumerator WaveTwo(){

		GameObject temp;
		EnemyMovement em;

		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();

		wm.hasDest = false;
		wm.right = true;

		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.wm = wm;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-450, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-150, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;
		wm.waveSet = true;

		yield break;

	}

	public void StartTwo(){
		StartCoroutine ("WaveTwo");
	}

	//Right side sloping wave that travels down while moving left and right
	IEnumerator WaveTwoPointFive(){

		GameObject temp;
		EnemyMovement em;

		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();

		wm.hasDest = false;
		wm.right = false;

		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (450, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (150, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;
		wm.waveSet = true;

		yield break;
	}

	public void StartTwoPointFive(){
		StartCoroutine ("WaveTwoPointFive");
	}

	//3 turrets travel down screen
	IEnumerator TurretWaveOne(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		yield break;
	}

	public void StartTurretWaveOne(){
		StartCoroutine ("TurretWaveOne");
	}

	//Double sine wave from each side. Right now they collide with eachother; should I fix this?
	IEnumerator DroneSineWave(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (1000, 200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1000, -200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		temp = Instantiate (drone, new Vector3 (1200, 200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1200, -200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		yield return new WaitForSeconds(3f);
		temp = Instantiate (drone, new Vector3 (1000, 200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1000, -200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		temp = Instantiate (drone, new Vector3 (1200, 200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1200, -200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		yield return new WaitForSeconds(3f);
		temp = Instantiate (drone, new Vector3 (1000, 200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1000, -200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		temp = Instantiate (drone, new Vector3 (1200, 200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1200, -200, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		yield break;
	}
	public void StartDroneSineWave(){
		StartCoroutine ("DroneSineWave");
	}

	//Alternating from left and right top corners to right and left bottom corners
	IEnumerator DroneCrossWave(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield break;
	}
	public void StartDroneCrossWave(){
		StartCoroutine ("DroneCrossWave");
	}

	//Spawns drones in a semicircle at the top of the map0
	IEnumerator DronesInnerCircle(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = .8f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (50, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-50, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (100, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-100, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (150, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-150, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		yield break;
	}
	public void StartDronesInnerCircle(){
		StartCoroutine ("DronesInnerCircle");
	}

	//spawns drones in quarter circles on the upper left and right corners
	IEnumerator DronesCornerCircles(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		yield break;
	}
	public void StartDronesCornerCircles(){
		StartCoroutine ("DronesCornerCircles");
	}

	//spawns turrets every 2 seconds on alternating sides of the screen
	IEnumerator AlternateTurrets(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield break;
	}

	public void StartAlternateTurrets(){
		StartCoroutine ("AlternateTurrets");
	}

	//Two collumns of turrets surrounded by drones
	IEnumerator DronesAndTurrets(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield break;
	}

	public void StartDronesAndTurrets(){
		StartCoroutine ("DronesAndTurrets");
	}
}
