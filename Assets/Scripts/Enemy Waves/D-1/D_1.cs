using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class D_1 : MonoBehaviour
{

    public Waves waves;
    public ScreenFade sf;
    public LevelCompleteHandler finished;
    public GameObject drone;
    public GameObject missile;
    public GameObject turret;

	public DialogueBox dialog;

    void Start()
    {
		sf = GameObject.Find("ScreenFade").GetComponent<ScreenFade>();
        StartCoroutine ("LevelLayout2");
    }

    IEnumerator LevelLayout2()
    {
		//JUSTIN
		Coroutine co;

		StartCoroutine(sf.FadeFromBlack());
		yield return new WaitForSeconds(2f);

		co = StartCoroutine(dialog.handleDialogue(3f, Characters.COLONEL, "How are you adjusting to the altitude, Roger?"));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(6f, Characters.ROGER, "It's none too different from the standard fighter jets I've flown before, sir. Bit smoother, actually."));
		yield return dialog.WaitForSecondsOrSkip(5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(1.5f, Characters.COLONEL, "Glad to hear it."));
		yield return dialog.WaitForSecondsOrSkip(0.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.COLONEL, "We still haven't pinpointed the location of this alien invasion, but we'll need every pilot we can manage to fight off these hoards."));
		yield return dialog.WaitForSecondsOrSkip(4f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.COLONEL, "There's a small swarm nearing our base as we speak. Should make for an excellent training exercise."));
		yield return dialog.WaitForSecondsOrSkip(4f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Well, with a ship like this it shouldn't be a problem."));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.STAMPER, "Much obliged."));
		yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "I'm sorry. Have we met?"));
		yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(6f, Characters.COLONEL, "This is Calvin Stamper. He's personally responsible for that ship you're piloting."));
		yield return dialog.WaitForSecondsOrSkip(5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(6f, Characters.COLONEL, "I've asked him to contribute his wisdom to your radio support team on missions."));
		yield return dialog.WaitForSecondsOrSkip(5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Support team? Who else is on call?"));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.COLONEL, "Ah, yes. I believe you already know Martha."));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "Oh, God. Please not her..."));
		yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(6f, Characters.MARTHA, "I want to be here as much as you do. You know how much I hated my last job."));
		yield return dialog.WaitForSecondsOrSkip(5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.ROGER, "Really? Telephone operating was that stressful of a job?"));
		yield return dialog.WaitForSecondsOrSkip(4f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(6f, Characters.COLONEL, "That'll be enough! Say what you will, Roger, but she's damn fine at handling intel."));
		yield return dialog.WaitForSecondsOrSkip(5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.COLONEL, "Now look alive! They're headed your way!"));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
		//JUSTIN

      // Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
        waves.StartSpawnLinear(drone, 5, 2, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0,"null");
        waves.StartSpawnLinear(drone, 2, 2, 200, -1, -1, 50, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0,"null");
        Debug.Log(waves.leftScreen + " " + waves.upScreen);
        yield return new WaitForSeconds(10f);
		waves.StartSpawnLinear(drone, 5, 2, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0,"d7");
		waves.StartSpawnLinear(drone, 2, 2, 200, -1, -1, 50, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0,"d7");
        //waves.StartSpawnWorms(3, 5, 3, .5f, 30, 200, 1, 50, waves.leftScreen, 100, Quaternion.Euler(0, 0, 90), 0, -200,"d4");
        yield return new WaitForSeconds(10f);
        waves.StartSpawnFollow(turret, 3, 4, .5f, 30, 150, -1, 200, 5, waves.downScreen, Quaternion.identity, 0, 0,"t3");
        yield return new WaitForSeconds(5f);
        waves.StartLR6Alternate(drone);
        yield return new WaitForSeconds(6.5f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(8f);
        waves.StartSpawnFromBackground(drone, 3, 10f, 2f, Vector3.down, 10f, 1, 1, 200, waves.leftScreen, waves.downScreen, Quaternion.identity, 200, 0,"d23");
        waves.TwoWormsBelow();
        yield return new WaitForSeconds(4f);
        waves.StartSpawnSideToBottom(turret, 5, 2f, 2f, 4f, 100, -100, 200, waves.leftScreen, waves.upScreen, Quaternion.identity, 200, 0,"t1");
        waves.StartSpawnSideToBottom(turret, 5, 3f, 4f, 5f, -100, -100, 200, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0,"t1");
        yield return new WaitForSeconds(6f);
        waves.StartSpawnSideToBottom(turret, 5, 2f, 3f, 5f, -100, -100, 200, waves.rightScreen, waves.upScreen, Quaternion.identity, 200, 0,"t2");
        yield return new WaitForSeconds(10f);
        waves.StartBigW(drone);
        yield return new WaitForSeconds(9f);
        waves.StartFlanktasticFour(turret);
        yield return new WaitForSeconds(10f);
        waves.FiveWormsTop();
        yield return new WaitForSeconds(20f);
        waves.StartLR6Alternate(turret);
        waves.StartDoubleWorms();
		yield return new WaitForSeconds(15f);

		//JUSTIN
		dialog.isSkipping = false;
		co = StartCoroutine(dialog.handleDialogue(5f, Characters.COLONEL, "Excellent work, Roger. You were made for this machine."));
		yield return dialog.WaitForSecondsOrSkip(4f); if(co != null) StopCoroutine(co);

		sf.Fade();
		yield return new WaitForSeconds(2f);
		//JUSTIN

        finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

}
