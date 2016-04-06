using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1_1 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;

	public GameObject drone;
	public GameObject missile;
	public GameObject turret;

	void Start () {
		//StartCoroutine ("LevelLayout2");
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			waves.StartTest ();
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			waves.StartTemplate ();
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
		//Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
		yield return new WaitForSeconds (5f);
		waves.StartDoubleWorms ();
		waves.StartBigW (drone);
		yield return new WaitForSeconds (10f);
		waves.StartLR6Alternate (missile);
		waves.FiveWormsTop ();
		yield return new WaitForSeconds (14f);
		waves.StartFourFromBelow (drone);
		yield return new WaitForSeconds (6.5f);
		waves.StartFlanktasticFour (drone);
		yield return new WaitForSeconds (1.5f);
		waves.FiveFromBeloW (turret);
		waves.TwoWormsBelow ();
		yield return new WaitForSeconds (4f);
		waves.UpLeftRight (missile);
		yield return new WaitForSeconds (8f);
		waves.TopW (drone);
		yield return new WaitForSeconds (1f);
		waves.TopW (drone);
		yield return new WaitForSeconds (1f);
		waves.TopW (drone);
		yield break;
	}

}
