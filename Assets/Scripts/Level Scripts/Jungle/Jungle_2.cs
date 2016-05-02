using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Jungle_2 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;
	public LevelCompleteHandler finished;
	public GameObject drone;
	public GameObject missile;
	public GameObject turret;
	public GameObject snail; 

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

		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "You should be nearing the satelite equipment, but I can't pinpoint y--*kzzt*--ocation."));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Say again? You're breaking up."));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "Something must be interfer--*kzzt*--our equipmen--*kzzt*"));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Martha? Stamper? Does anyone copy?"));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
		//JUSTIN
		
		yield return new WaitForSeconds (3f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, -400, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d4");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 400, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d1");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 0, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d1");

		yield return new WaitForSeconds (1f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, -300, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d4");

		yield return new WaitForSeconds (1f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 300, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d4");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnLinear (turret, 3, 2, 100, 0, -1, 50, -400, waves.upScreen, Quaternion.identity, 0, 0, "t1");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 200, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d5");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnLinear (turret, 3, 2, 100, 0, -1, 50, 400, waves.upScreen, Quaternion.identity, 0, 0, "t1");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, -200, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d5");

		yield return new WaitForSeconds (6f);
		waves.StartSpawnFollow (drone, 3, 1, .5f, 30, 200, -1, 50, 0, waves.downScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnFollow (drone, 3, 1, .5f, 30, 200, -1, 50, 300, waves.downScreen, Quaternion.identity, 0, 0, "d1");
		//
		yield return new WaitForSeconds (8f);
		waves.StartSpawnRCornerCircle (drone, .5f, 30, 200, 1, 50, "d1");
		waves.StartSpawnLCornerCircle (drone, .5f, 30, 200, 1, 50, "d1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnFollow (drone, 1, 1, .5f, 30, 200, 1, 50, 0, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (1f);
		waves.StartSpawnFollow (drone, 1, 1, .5f, 30, 200, 1, 50, -300, waves.upScreen, Quaternion.identity, 0, 0, "d1");
		waves.StartSpawnFollow (drone, 1, 1, .5f, 30, 200, 1, 50, 300, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnFollow (drone, 1, 1, .5f, 30, 200, 1, 50, -400, waves.upScreen, Quaternion.identity, 0, 0, "d1");
		waves.StartSpawnFollow (drone, 1, 1, .5f, 30, 200, 1, 50, 400, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (.5f);
		waves.StartSpawnLinear (turret, 3, 2, 100, 0, -1, 50, 0, waves.upScreen, Quaternion.identity, 0, 0, "t2");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 150, 1, 100, waves.leftScreen, 300, Quaternion.Euler (0, 0, 90), 0, 0, "d4");
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 150, 1, 100, waves.rightScreen, 300, Quaternion.Euler (0, 0, -90), 0, 0, "d4");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 200, 1, 100, waves.leftScreen, -100, Quaternion.Euler (0, 0, 90), 0, 0, "d5");
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 200, 1, 100, waves.rightScreen, -100, Quaternion.Euler (0, 0, -90), 0, 0, "d5");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnLinear(turret, 3, 3, 100, 0, -1, 50, 0, waves.upScreen, Quaternion.identity, 0, 0, "t1");

		yield return new WaitForSeconds (3f);
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 200, 1, 100, waves.leftScreen, -400, Quaternion.Euler (0, 0, 90), 0, 0, "d5");
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 200, 1, 100, waves.rightScreen, -400, Quaternion.Euler (0, 0, -90), 0, 0, "d5");

		yield return new WaitForSeconds (8f);
		waves.StartSpawnLinear(turret, 3, 3, 100, 0, -1, 50, 0, waves.upScreen, Quaternion.identity, 0, 0, "t2");

		yield return new WaitForSeconds (5f);
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 80, 5, 50, -400, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d4");
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 80, 5, 50, 400, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d4");

		yield return new WaitForSeconds (4f);
		waves.StartSpawnLinear (drone, 4, 1, 200, 0, 1, 50, -400, waves.downScreen, Quaternion.identity, 0, 0, "d5");
		waves.StartSpawnLinear (snail, 4, 1, 200, 0, -1, 50, 400, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (8f);
		waves.StartSpawnLinear (drone, 4, 1, 200, 0, 1, 50, 200, waves.downScreen, Quaternion.identity, 0, 0, "d5");
		waves.StartSpawnLinear (snail, 4, 1, 200, 0, -1, 50, -200, waves.upScreen, Quaternion.identity, 0, 0, "d1");

		yield return new WaitForSeconds (6f); 
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 250, 1, 100, waves.leftScreen, 0, Quaternion.Euler (0, 0, 90), 0, 0, "d5");
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 250, 1, 100, waves.rightScreen, 0, Quaternion.Euler (0, 0, -90), 0, 0, "d5");

		yield return new WaitForSeconds (4f);
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 250, 1, 100, waves.leftScreen, 200, Quaternion.Euler (0, 0, 90), 0, 0, "d5");
		waves.StartSpawnWorms (1, 4, 1, .5f, 30, 250, 1, 100, waves.rightScreen, 200, Quaternion.Euler (0, 0, -90), 0, 0, "d5");

		yield return new WaitForSeconds (5f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 400, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d5");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 0, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d5");

		yield return new WaitForSeconds (2f);
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, -400, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d5");
		waves.StartSpawnWorms (2, 4, 1, .5f, 30, 80, 5, 50, 0, waves.upScreen, Quaternion.Euler (0, 0, 0), 0, 0, "d5");

		yield return new WaitForSeconds (14f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.ROGER, "Guys? Do you read me? Short range scanners are picking up something big ahead."));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.STATIC, "... *kzzzzzzt* ..."));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);

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
