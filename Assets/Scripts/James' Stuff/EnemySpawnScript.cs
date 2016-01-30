using UnityEngine;
using System.Collections;

public class EnemySpawnScript : MonoBehaviour {

	public GameObject Jerry;

	float timer;
	float spawnTime;

	// Use this for initialization
	void Start () {
		spawnTime = Random.Range (10, 15);
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < spawnTime) {
			timer += Time.deltaTime;
		} else {
			float rand = Random.Range (0, 5);
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
			spawnTime = Random.Range (15, 20);
		}
	}

	IEnumerator SpawnPattern1(){
		Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity);
		yield break;
	}

	IEnumerator SpawnPattern2(){
		Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity);
		yield break;
	}

	IEnumerator SpawnPattern3(){
		Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.8f);
		Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.8f);
		Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity);
		yield break;
	}

	IEnumerator SpawnPattern4(){
		Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.8f);
		Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.8f);
		Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity);
		yield break;
	}

	IEnumerator SpawnPattern5(){
		Instantiate (Jerry, new Vector3 (-250, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (250, transform.position.y, 0), Quaternion.identity);
		yield return new WaitForSeconds(.5f);
		Instantiate (Jerry, new Vector3 (-500, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (0, transform.position.y, 0), Quaternion.identity);
		Instantiate (Jerry, new Vector3 (500, transform.position.y, 0), Quaternion.identity);
		yield break;
	}
}
