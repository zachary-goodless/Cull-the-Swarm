using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QueenPatterns : MonoBehaviour
{

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

    public DialogueBox dialog;

    // Use this for initialization
    void Start()
    {
        b = GetComponent<Boss>();
        b.tiltEnabled = false;
        b.isQueen = true;
        attackQueue = new List<int>();
        StartCoroutine(FlyIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (b.phase != currentPhase)
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
        for (int i = 0; i < 70; ++i)
        {
            if (i % 10 == 0) CameraShake.shakeCamera(); //JUSTIN
            yield return null;
        }
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Another fly swatted. I'm closing in on the facility now."));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "I don't know that the Colonel should be your biggest concern right now..."));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "Sonar is picking up activity from something BIG headed your way."));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        //JUSTIN

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 320; i++)
        {
            transform.Translate(new Vector3(0, 1000f / 320f, 0));
            if (i % 10 == 0) CameraShake.shakeCamera();	//JUSTIN
            yield return null;
        }

        //JUSITN
        co = StartCoroutine(dialog.handleDialogue(2f, Characters.COLONEL, "The termite queen! She's escaped!"));
        yield return dialog.WaitForSecondsOrSkip(1f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "What are you talking about? What's going on?"));
        yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(2f, Characters.COLONEL, "*sigh*"));
        yield return dialog.WaitForSecondsOrSkip(1f); if (co != null) StopCoroutine(co);
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
        ShipPatterns sp = GameObject.FindObjectOfType<ShipPatterns>();
        if (sp != null)
        {
            sp.songDelay = 7.5f;
        }
        //JUSTIN

        yield return new WaitForSeconds(1f);
        StartCoroutine(Pattern1());
    }

    void ChooseRandomPattern()
    {
        if (b.health < 0) { return; }
        int chosenAttack;

        if (attackQueue.Count == 0 || phaseJustChanged)
        {
            attackQueue.Clear();
            Debug.Log("Refreshing attack queue for phase " + currentPhase);
            if (currentPhase == 0)
            {
                attackQueue.Add(2);
                attackQueue.Add(3);
                attackQueue.Add(8);
            }
            else if (currentPhase == 1)
            {
                attackQueue.Add(2);
                attackQueue.Add(3);
                attackQueue.Add(8);

                attackQueue.Add(1);
                attackQueue.Add(5);
                attackQueue.Add(6);
            }
            else if (currentPhase == 2)
            {
                attackQueue.Add(5);
                attackQueue.Add(6);

                attackQueue.Add(4);
                attackQueue.Add(7);
            }
            else if (currentPhase == 3)
            {
                attackQueue.Add(9);
            }
        }

        Debug.Log("Queue contents:");
        foreach (int num in attackQueue)
        {
            Debug.Log(num);
        }

        if (phaseJustChanged)   // Always do the new attack first when phase changes
        {
            chosenAttack = attackQueue[attackQueue.Count - 1];
            phaseJustChanged = false;
        }
        else
        {
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
            case 8:
                StartCoroutine(Pattern8());
                break;
            case 9:
                StartCoroutine(Pattern9());
                break;
        }

    }

    // Rainbow spiral
    IEnumerator Pattern1()
    {
        BulletType[] c = { BulletType.RedCrawler, BulletType.YellowCrawler, BulletType.GreenCrawler, BulletType.CyanCrawler, BulletType.BlueCrawler, BulletType.PurpleCrawler, BulletType.PinkCrawler };
        int cIndex = 0;
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 90; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 4 == 0 && i != 0)
            {
                cIndex++;
            }

            for (int j = 0; j < 6; j++)
            {
                float fAngle = angle + j * 60f;
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(fAngle * Mathf.Deg2Rad) * 240f, transform.position.y + Mathf.Sin(fAngle * Mathf.Deg2Rad) * 60f + 120f);
                BulletManager.ShootBullet(pos, 12f, fAngle + 30f, -0.2f, 1f, 0f, c[cIndex % c.Length]);
                BulletManager.AddAction(new BulletAction(60, true, 0, 0, 0.4f, 8f, 0f));
            }
            angle -= 17;
            yield return new WaitForSeconds(0.08f);
        }
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Spider's pattern
    IEnumerator Pattern2()
    {

        float angle = 0f;
        float anglechange = 0f;
        float speed = 14f;

        for (int i = 0; i < 48; i++)
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
            for (int j = 0; j < 6; j++)
            {
                BulletManager.ShootBullet(transform.position, speed, 60f * j + angle, -0.3f, 2f, 0, BulletType.CyanDarkBlade);
                BulletManager.AddAction(new BulletAction(60, true, -1f, 0, 0.6f, speed, 0));
                BulletManager.AddAction(new BulletAction(1, BulletType.CyanDarkArrow));
                BulletManager.ShootBullet(transform.position, speed, 30f + 60f * j + angle, -0.3f, 2f, 0, BulletType.RedDarkBlade);
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

    //Moth's pattern
    IEnumerator Pattern3()
    {
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 7; i++)
        {

            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 3 == 0)
            {
                b.MoveRandomly();
            }

            int numShot = 8 + i * 2;
            Vector2 pos = new Vector2(transform.position.x + Random.Range(-60f, 60f), transform.position.y + Random.Range(0f, 100f));
            for (int j = 0; j < numShot; j++)
            {
                BulletManager.ShootBullet(pos, Random.Range(3f, 4f), angle + j * 360 / numShot, 0.02f, 20f, -0.7f, BulletType.RedShard);
                BulletManager.AddAction(new BulletAction(90, true, 0, 0, 0, 0, 0.7f));
                BulletManager.ShootBullet(pos, Random.Range(2.5f, 3.5f), angle + (j + 0.3f) * 360 / numShot, 0.02f, 20f, 0.7f, BulletType.PinkShard);
                BulletManager.AddAction(new BulletAction(90, true, 0, 0, 0, 0, -0.7f));
                BulletManager.ShootBullet(pos, Random.Range(2f, 3f), angle + (j + 0.6f) * 360 / numShot, 0.02f, 20f, 0.7f, BulletType.PurpleShard);
                BulletManager.AddAction(new BulletAction(90, true, 0, 0, 0, 0, -0.7f));
            }
            angle += Random.Range(0f, 14f);
            yield return new WaitForSeconds(0.8f);
        }
        yield return new WaitForSeconds(2f);

        ChooseRandomPattern();
        yield break;
    }

    // Scorpion pattern
    IEnumerator Pattern4()
    {
        float minSpeed = 1.2f;
        float maxSpeed = 5.5f;

        float leftSpeed = minSpeed;
        float rightSpeed = minSpeed;
        Vector2 startPos = transform.position;

        for (int i = 0; i < 80; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }
            if (i % 5 == 0)
            {
                leftSpeed = Random.Range(minSpeed, maxSpeed);
                rightSpeed = Random.Range(minSpeed, maxSpeed);
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            BulletManager.ShootBullet(new Vector2(startPos.x - i * 15, startPos.y), 0, 90, BulletType.YellowShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 90, -0.2f, leftSpeed, 0));

            BulletManager.ShootBullet(new Vector2(startPos.x - i * 15, startPos.y), 0, 270, BulletType.YellowShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 270, -0.2f, leftSpeed, 0));

            BulletManager.ShootBullet(new Vector2(startPos.x + i * 15, startPos.y), 0, 90, BulletType.YellowShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 90, -0.2f, rightSpeed, 0));

            BulletManager.ShootBullet(new Vector2(startPos.x + i * 15, startPos.y), 0, 270, BulletType.YellowShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 270, -0.2f, rightSpeed, 0));
        }

        for (int i = 0; i < 80; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }
            if (i % 3 == 0)
            {
                leftSpeed = Random.Range(minSpeed, maxSpeed);
                rightSpeed = Random.Range(minSpeed, maxSpeed);
                yield return new WaitForSeconds(0.03f);
                continue;
            }
            BulletManager.ShootBullet(new Vector2(startPos.x - i * 15, startPos.y), 0, 90, BulletType.RedShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 90, -0.2f, leftSpeed, 0));

            BulletManager.ShootBullet(new Vector2(startPos.x - i * 15, startPos.y), 0, 270, BulletType.RedShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 270, -0.2f, leftSpeed, 0));

            BulletManager.ShootBullet(new Vector2(startPos.x + i * 15, startPos.y), 0, 90, BulletType.RedShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 90, -0.2f, rightSpeed, 0));

            BulletManager.ShootBullet(new Vector2(startPos.x + i * 15, startPos.y), 0, 270, BulletType.RedShard);
            BulletManager.AddAction(new BulletAction(60, false, 10, 270, -0.2f, rightSpeed, 0));
        }
        yield return new WaitForSeconds(3f);
        ChooseRandomPattern();
        yield break;
    }

    // Beetle pattern
    IEnumerator Pattern5()
    {
        b.MoveRandomly();
        yield return new WaitForSeconds(0.5f);
        b.ParticleBurst();
        yield return new WaitForSeconds(1f);

        float r = 30;
        for (int i = 0; i < 80; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }
            if (i > 20)
            {
                r += 8;
            }
            for (int j = 0; j < 3; j++)
            {
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(i + j * 120 * Mathf.Deg2Rad) * r, transform.position.y + Mathf.Sin(i + j * 120 * Mathf.Deg2Rad) * r / 3f);
                BulletManager.ShootBullet(pos, 0, 270, 0.5f, 30, 0, BulletType.YellowBlade);
            }
            BulletManager.ShootBullet(transform.position, 6, Random.Range(0f, 360f), BulletType.PinkBlade);
            yield return new WaitForSeconds(0.05f);
        }
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 45; i++)
        {
            BulletManager.ShootBullet(transform.position, 6, angle + i * 8, BulletType.YellowArrow);
        }
        yield return new WaitForSeconds(1.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Moth pattern 2
    IEnumerator Pattern6()
    {

        float angle = 90f;
        float anglechange = 13f;
        float speed = 10f;
        int delay = 60;

        for (int i = 0; i < 50; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }
            for (int j = 0; j < 5; j++)
            {
                BulletManager.ShootBullet(new Vector2(transform.position.x + 240f, transform.position.y), speed, j * 72 + angle, -0.1f, 1, 0, BulletType.WhiteDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 72 + angle, 0.04f, 10f, 0));

                BulletManager.ShootBullet(new Vector2(transform.position.x - 240f, transform.position.y), speed, j * 72 - angle + 180, -0.1f, 1, 0, BulletType.WhiteDarkDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 72 - angle + 180, 0.04f, 10f, 0));
            }

            angle += anglechange;
            anglechange -= 0.6f;
            speed -= 0.15f;
            delay++;

            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(4.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Spider pattern 2
    IEnumerator Pattern7()
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
            if (Random.value < 0.5f)
            {
                sign = 1f;
            }
            else
            {
                sign = -1f;
            }
            float offset = sign * Random.Range(30f, 40f);

            BulletManager.ShootBullet(pos, 12f, 270f + offset, -0.15f, 2f, -sign, BulletType.PinkDarkBlade);
            BulletManager.AddAction(new BulletAction(110, true, 0f, 0f, 0.3f, 12f, sign * 1.5f));

            yield return new WaitForSeconds(0.08f);
        }
        b.MoveToSlow(new Vector2(0, 300));
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Scorpion pattern 2
    IEnumerator Pattern8()
    {
        float angle = Random.Range(0, 360);
        for (int i = 0; i < 3; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }
            for (int j = 0; j < 36; j++)
            {
                BulletManager.ShootBullet(transform.position, 6f + Mathf.Sin(j * 50 * Mathf.Deg2Rad) * 3.5f, angle + j * 10, -0.02f, 2, 0, BulletType.BlueBlade);
                BulletManager.ShootBullet(transform.position, 5f - Mathf.Sin(j * 50 * Mathf.Deg2Rad) * 3.5f, angle + j * 10, -0.02f, 2, 0, BulletType.PinkBlade);
            }
            b.MoveRandomly();
            angle += 170;
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(1.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Rainbow finale
    IEnumerator Pattern9()
    {
        BulletType[] c1 = { BulletType.RedCrawler, BulletType.YellowCrawler, BulletType.GreenCrawler, BulletType.CyanCrawler, BulletType.BlueCrawler, BulletType.PurpleCrawler, BulletType.PinkCrawler };
        BulletType[] c2 = { BulletType.RedDarkBubble, BulletType.YellowDarkBubble, BulletType.GreenDarkBubble, BulletType.CyanDarkBubble, BulletType.BlueDarkBubble, BulletType.PurpleDarkBubble, BulletType.PinkDarkBubble };

        int cIndex = 0;
        int cIndex2 = 0;
        float angle = Random.Range(0f, 360f);
        int i = 0;
        int numShot = 6;
        float t = 0.12f;
        while(b.health > 0)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }

            if(i % 30 == 0 && i != 0 && t > 0.05f)
            {
                t -= 0.01f;
            }

            if (i % 4 == 0 && i != 0)
            {
                cIndex++;
            }
            if(i%20 == 0)
            {
                Vector2 pos = new Vector2(transform.position.x, transform.position.y + 100f);
                BulletManager.ShootBullet(pos, 8f, BulletManager.AngleToPlayerFrom(pos), -0.2f, 1f, 0f, c2[cIndex2 % c2.Length]);
                BulletManager.AddAction(new BulletAction(80, true, 0, 0, 0.5f, 12f, 0f));
                cIndex2++;
            }
            i++;

            for (int j = 0; j < numShot; j++)
            {
                float fAngle = angle + j * 360 / numShot;
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(fAngle * Mathf.Deg2Rad) * 240f, transform.position.y + Mathf.Sin(fAngle * Mathf.Deg2Rad) * 60f + 120f);
                BulletManager.ShootBullet(pos, 12f, fAngle + 30f, -0.2f, 1f, 0f, c1[cIndex % c1.Length]);
                BulletManager.AddAction(new BulletAction(60, true, 0, 0, 0.4f, 8f, 0f));
            }
            

            angle -= 17;
            yield return new WaitForSeconds(t);
        }
        yield break;
    }
}
