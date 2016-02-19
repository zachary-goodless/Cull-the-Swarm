using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1_1 : MonoBehaviour {
	//Well, here goes nothin'.
	//I guess I'll try to go for a sort of interchangeable parts deal. The script won't be super dynamic, but it should allow for some copy and pasting when we need it.
	// Use this for initialization


	public GameObject Drone;
	public GameObject turret;

	//Either need to get rid of this or update it.
	public GameObject waveManager;
	public WaveManager wm;

	void Start () {
		StartCoroutine ("LevelLayout");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator LevelLayout(){
		yield return new WaitForSeconds (5f);
		StartCoroutine ("WaveOne");
		yield return new WaitForSeconds (15f);
		StartCoroutine ("WaveTwo");
		yield return new WaitForSeconds (5f);
		StartCoroutine ("WaveTwoPointFive");
		yield return new WaitForSeconds (5f);
		StartCoroutine ("WaveTwo");
		yield return new WaitForSeconds (5f);
		StartCoroutine ("WaveTwoPointFive");
		yield return new WaitForSeconds (5f);
	}
	IEnumerator WaveOne(){
		GameObject temp;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		EnemyMovement em;
		wm.hasDest = true;
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
		temp = Instantiate (Drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		em.flightTime = 2;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		em.flightTime = 2;
		temp = Instantiate (Drone, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		em.flightTime = 2;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		em.flightTime = 2;
		temp = Instantiate (Drone, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		em.flightTime = 2;
		yield break;
	}
	IEnumerator WaveTwo(){
		GameObject temp;
		EnemyMovement em;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		wm.hasDest = false;
		wm.right = true;
		temp = Instantiate (Drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.wm = wm;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (-450, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (-150, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		wm.waveSet = true;
		yield break;

	}
	IEnumerator WaveTwoPointFive(){
		GameObject temp;
		EnemyMovement em;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		wm.hasDest = false;
		wm.right = false;
		temp = Instantiate (Drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (450, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (150, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dir = -1;
		em.oscillate = true;
		wm.waveSet = true;
		yield break;
	}
}
