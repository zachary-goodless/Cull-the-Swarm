using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1_2 : MonoBehaviour
{

    public Waves waves;
    public ScreenFade sf;
    public LevelCompleteHandler finished;
    public GameObject drone;
    public GameObject missile;
    public GameObject turret;
	public GameObject snail;

	public DialogueBox dialog;

    void Start()
    {
		sf = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
        StartCoroutine("LevelLayout2");
    }

    IEnumerator LevelLayout2()
    {
		//JUSTIN
		Coroutine co;

		StartCoroutine(sf.FadeFromBlack());
		yield return new WaitForSeconds(2f);

		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "You don't care much for small victories, do you Stamper?"));
		yield return dialog.WaitForSecondsOrSkip(2f, co);
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.STAMPER, "No, no. You keep swatting flies from the sky. Stop the presses, it's D-Day all over again!"));
		yield return dialog.WaitForSecondsOrSkip(4f, co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "I'm sorry, do you want to get cozy in this cockpit?"));
		yield return dialog.WaitForSecondsOrSkip(2f, co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "Both of you cut it out! Roger, just go shoot more flies."));
		yield return dialog.WaitForSecondsOrSkip(3f, co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "I'd rather shoot flies than get on your bad side."));
		yield return dialog.WaitForSecondsOrSkip(2f, co);
		//JUSTIN

        // Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD

        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d1");
       yield return new WaitForSeconds(2f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 250, 1, 50, "d4");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 250, 1, 50, "d4");
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        waves.StartSpawnOsc(drone, 2, 0, 100, 200, false, false, 200, 0, -1, 50, -250, waves.upScreen, Quaternion.identity, 500, 0, "d5");
		waves.StartSpawnLinear(snail, 3, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d4");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d5");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d5");
        yield return new WaitForSeconds(9f);
        waves.StartLR6Alternate(drone);
        waves.StartSpawnLinear(turret, 3, 2, 180, 0, -1, 60, -500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        waves.StartSpawnLinear(turret, 3, 2, 180, 0, -1, 60, 500, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, 1, 0, 50, -1000, 0, Quaternion.identity, 0, 0, "d1");
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, -1, 0, 50, 1000, 0, Quaternion.identity, 0, 0, "d1");
        yield return new WaitForSeconds(6.5f);
        //waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(8f);
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, -400, waves.upScreen, Quaternion.identity, 0, 0, "t2");
        waves.StartSpawnLinear(snail, 3, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d4");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, 400, waves.upScreen, Quaternion.identity, 0, 0, "t2");
        yield return new WaitForSeconds(10f);
        waves.StartFourFromBelow(drone);
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, 1, 0, 50, -1000, 0, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, -1, 0, 50, 1000, 0, Quaternion.identity, 0, 0, "d5");
        waves.FiveWormsTop();
        yield return new WaitForSeconds(10f);
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, -1, 0, 50, 1200, 0, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnSin(drone, 3, 3, 10, 1, 250, 1, 0, 50, -1200, 0, Quaternion.identity, 0, 0, "d5");
        yield return new WaitForSeconds(9f);
        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d6");
        yield return new WaitForSeconds(3f);
        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d1");
        yield return new WaitForSeconds(3f);
        waves.StartSpawnInnerCircle(drone, 0, 0, 180, 1, 50, "d1");
        waves.StartSpawnLCornerCircle(turret, 0, 5, 200, 1, 50, "d1");
        yield return new WaitForSeconds(10f);
        waves.StartDoubleWorms();
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d4");
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d4");
        //waves.StartBigW(drone);
        //waves.StartFlanktasticFour(drone);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(turret, 0, 0, 200, 1, 50, "d4");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d4");
        waves.StartLR6Alternate(drone);
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d1");
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnSin(drone, 8, 1, 1, 5, 200, 1, 0, 50, -1000, 400, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d1");
        yield return new WaitForSeconds(2f);
        waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d4");
        waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d4");
        yield return new WaitForSeconds(10f);
       // waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d6");
        //waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d6");
        waves.StartSpawnFollow(drone, 5, 1.5f, .05f, 0, 200, 1, 50, 600, waves.upScreen, Quaternion.identity, 0, 0, "d7");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, -300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnLinear(snail, 5, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d4");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, 300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnFollow(drone, 5, 1.5f, -.05f, 0, 200, 1, 50, -600, waves.upScreen, Quaternion.identity, 0, 0, "d7");
        yield return new WaitForSeconds(10f);
        //waves.StartBigW(drone);
        //waves.StartSpawnLCornerCircle(drone, 0, 0, 200, 1, 50, "d4");
        //waves.StartSpawnRCornerCircle(drone, 0, 0, 200, 1, 50, "d4");
        waves.StartSpawnFollow(drone, 5, 1.5f, .05f, 0, 200, 1, 50, 600, waves.upScreen, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, -300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnLinear(snail, 5, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d5");
        waves.StartSpawnLinear(turret, 5, 1.5f, 160, 0, -1, 60, 300, waves.upScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnFollow(drone, 5, 1.5f, -.05f, 0, 200, 1, 50, -600, waves.upScreen, Quaternion.identity, 0, 0, "d5");
        yield return new WaitForSeconds(20f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.STAMPER, "Great job, Roger. You're nearing the heart of the city."));
		yield return dialog.WaitForSecondsOrSkip(1f, co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "What do I have to look forward to there?"));
		yield return dialog.WaitForSecondsOrSkip(1.5f, co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.MARTHA, "Nothing good..."));
		yield return dialog.WaitForSecondsOrSkip(1f, co);

		sf.Fade();
		yield return new WaitForSeconds(2f);
		//JUSTIN

        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}
