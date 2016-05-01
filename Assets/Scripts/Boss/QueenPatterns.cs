using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueenPatterns : MonoBehaviour {

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

	public DialogueBox dialog;

	// Use this for initialization
	void Start ()
    {
        b = GetComponent<Boss>();
        b.tiltEnabled = false;
        b.isQueen = true;
        attackQueue = new List<int>();
        StartCoroutine(FlyIn());
    }
	
	// Update is called once per frame
	void Update () {
        if(b.phase != currentPhase)
        {
            phaseJustChanged = true;
            currentPhase = b.phase;
        }
	}

    IEnumerator FlyIn()
    {
		//JUSTIN
		Boss.isOnBossStart = true;
		Coroutine co;
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.COLONEL, "No no no!"));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
		for(int i = 0; i < 70; ++i)
		{
			if(i % 10 == 0) CameraShake.shakeCamera();	//JUSTIN
			yield return null;
		}
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Another fly swatted. I'm closing in on the facility now."));
		yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "I don't know that the Colonel should be your biggest concern right now..."));
		yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "Sonar is picking up activity from something BIG headed your way."));
		yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
		//JUSTIN

        for(int i = 0; i < 480; i++)
        {
            transform.Translate(new Vector3(0, 1000f / 480f, 0));
			if(i % 10 == 0) CameraShake.shakeCamera();	//JUSTIN
            yield return null;
        }

		//JUSITN
		co = StartCoroutine(dialog.handleDialogue(2f, Characters.COLONEL, "The termite queen! She's escaped!"));
		yield return dialog.WaitForSecondsOrSkip(1f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "What are you talking about? What's going on?"));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(1.5f, Characters.COLONEL, "*sigh*"));
		yield return dialog.WaitForSecondsOrSkip(0.5f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.COLONEL, "The government paid our organization to develop bio-weapons using experiments on insects."));
		yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "They escaped captivity, and we painted our mistake as an alien invasion while we cleaned up this mess."));
		yield return dialog.WaitForSecondsOrSkip(3f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "But if we have a common foe in these bugs, why oppose me here?"));
		yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.COLONEL, "You intended to exterminate them all. We had hoped we could salvage the program and continue our research."));
		yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.COLONEL, "The world cannot discover what we've been doing. But with the queen's escape, the whole world is in danger."));
		yield return dialog.WaitForSecondsOrSkip(2.5f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.COLONEL, "The other insects are controlled by a pheromone that she secretes. Kill her, and the bugs will die."));
		yield return dialog.WaitForSecondsOrSkip(2.5f); if (co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.COLONEL, "Do it, Roger! End the queen's reign!"));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
		Boss.isOnBossStart = false;
		//JUSTIN

        ChooseRandomPattern();
    }

    void ChooseRandomPattern()
    {
        if(b.health < 0) { return; }
        int chosenAttack;

        if (attackQueue.Count == 0 || phaseJustChanged) {
            attackQueue.Clear();
            Debug.Log("Refreshing attack queue for phase " + currentPhase);
            if (currentPhase == 0){
                attackQueue.Add(1);
            } else if (currentPhase == 1){
                attackQueue.Add(1);
            }
            else if (currentPhase == 2){
                attackQueue.Add(1);
            }
            else if (currentPhase == 3){
                attackQueue.Add(1);
            }
        }

        Debug.Log("Queue contents:");
        foreach(int num in attackQueue)
        {
            Debug.Log(num);
        }

        if (phaseJustChanged)   // Always do the new attack first when phase changes
        {
            chosenAttack = attackQueue[attackQueue.Count-1];
            phaseJustChanged = false;
        } else {
            chosenAttack = attackQueue[Random.Range(0, attackQueue.Count)];
        }

        Debug.Log("Chose " + chosenAttack);

        attackQueue.Remove(chosenAttack);

        switch (chosenAttack)
        {
            case 1:
                StartCoroutine(Pattern1());
                break;
        }

    }

    // Shoots waving pattern of green dots from two sides
    IEnumerator Pattern1()
    {

        for(int i = 0; i < 3; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            b.MoveRandomly();

            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(1f);
        ChooseRandomPattern();
        yield break;
    }
}
