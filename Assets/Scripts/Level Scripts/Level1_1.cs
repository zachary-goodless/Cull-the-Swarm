using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1_1 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;

	void Start () {
		StartCoroutine ("LevelLayout2");
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			waves.StartTest ();
		}
		if (sf.finished) {
			SceneManager.LoadScene ((int)SceneIndex.WORLD_MAP);
		}
	}

	IEnumerator LevelLayout(){
        
		yield return new WaitForSeconds (5f);
		waves.StartOne ();
		yield return new WaitForSeconds (13f);
		waves.StartTwo();
		yield return new WaitForSeconds (5f);
		waves.StartTwoPointFive();
		yield return new WaitForSeconds (5f);
		waves.StartTwo();
		yield return new WaitForSeconds (5f);
		waves.StartTwoPointFive();
		yield return new WaitForSeconds (5f);
        waves.StartOne();
        yield return new WaitForSeconds(8f);
        waves.StartTurretWaveOne ();
		yield return new WaitForSeconds (4f);
		waves.StartDroneSineWave ();
		yield return new WaitForSeconds (15f);
		waves.StartDroneCrossWave ();
		yield return new WaitForSeconds (10f);
		waves.StartDronesAndTurrets ();
		yield return new WaitForSeconds (10f);
		waves.StartDronesInnerCircle ();
		waves.StartDronesCornerCircles ();
		waves.StartAlternateTurrets ();
		yield return new WaitForSeconds (2f);
		waves.StartDronesInnerCircle ();
		waves.StartDronesCornerCircles ();
		yield return new WaitForSeconds (2f);
		waves.StartDronesInnerCircle ();
		waves.StartDronesCornerCircles ();
		yield return new WaitForSeconds (4f);
		waves.StartDronesInnerCircle ();
		waves.StartDronesCornerCircles ();
		yield return new WaitForSeconds (2f);
		waves.StartDronesInnerCircle ();
		waves.StartDronesCornerCircles ();
		yield return new WaitForSeconds (2f);
		waves.StartDronesInnerCircle ();
		waves.StartDronesCornerCircles ();
		yield return new WaitForSeconds (16f);
		sf.Fade ();
		yield break;
	}

	IEnumerator LevelLayout2(){

		yield return new WaitForSeconds (5f);
		waves.StartDoubleWorms ();
		yield break;
	}

}
