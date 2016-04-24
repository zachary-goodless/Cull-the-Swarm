using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Cave2 : MonoBehaviour {

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

		co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "You're nearing the heart of the cavern. How are you fairing?"));
		yield return dialog.WaitForSecondsOrSkip(2f, co);
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.ROGER, "Honestly, I'm acclimating well. These bugs aren't for the faint of heart, though."));
		yield return dialog.WaitForSecondsOrSkip(4f, co);
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.STAMPER, "Well, wait until the end of the mission if you're planning on fainting. We've got more monsters up ahead!"));
		yield return dialog.WaitForSecondsOrSkip(4f, co);
		//JUSTIN

		//Wave One- 5 drones sine wave across the screen from left to right.
		waves.StartSpawnSin (drone, 5, 1.5f, 10, 2, 250, 1, 0, 100, waves.leftScreen, -200, Quaternion.identity, 0, 0, "d1");

		//Wave One- 5 drones sine wave across the screen from right to left.
		waves.StartSpawnSin (drone, 5, 1.5f, 10, 2, 250, -1, 0, 100, waves.rightScreen, 200, Quaternion.identity, 0, 0, "d1");
		yield return new WaitForSeconds (8f);

		//Wave Two (3 sec after)- Turret One, left side of the screen. Moves slowly towarsds bottom of the screen.
		waves.StartSpawnLinear (turret, 1, 0, 150, 0, -1, 180, -500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
		yield return new WaitForSeconds (3f);

		//waves.StartSpawnFollow (drone, 4, 0, 0, 0, 250, 1, 100, waves.rightScreen - 300, waves.upScreen + 300, Quaternion.Euler (0, 0, 315), 200, -200, "");
		//Wave Two (3 sec after)- 4 Drones, right side of the screen. Startspawn follow moves linearly towards the bottom of the screen.
		waves.StartSpawnLinear(drone,4,.5f,250,0,-1,100,500,waves.upScreen,Quaternion.identity,-250,0,"d1");
		yield return new WaitForSeconds (3f);

		//Wave Three (3 sec after)- Turret One, right side of the screen. Moves slowly towards bottom of the screen.
		waves.StartSpawnLinear (turret, 1, 0, 150, 0, -1, 180, 500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
		yield return new WaitForSeconds (3f);

		//Wave Three (3 sec after)- 4 Drones, left side of the screen. Startspawn follow moves linearly towards the bottom of the screen.
		waves.StartSpawnLinear(drone,4,.5f,250,0,-1,100,-500,waves.upScreen,Quaternion.identity,250,0,"d1");

		//waves.StartSpawnFollow (drone, 4, 0, 0, 0, 250, 1, 100, waves.leftScreen + 300, waves.upScreen + 300, Quaternion.Euler (0, 0, 315), -200, -200, "");
		yield return new WaitForSeconds (4f);

		//Wave Four (4 sec after)-  1 worm,  moves along the bottom. from left to right.
		waves.StartSpawnWorms (1, 6, 0, .5f, 20, 250, 1, 200, waves.leftScreen, -400, Quaternion.Euler(0,0,90), 0, 0, "d1");
		yield return new WaitForSeconds (3f);

		//Wave Five ( 1 sec after)- 1 worm, moves along the bottom above the original worm, from right to left.
		waves.StartSpawnWorms (1, 6, 0, .5f, 20, 250, 1, 200, waves.rightScreen, -250, Quaternion.Euler(0,0,-90), 0, 0, "d1");
		yield return new WaitForSeconds (1f);

		//Wave Six (1 sec after)- 1 worm, moves along the bottom above Wave Four, from left to right.
		waves.StartSpawnWorms (1, 6, 0, .5f, 20, 250, 1, 200, waves.leftScreen, -100, Quaternion.Euler(0,0,90), 0, 0, "d1");
		yield return new WaitForSeconds (2f);

		//Wave Seven (2 sec after)- 4 drones moving in a sine wave across the left to right at the top of the screen.
		waves.StartSpawnSin (drone, 4, 1.5f, 10, 2, 250, 1, 0, 400, waves.leftScreen, -200, Quaternion.identity, 0, 0, "d1");
		yield return new WaitForSeconds (8f);

		//Wave Eight (8 sec after)- 2 drones on the left side of the screen. Move linearly to the bottom of the screen.
		waves.StartSpawnLinear (snail, 2, 0, 200, 0, -1, 100, -500, waves.upScreen, Quaternion.identity, 300, 0, "d4");
		yield return new WaitForSeconds (1f);

		//Wave Nine ( .5-1 sec after)- 1 turret on the left side of the screen. Move linearly to the bottom of the screen
		waves.StartSpawnLinear (turret, 1, 0, 150, 0, -1, 180, -400, waves.upScreen, Quaternion.identity, 0, 0, "t3");
		yield return new WaitForSeconds (3f);

		//Wave Ten (2 sec after)- 2 drones on the middle of the screen. Moves linearly to the bottom of the screen.
		waves.StartSpawnLinear (snail, 2, 0, 200, 0, -1, 100, -150, waves.upScreen, Quaternion.identity, 300, 0, "d4");
		yield return new WaitForSeconds (1f);

		//Wave Eleven (.5-1 sec after)- 1 turret on the middle of the screen behind the 2 drones. moves linearly to the bottom of the screen.
		waves.StartSpawnLinear (turret, 1, 0, 150, 0, -1, 180, 0, waves.upScreen, Quaternion.identity, 0, 0, "t3");
		yield return new WaitForSeconds (3f);

		//Wave Twelve (2 sec after)- 2 drones on the right side of the screen. moves linearly to the bottom of the screen.
		waves.StartSpawnLinear (snail, 2, 0, 200, 0, -1, 100, 500, waves.upScreen, Quaternion.identity, -300, 0, "d4");
		yield return new WaitForSeconds (1f);

		//Wave Thirteen (.5-1 sec after)- 1 turret on the right side of the screen behind the 2 drones. Moves linearly to the bottom of the screen.
		waves.StartSpawnLinear (turret, 1, 0, 150, 0, -1, 180, 400, waves.upScreen, Quaternion.identity, 0, 0, "t3");
		yield return new WaitForSeconds (4f);

		//Wave Fourteen ( 4 sec after)-  3 drones from background, bottom left side of the screen to upper right.
		waves.StartSpawnFromBackground (drone, 3, 1, 2.5f, new Vector3 (-600, -400, 50), 250, 1, 1, 100, -650, 400, Quaternion.identity, 0, 0, "d1");
		yield return new WaitForSeconds (4f);

		//Wave Fifteen (4 sec after) - 3 drones from background, bottom right side of the screen to upper left.
		waves.StartSpawnFromBackground (drone, 3, 1, 2.5f, new Vector3 (600, -400, 50), 250, -1, 1, 100, 650, 400, Quaternion.identity, 0, 0, "d1");
		yield return new WaitForSeconds (3f);

		//Wave Sixteen ( 3 sec after)- 1 worm moves across the top area of the screen from right to left.
		waves.StartSpawnWorms(1,6,0,.5f,30,250,1,200,waves.rightScreen,300,Quaternion.Euler(0,0,-90),0,0,"d2");
		yield return new WaitForSeconds (2f);

		//Wave Sixteen ( 2 sec after) - 1 worm moves across the top of the area of the screen from left to right.
		waves.StartSpawnWorms(1,6,0,.5f,30,250,1,200,waves.leftScreen,150,Quaternion.Euler(0,0,90),0,0,"d3");
		yield return new WaitForSeconds (8f);

		//Wave Seventeen (8 sec after)- column of 2 drones wide, 4 drones tall from the center of the screen.
		waves.StartSpawnFollow(drone,4,.8f,.02f,0,250,1,100,100,waves.upScreen,Quaternion.identity,0,0,"t1");
		waves.StartSpawnFollow(drone,4,.8f,-.02f,0,250,1,100,-100,waves.upScreen,Quaternion.identity,0,0,"t2");
		yield return new WaitForSeconds (4f);

		//Wave Nineteen (4 sec after)- curve of 5  snails from the left top outside of screen that exit near the center of the right side of the screen.
		waves.StartSpawnTopToSide(snail, 5, 1, 5,180,1,-1,120,-600,waves.upScreen,Quaternion.identity,0,0,"d4");
		yield return new WaitForSeconds (3f);
		
		//Wave Twenty (3 sec after)-curve of 5  snails from the right top outside of screen that exit near the center of the left side of the screen.
		waves.StartSpawnTopToSide(snail, 5, 1, 4,180,-1,-1,120,600,waves.upScreen,Quaternion.identity,0,0,"d4");
		yield return new WaitForSeconds (8f);

		//Wave Twenty-One (8 sec after)- 4 drones form background to the center top area. Move linearly to the bottom of the screen.
		waves.StartSpawnFromBackground(drone, 4, 1, 2f, new Vector3(0,350,50), 250, 0, -1, 100, 0,-400,Quaternion.identity,0,0,"d1");
		yield return new WaitForSeconds (4f);
		
		//Wave Twenty-Two (4 sec after)- 4 snails from left top area of the screen, move linearly towards bottom of the screen.
		waves.StartSpawnLinear(snail,4,1.5f,150,0,-1,120,-500,waves.upScreen,Quaternion.identity,0,0,"d4");
		yield return new WaitForSeconds (4f);

		//Wave Twenty-Three (4 sec after)- 4 drones from right top area of the screen, move linearly towards bottom of the screen.
		waves.StartSpawnLinear(snail,4,1.5f,150,0,-1,120,500,waves.upScreen,Quaternion.identity,0,0,"d4");

		yield return new WaitForSeconds (16f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.STAMPER, "You pushed them back again!"));
		yield return dialog.WaitForSecondsOrSkip(1f, co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "The queen's just up ahead, Roger. Stay sharp!"));
		yield return dialog.WaitForSecondsOrSkip(1.5f, co);

		sf.Fade();
		yield return new WaitForSeconds(2f);
		//JUSTIN

		sf.Fade ();
		finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
		yield break;
	}
}