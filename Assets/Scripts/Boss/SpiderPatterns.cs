using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpiderPatterns : MonoBehaviour {

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

	public DialogueBox dialog;	//JUSTIN

	// Use this for initialization
	void Start () {
        b = GetComponent<Boss>();
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
		yield return new WaitForSeconds(1.5f);
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "..."));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
		//JUSTIN

        for(int i = 0; i < 120; i++)
        {
            transform.Translate(new Vector3(0, -700f / 120f, 0));
            yield return null;
        }

		//JUSTIN
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "Everything alright, Roger?"));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Yeah, actually..."));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.MARTHA, "It's just that after your reaction to the scorpion, I took you for a bit of an aracnophobe."));
		yield return dialog.WaitForSecondsOrSkip(2.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(4f, Characters.ROGER, "Oh, no. I'm scared. Terrified even. I've just sort of become numb to the fear of giant insects."));
		yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
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
            if (currentPhase == 0)
            {
                attackQueue.Add(1);
            } else if (currentPhase == 1)
            {
                attackQueue.Add(1);
            } else if(currentPhase == 2)
            {
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

    // 8 way spread that has random turn speed
    IEnumerator Pattern1()
    {

        float angle = 0f;
        float anglechange = 0f;
        float speed = 12f;

        for(int i = 0; i < 30; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 10 == 0)
            {

                anglechange = Random.Range(-2f, 2f);
                angle += 22.5f;

            }
            for (int j = 0; j < 8; j++)
            {
                BulletManager.ShootBullet(new Vector2(transform.position.x, transform.position.y), speed, 22.5f + 45f*j + angle, -0.3f, 2f, 0, BulletType.RedDarkBlade);
                BulletManager.AddAction(new BulletAction(60, true, -1f, 0, 0.6f, speed, 0));
                BulletManager.AddAction(new BulletAction(1, BulletType.RedDarkArrow));
            }
            angle += anglechange;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        ChooseRandomPattern();
        yield break;
    }
}
