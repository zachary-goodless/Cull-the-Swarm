using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour{
	public GameObject drone;
	public GameObject turret;

	//Either need to get rid of this or update it.
	public GameObject waveManager;
	public WaveManager wm;

	//So far, these waves are specific to a certain kind fo enemy. Need to figure out how to fix that.

	//Space invaders-style wave 
	IEnumerator WaveOne(){
		GameObject temp;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		EnemyMovement em;
		wm.hasDest = true;
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
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
		em.dirY = -.5f;
		temp = Instantiate (turret, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
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

	IEnumerator DronesInnerCircle(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = .8f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (50, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-50, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (100, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-100, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (150, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-150, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1;
		yield break;
	}
	public void StartDronesInnerCircle(){
		StartCoroutine ("DronesInnerCircle");
	}

	IEnumerator DronesCornerCircles(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,60))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,30))) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.rotInc = 0;
		em.dirY = 1f;
		yield break;
	}
	public void StartDronesCornerCircles(){
		StartCoroutine ("DronesCornerCircles");
	}

	//spawns turrests every 2 seconds
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

	IEnumerator DronesAndTurrets(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
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
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
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
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
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
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
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
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
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
		em.dirY = 1f;
		em.rotInc = .05f;
		yield break;
	}

	public void StartDronesAndTurrets(){
		StartCoroutine ("DronesAndTurrets");
	}
}
