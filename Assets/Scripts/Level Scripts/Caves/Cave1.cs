using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Cave1 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;

	public LevelCompleteHandler finished;

	public GameObject drone;
	public GameObject missile;
	public GameObject turret;
	public GameObject snail;

	public DialogueBox dialog;

	void Start () {
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
		StartCoroutine ("LevelLayout");
	}

	IEnumerator LevelLayout(){

		//JUSTIN
		Coroutine co;

		StartCoroutine(sf.FadeFromBlack());
		yield return new WaitForSeconds(2f);

		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.STAMPER, "A bit dank in here, you think?"));
		yield return dialog.WaitForSecondsOrSkip(1.5f, co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "No kidding..."));
		yield return dialog.WaitForSecondsOrSkip(1f, co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "Intel reports that the invasion is trying to build a hive in these caverns. That would be problematic for us."));
		yield return dialog.WaitForSecondsOrSkip(3f, co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "You know what to do."));
		yield return dialog.WaitForSecondsOrSkip(1.5f, co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "Shoot bugs?"));
		yield return dialog.WaitForSecondsOrSkip(1f, co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.STAMPER, "Shoot bugs."));
		yield return dialog.WaitForSecondsOrSkip(1f, co);
		//JUSTIN

		//Wave One- One worm, straight down center to bottom.
		waves.StartSpawnWorms (1, 6, 0, .5f, 40, 200, 1, 300, 0, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d1");
		yield return new WaitForSeconds (3f);

		//Wave Two (3 sec after)- 3 snails, lined in a row, left side of the screen moving linearly towards the bottom of the screen
		waves.StartSpawnLinear (snail, 3, 0, 200, 0, -1, 120, -700, waves.upScreen, Quaternion.identity, 200, 0, "d4");
		yield return new WaitForSeconds (1f);

		//Wave Three (1 sec after)- one turret, lined behind the three drone wave.
		waves.StartSpawnLinear (turret, 1, 0, 200, 0, -1, 150, -500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
		yield return new WaitForSeconds (3f);

		//Wave Four ( 3 sec after)- 3 snails lined in a row, right side of the screen, moving linearly towards the bottom of the screen
		waves.StartSpawnLinear (snail, 3, 0, 200, 0, -1, 120, 700, waves.upScreen, Quaternion.identity, -200, 0, "d4");
		yield return new WaitForSeconds (1f);

		//Wave Five (1 sec after)- one turret, lined behind the three drone wave, linearly towards the bottom of the screen.
		waves.StartSpawnLinear (turret, 1, 0, 200, 0, -1, 150, 500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
		yield return new WaitForSeconds (3f);

		//Wave Six (3 sec after)- 3 snails lined in a row, middle of the screen, moving linearly towards the bottom of the screen
		waves.StartSpawnLinear (snail, 3, 0, 200, 0, -1, 120, -200, waves.upScreen, Quaternion.identity, 200, 0, "d4");
		yield return new WaitForSeconds (1f);


		waves.StartSpawnLinear (turret, 1, 0, 200, 0, -1, 150, 0, waves.upScreen, Quaternion.identity, 0, 0, "t1");
		yield return new WaitForSeconds (6f);

		//Wave Seven (6 sec after)-  2 turrets, evenly spaced around the middle of the screen
		waves.StartSpawnLinear (turret, 2, 0, 200, 0, -1, 150, -400, waves.upScreen, Quaternion.identity, 800, 0, "t1");
		yield return new WaitForSeconds (3f);

		//Wave Eight (3 sec after)-  5 drones curve from the outside left of the screen towards the center and exit on the right
		waves.StartSpawnSideToBottom (drone, 5, 1.5f, 7, 250, 1, -1, 100, waves.leftScreen, 200, Quaternion.identity, 0, 0, "d1");
		yield return new WaitForSeconds (3f);

		//Wave Eight (3 sec after)- 5 drones curve from the outside right of the screen towards the center and exit on the left
		waves.StartSpawnSideToBottom (drone, 5, 1.5f, 7, 250, -1, 1, 100, waves.rightScreen, -200, Quaternion.identity, 0, 0, "d1");
		yield return new WaitForSeconds (8f);

		//Wave Nine (8 sec after)- 4 drones from background to left side of the screen towards upper center of the screen, exit off the right side of the screen.
		waves.StartSpawnFromBackground (drone, 4, 1, 2, new Vector3 (0, waves.upScreen - 200, 50), 200, .5f, -1, 100, -600, -400, Quaternion.identity, 0, 200, "d1");
		yield return new WaitForSeconds (8f);

		//Wave Nine (8 sec after)- 4 drones from background to right side of the screen towards upper center of the screen, exit off the left side of the screen.
		waves.StartSpawnFromBackground (drone, 4, 1, 2, new Vector3 (0, waves.upScreen - 200, 50), 200, -.5f, -1, 100, 600, -400, Quaternion.identity, 0, 200, "d1");
		yield return new WaitForSeconds (4f);

		//Wave Ten (4 sec after)- 1 worm from left side of the screen slinks linearly across to the right. Upper portion of the screen.
		waves.StartSpawnWorms (1, 6, 0, .5f, 30, 200, 1, 200, waves.leftScreen, 200, Quaternion.Euler (0, 0, 90), 0, 0, "d4");
		yield return new WaitForSeconds (4f);

		//Wave Eleven (4 sec after)- 1 worm from right side of the screen slinks linearly across to the left.
		waves.StartSpawnWorms (1, 6, 0, .5f, 30, 200, 1, 200, waves.rightScreen, -200, Quaternion.Euler (0, 0, -90), 0, 0, "d4");
		yield return new WaitForSeconds (8f);

		//Wave Twelve (8 sec after)-  2 worm from top left side of the screen move linearly towards bottom of the screen.
		waves.StartSpawnWorms (2, 6, 0, .5f, 40, 250, 1, 200, -600, waves.upScreen, Quaternion.Euler (0, 0, 0), 300, 0, "d1");
		yield return new WaitForSeconds (4f);

		//Wave Thirteen (4 sec after)- 2 worms from top right side of the screen move linearly towards bottom of the screen.
		waves.StartSpawnWorms (2, 6, 0, -.5f, 40, 250, 1, 200, 600, waves.upScreen, Quaternion.Euler (0, 0, 0), -300, 0, "d1");
		yield return new WaitForSeconds (4f);

		//Wave Fourteen (4 sec after)- 2 worms from center of screen move linearly towards bottom of screen.
		waves.StartSpawnWorms (1, 6, 0, .5f, 40, 250, 1, 200, -150, waves.upScreen, Quaternion.Euler (0, 0, 0), 300, 0, "d1");
		waves.StartSpawnWorms (1, 6, 0, -.5f, 40, 250, 1, 200, 150, waves.upScreen, Quaternion.Euler (0, 0, 0), 300, 0, "d1");


		//Totally thought this said worms in the document; saving them since they work well for the angle they're using

		/*
		yield return new WaitForSeconds (5f);
		waves.StartSpawnWorms (3, 6, 0, .5f, 30, 250, 1, 250, waves.leftScreen+600, waves.downScreen-200, Quaternion.Euler (0, 0, 120), -200, 200, "");
		yield return new WaitForSeconds (5f);
		waves.StartSpawnWorms (3, 6, 0, -.5f, 30, 250, 1, 250, waves.rightScreen-600, waves.downScreen-200, Quaternion.Euler (0, 0, 225), 200, 200, "");
		*/

		yield return new WaitForSeconds (5f);

		//Wave Fifteen (5 sec after)- 3 drone from left bottom of the screen move linearly towards the top right side of the screen.
		waves.StartSpawnLinear (drone, 3, 0, 250, .7f, .5f, 100, waves.leftScreen-400, waves.downScreen - 200, Quaternion.identity, 200, -200, "d1");
		yield return new WaitForSeconds (5f);

		//Wave Sixteen (5 sec after)- 3 drones from right bottom of the screen move linearly towards the top left side of the screen.
		waves.StartSpawnLinear (drone, 3, 0, 250, -.7f, .5f, 100, waves.rightScreen+400, waves.downScreen - 200, Quaternion.identity, -200, -200, "d1");
		yield return new WaitForSeconds (4f);

		//Wave Seventeen (4 sec after)- 3 turrets spawn sequentially at the center of the screen and move linearly towards the bottom of the screen.
		waves.StartSpawnLinear (turret, 3, 1.5f, 200, 0, -1, 150, 0, waves.upScreen, Quaternion.identity, 0, 0, "t2");
		yield return new WaitForSeconds (3f);

		//Wave Nineteen (3 sec after)- 4 drones from background to left top side of the screen and move to the bottom right side of the screen.
		waves.StartSpawnFromBackground (drone, 4, 1, 2.5f, new Vector3 (-600, 400, 50), 250, 1, -1, 100, -650, -300, Quaternion.identity, 0, 0, "d3");
		yield return new WaitForSeconds (3f);

		//Wave Twenty (3 sec after)- 4 drones from background to right top side of the screen and move to the bottom left side of the screen.
		waves.StartSpawnFromBackground (drone, 4, 1, 2.5f, new Vector3 (600, 400, 50), 250, -1, -1, 100, 650, -300, Quaternion.identity, 0, 0, "d3");

		yield return new WaitForSeconds (16f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "There. That should get them off your back for a little while."));
		yield return dialog.WaitForSecondsOrSkip(3f, co);

		sf.Fade();
		yield return new WaitForSeconds(2f);
		//JUSTIN

		finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
		yield break;
	}
}
