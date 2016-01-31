using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnScript : MonoBehaviour {

	public GameObject Jerry;
	public GameObject waveManager;
	//public WaveManager wm;

	float timer;
	float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Random.Range (10f, 15f);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < spawnTime) {
			timer += Time.deltaTime;
		} else {
			float rand = Random.Range (0f, 5f);
			if (rand > 4) {
				StartCoroutine ("SpawnPattern1");
			} else if (rand > 3) {
				StartCoroutine ("SpawnPattern2");
			} else if (rand > 2) {
				StartCoroutine ("SpawnPattern3");
			} else if (rand > 1) {
				StartCoroutine ("SpawnPattern4");
			} else {
				StartCoroutine ("SpawnPattern5");
			}
			timer = 0;
			spawnTime = Random.Range (20, 25);
		}
	}

	IEnumerator SpawnPattern1(){
		GameObject temp;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		EnemyMovement em;
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
		temp = Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 300, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 450, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 300, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		yield break;
	}

	IEnumerator SpawnPattern2(){
		GameObject temp;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		EnemyMovement em;
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
		temp = Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		yield break;
	}

	IEnumerator SpawnPattern3(){
		GameObject temp;
		EnemyMovement em;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
		temp = Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.8f);
		temp = Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		temp = Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.8f);
		temp = Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		temp = Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		yield break;
	}

	IEnumerator SpawnPattern4(){
		GameObject temp;
		EnemyMovement em;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
		temp = Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		temp = Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.8f);
		temp = Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		temp = Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.8f);
		temp = Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		yield break;
	}

	IEnumerator SpawnPattern5(){
		GameObject temp;
		EnemyMovement em;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;
		temp = Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		temp = Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		temp =Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		temp = Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
		wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		yield break;
	}
}
