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
                attackQueue.Add(2);
                attackQueue.Add(4);
            } else if (currentPhase == 1)
            {
                attackQueue.Add(4);
                attackQueue.Add(5);
                attackQueue.Add(6);
                attackQueue.Add(3);
            } else if(currentPhase == 2)
            {
                attackQueue.Add(7);
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
            case 2:
                StartCoroutine(Pattern2());
                break;
            case 3:
                StartCoroutine(Pattern3());
                break;
            case 4:
                StartCoroutine(Pattern4());
                break;
            case 5:
                StartCoroutine(Pattern5());
                break;
            case 6:
                StartCoroutine(Pattern6());
                break;
            case 7:
                StartCoroutine(Pattern7());
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
                BulletManager.ShootBullet(transform.position, speed, 22.5f + 45f*j + angle, -0.3f, 2f, 0, BulletType.RedDarkBlade);
                BulletManager.AddAction(new BulletAction(60, true, -1f, 0, 0.6f, speed, 0));
                BulletManager.AddAction(new BulletAction(1, BulletType.RedDarkArrow));
            }
            angle += anglechange;

            yield return new WaitForSeconds(0.1f);
        }
        b.MoveRandomly();
        yield return new WaitForSeconds(1f);
        ChooseRandomPattern();
        yield break;
    }

    // Spiral that aims at player after delay
    IEnumerator Pattern2()
    {

        float angle = BulletManager.AngleToPlayerFrom(transform.position) + 60f;
        float speed = 5f;

        for (int i = 0; i < 8; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(3f);
                ChooseRandomPattern();
                yield break;
            }

            for (int j = 0; j < 12; j++)
            {
                Vector2 stopPos = new Vector2(transform.position.x + Mathf.Cos(angle) * speed * 60, transform.position.x + Mathf.Sin(angle) * speed * 60);
                BulletManager.ShootBullet(transform.position, speed, angle + j * 30, BulletType.PinkDot);
                BulletManager.AddAction(new BulletAction(60, false, 0f, 0f));
                BulletManager.AddAction(new BulletAction(60));
                BulletManager.AddAction(new BulletAction(1, BulletType.PinkCrawler));
                BulletManager.AddAction(new BulletAction(1, true, 0f, 0f, 0.1f, 4f, 0f));
            }
            angle += 13.3f;
            speed += 0.4f;

            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(3f);
        ChooseRandomPattern();
        yield break;
    }

    // Dive and shoot from below
    IEnumerator Pattern3()
    {
        float px = BulletManager.PlayerPosition().x;
        b.MoveToSlow(new Vector2(px, 400));
        yield return new WaitForSeconds(1f);
        b.ParticleBurst();
        yield return new WaitForSeconds(1f);
        b.MoveTo(new Vector2(px, -600));
        yield return new WaitForSeconds(0.2f);
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-40, 40), transform.position.y);
                BulletManager.ShootBullet(pos, Random.Range(3f, 8f), Random.Range(60f, 120f), BulletType.BlueFire);
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2f);


        for (int j = 0; j < 3; j++)
        {
            if (phaseJustChanged)
            {
                b.MoveToSlow(new Vector2(0, 300));
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }

            px = BulletManager.PlayerPosition().x;
            b.MoveTo(new Vector2(px, -500));
            yield return new WaitForSeconds(0.5f);
            b.MoveTo(new Vector2(px, -600));
            for (int i = 0; i < 30; i++)
            {
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-40, 40), transform.position.y);
                BulletManager.ShootBullet(pos, Random.Range(3f, 8f), Random.Range(40f, 140f), BulletType.BlueFire);
            }

            yield return new WaitForSeconds(1f);
        }
        b.MoveToSlow(new Vector2(0, 300));
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Waterfall pattern
    IEnumerator Pattern4()
    {
        for (int j = 0; j < 70; j++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }

            Vector2 pos = new Vector2(Random.Range(-800f, 800f), 500f);
            float sign;
            if(Random.value < 0.5f){
                sign = 1f;
            } else {
                sign = -1f;
            }
            float offset = sign * Random.Range(30f, 40f);

            BulletManager.ShootBullet(pos, 12f, 270f + offset, -0.1f, 4f, -sign, BulletType.BlueDarkBlade);
            BulletManager.AddAction(new BulletAction(90, true, 0f, 0f, 0.12f, 12f, sign*1.3f));

            yield return new WaitForSeconds(0.08f);
        }
        b.MoveToSlow(new Vector2(0, 300));
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // 8 way spread that has random turn speed (harder)
    IEnumerator Pattern5()
    {

        float angle = 0f;
        float anglechange = 0f;
        float speed = 14f;

        for (int i = 0; i < 64; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 8 == 0)
            {

                anglechange = Random.Range(-3f, 3f);
                angle += 22.5f;

            }
            for (int j = 0; j < 8; j++)
            {
                BulletManager.ShootBullet(transform.position, speed, 22.5f + 45f * j + angle, -0.3f, 2f, 0, BulletType.CyanDarkBlade);
                BulletManager.AddAction(new BulletAction(60, true, -1f, 0, 0.6f, speed, 0));
                BulletManager.AddAction(new BulletAction(1, BulletType.CyanDarkArrow));
            }
            angle += anglechange;

            yield return new WaitForSeconds(0.1f);
        }
        b.MoveRandomly();
        yield return new WaitForSeconds(1f);
        ChooseRandomPattern();
        yield break;
    }

    // Bubble HELL
    IEnumerator Pattern6()
    {
        float speed1 = 0f;
        float speed2 = 7f;
        for (int j = 0; j < 30; j++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }

            Vector2 pos = new Vector2(Random.Range(-800f, 800f), -550f);
            float sign;
            if (Random.value < 0.5f)
            {
                sign = 1f;
            }
            else
            {
                sign = -1f;
            }
            float offset = Random.Range(-30f, 30f);

            BulletManager.ShootBullet(pos, speed1, 90f + offset, 0.03f, 8f, -sign, BulletType.BlueDarkBubble);
            yield return new WaitForSeconds(0.03f);

            BulletManager.ShootBullet(pos, speed1, 90f - offset, 0.03f, 8f, sign, BulletType.BlueDarkOrb);
            yield return new WaitForSeconds(0.03f);

            pos = new Vector2(Random.Range(-800f, 800f), 550f);
            if (Random.value < 0.5f)
            {
                sign = 1f;
            }
            else
            {
                sign = -1f;
            }
            offset = Random.Range(-30f, 30f);

            BulletManager.ShootBullet(pos, speed2, 270f + offset, 0.03f, 8f, -sign, BulletType.BlueDarkBubble);
            yield return new WaitForSeconds(0.03f);

            BulletManager.ShootBullet(pos, speed2, 270f - offset, 0.03f, 8f, sign, BulletType.BlueDarkOrb);
            yield return new WaitForSeconds(0.03f);

            speed1 += 0.16f;
            speed2 -= 0.16f;
        }
        b.MoveToSlow(new Vector2(0, 300));
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Webs
    IEnumerator Pattern7()
    {
        float angle = BulletManager.AngleToPlayerFrom(transform.position) + Random.Range(-5f,5f);
        for (int j = 0; j < 40; j++)
        {
            BulletManager.ShootBullet(transform.position, 8f + Mathf.Cos(j * 360f / 40f * 8f * Mathf.Deg2Rad) * 2f, angle + j * 360f/40f, -0.1f, 3f, 0, BulletType.WhiteShard);
        }
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 12; i++)
            {
                BulletManager.ShootBullet(transform.position, 4 + i * 1.5f, angle + j * 45, -0.1f, 3f, 0, BulletType.WhiteShard);
            }
        }

        b.MoveRandomly();
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }
}
