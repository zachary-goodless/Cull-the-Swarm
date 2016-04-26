using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Jungle_1 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;
	public LevelCompleteHandler finished;
	public GameObject drone;
	public GameObject missile;
	public GameObject turret;

	public DialogueBox dialog;

	void Start ()
	{
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
		StartCoroutine ("LevelLayout");
	}

	IEnumerator LevelLayout(){

		//JUSTIN
		Coroutine co;

		StartCoroutine(sf.FadeFromBlack());
		yield return new WaitForSeconds(2f);

		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "Intel reports that the swarm has damaged most of our satelite equipment in this area."));
		yield return dialog.WaitForSecondsOrSkip(3f, co);
		co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.MARTHA, "We need you to go in and stop them so we can salvage what we can."));
		yield return dialog.WaitForSecondsOrSkip(2.5f, co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Right. I'll be back by morning."));
		yield return dialog.WaitForSecondsOrSkip(1.5f, co);
		//JUSTIN

		waves.StartSpawnWorms (2, 5, 3, .5f, 30, 200, 1, 50, waves.leftScreen, 100, Quaternion.Euler (0, 0, 90), 0, -200, "d1");
		waves.StartSpawnWorms (2, 5, 3, .5f, 30, -200, -1, 50, waves.rightScreen, 200, Quaternion.Euler (0, 0, -90), 0, -200, "d1");

		yield return new WaitForSeconds (3f);
		//example waves.StartSpawnFollow (turret, 3, 4, .5f, 30, 150, -1, 200, 5, waves.downScreen, Quaternion.identity, 0, 0,"t3");
		waves.StartSpawnLinear (turret, 3, 4, 100f, 0f, -1f, 50f, 0f, waves.upScreen, Quaternion.identity, 0f, -1f, "t1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnLinear (turret, 3, 5, 100f, 0f, -1f, 50f, -400, waves.upScreen, Quaternion.identity, 0f, -1f, "t1");
		waves.StartSpawnLinear (turret, 3, 5, 100f, 0f, -1f, 50f, 400, waves.upScreen, Quaternion.identity, 0f, -1f, "t1");
	//	yield return new WaitForSeconds (1f);
	//	waves.StartSpawnLinear (turret, 3, 5, 100f, 0f, -1f, 50f, 400, waves.upScreen, Quaternion.identity, 0f, -1f, "t3");

		yield return new WaitForSeconds (10f);
		waves.StartSpawnDive (drone, 2, 2, 600, 1, 200, 10, -1, 20, waves.leftScreen, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnDive (drone, 2, 2, 600, 1, 200, -10, -1, 20, waves.rightScreen, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (8f);
		waves.StartSpawnFollow (turret, 4, 1, 1f, 30, 250, 1, 50, -300, waves.upScreen, Quaternion.identity, 0, 0, "t2");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnDive (drone, 4, 2, 600, 1, 200, -10, -1, 20, waves.rightScreen, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnLinear (drone, 3, 0, 200, 0, -1, 120, 400, waves.upScreen, Quaternion.identity, 200, 0, "d1");

		yield return new WaitForSeconds (5f);
		waves.StartSpawnFollow (turret, 4, 1, 1f, 30, 250, 1, 50, 300, waves.upScreen, Quaternion.identity, 0, 0, "t2");
		// not here waves.StartSpawnWorms (1, 10, 2, .5f, 30, 200, 1, 200, waves.leftScreen, 300, Quaternion.Euler (0, 0, 90), 0, 0, "d4");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnDive (drone, 2, 2, 600, 1, 200, -10, -1, 20, waves.leftScreen, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnLinear (drone, 3, 0, 200, 0, -1, 120, -500, waves.upScreen, Quaternion.identity, 200, 0, "d1");
		//
		yield return new WaitForSeconds (8f);
		waves.StartSpawnLinear (drone, 5, .5f, 300, 1, -1, 50, -100, waves.upScreen, Quaternion.identity, 0, 0, "d1");
		waves.StartSpawnLinear (drone, 5, .5f, -300, 1, -1, 50, 100, waves.downScreen, Quaternion.identity, 0, 0, "d1");
		//waves.StartSpawnOsc(drone, 2, 3, 5, 5, false, true
		//waves.StartSpawnSin (drone, 2, 3, 10, 5, 300, 0, 0, 50, 200, 300, Quaternion.identity, 0, 0, "d4");
		//
		yield return new WaitForSeconds (5f);
		waves.StartSpawnLinear (drone, 5, .5f, -300, 1, 1, 50, 100, waves.upScreen, Quaternion.identity, 0, 0, "d1");
		waves.StartSpawnLinear (drone, 5, .5f, 300, 1, 1, 50, -100, waves.downScreen, Quaternion.identity, 0, 0, "d1");
       // 
		yield return new WaitForSeconds(1f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, -200, waves.downScreen, Quaternion.Euler (0, 0, 180), 0, 0, "d4");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 200, waves.downScreen, Quaternion.Euler (0, 0, 180), 0, 0, "d4");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, -400, waves.downScreen, Quaternion.Euler (0, 0, 180), 0, 0, "d4");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 400, waves.downScreen, Quaternion.Euler (0, 0, 180), 0, 0, "d4");
		//*/
		yield return new WaitForSeconds (8f);
		waves.StartSpawnSideToBottom (drone, 5, 1.5f, 7, 250, -1, 1, 100, waves.rightScreen, -200, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (8f);
		waves.StartSpawnFromBackground (drone, 4, 1, 2, new Vector3 (0, waves.upScreen - 200, 50), 200, .5f, -1, 100, -600, -400, Quaternion.identity, 0, 200, "d1");
		//waves.StartSpawnFromBackground(drone, 3, 10f, 2f, Vector3.down, 10f, 1, 1, 200, waves.leftScreen, waves.downScreen, Quaternion.identity, 200, 0,"d23");

		yield return new WaitForSeconds (16f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.STAMPER, "You're making steady progress out there, keep at it!"));
		yield return dialog.WaitForSecondsOrSkip(2f, co);

		sf.Fade();
		yield return new WaitForSeconds(2f);
		//JUSTIN

		finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
		yield break;
	}

	IEnumerator LevelLayout2(){
		//Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
		yield return new WaitForSeconds(1f);
		//waves.StartSpawnLinear (drone, 5, 1, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 400, 0);
		//waves.StartSpawnWorms (3, 5, 0, .5f, 30, 300, 1, 50, waves.leftScreen, 200, Quaternion.Euler (0, 0, 90), 0, -200);
		/*yield return new WaitForSeconds (5f);
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
		*/
	}

}
