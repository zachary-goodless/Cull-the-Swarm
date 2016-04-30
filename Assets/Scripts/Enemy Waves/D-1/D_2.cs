using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class D_2 : MonoBehaviour
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

		co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "You've made short work of them before, Roger. You'll be back at base in no time."));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.ROGER, "I appreciate the praise, sir."));
		yield return dialog.WaitForSecondsOrSkip(2.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "I'm picking up heavier movement ahead of you. Stay on your toes!"));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.STAMPER, "Interesting how the difficulty curve seems to apply to everything."));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		//JUSTIN

        // Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
        waves.StartSpawnOsc(drone, 3, 0, 300, 200, false, true, 200, 0, -1, 50, -500, waves.upScreen + 150, Quaternion.identity, 500, 0, "d4");

        Debug.Log(waves.leftScreen + " " + waves.upScreen);
        yield return new WaitForSeconds(6f);
        waves.StartBigW(drone);
        waves.StartSpawnOsc(drone, 2, 0, 100, 200, false, false, 200, 0, -1, 50, -250, waves.upScreen, Quaternion.identity, 500, 0, "d4");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(drone, 5, 1, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0, "d1");
		yield return new WaitForSeconds (4f);
		waves.StartSpawnLinear(drone, 5, 1, 300, -1, -1, 50, waves.rightScreen, waves.upScreen, Quaternion.identity, -200, 0, "d1");
        yield return new WaitForSeconds(9f);
        waves.StartLR6Alternate(drone);
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, 1, 0, 50, -1000, 0, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnSin(drone, 2, 3, 10, 1, 250, -1, 0, 50, 1000, 0, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(6.5f);
		waves.StartSpawnLinear (drone, 5, 2, 300, 0, 1, 50, -400, waves.downScreen, Quaternion.identity, 0, 0, "d1");
		waves.StartSpawnLinear (drone, 5, 2, 300, 0, 1, 50, 400, waves.downScreen, Quaternion.identity, 0, 0, "d1");
        yield return new WaitForSeconds(8f);
        waves.StartSpawnLinear(turret, 2, 1.5f, 160, 0, -1, 60, -300, waves.upScreen, Quaternion.identity, 0, 0, "t1");
		waves.StartSpawnLinear(snail, 3, 1.5f, 200, 0, -1, 60, 0, waves.upScreen, Quaternion.identity, 0, 0, "d4");
        waves.StartSpawnLinear(turret, 2, 1.5f, 160, 0, -1, 60, 300, waves.upScreen, Quaternion.identity, 0, 0, "t1");
        yield return new WaitForSeconds(10f);
        waves.StartFourFromBelow(drone);
        yield return new WaitForSeconds(9f);
        waves.StartSpawnDive(drone, 5, 3, 300, 10, 200, 1, -1, 50, -500, waves.upScreen, Quaternion.identity, 0, 200, "d7");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(turret, 2, 4, 100, 0, 1, 50, 500, waves.downScreen, Quaternion.identity, 0, 0, "t3");
        waves.StartSpawnLinear(turret, 2, 4, 100, 0, 1, 50, -500, waves.downScreen, Quaternion.identity, 0, 0, "t3");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnLinear(drone, 4, 4, 350, -1, -1, 50, 500, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        waves.StartSpawnLinear(drone, 4, 4, 350, 1, -1, 50, -500, waves.upScreen, Quaternion.identity, 0, 0, "d23");
        yield return new WaitForSeconds(30f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(4.5f, Characters.ROGER, "That should be the last of them. Returning to base."));
		yield return dialog.WaitForSecondsOrSkip(3.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "Intel has picked up some bad news, Roger."));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(6f, Characters.MARTHA, "The leader of that pack's made himself known, and he's none too happy. If you don't act fast, we're all in serious trouble!"));
		yield return dialog.WaitForSecondsOrSkip(5f); if(co != null) StopCoroutine(co);

		sf.Fade();
		yield return new WaitForSeconds(2f);
		//JUSTIN

        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}
