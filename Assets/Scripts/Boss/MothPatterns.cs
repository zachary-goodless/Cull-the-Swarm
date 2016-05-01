using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MothPatterns : MonoBehaviour {

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
		co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Whatever's causing this radio interference must be just up ahead."));
		yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
		//JUSTIN

        for(int i = 0; i < 120; i++)
        {
            transform.Translate(new Vector3(0, -700f / 120f, 0));
            yield return null;
        }

		//JUSTIN
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.STATIC, "... *kzzzzzzt* ..."));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
		co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Looks like it's just you and me, moth."));
		yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
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
                attackQueue.Add(5);
            } else if (currentPhase == 1) {
                attackQueue.Add(2);
                attackQueue.Add(3);
                attackQueue.Add(6);
            } else if(currentPhase == 2) {
                attackQueue.Add(4);
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
        }

    }

    // Shoots waving pattern of green dots from two sides
    IEnumerator Pattern1()
    {

        float angle = 90f;
        float anglechange = 13f;
        float speed = 10f;
        int delay = 60;

        for(int i = 0; i < 50; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }
            for (int j = 0; j < 3; j++)
            {
                BulletManager.ShootBullet(new Vector2(transform.position.x + 240f, transform.position.y), speed, j * 120 + angle, -0.1f, 1, 0, BulletType.GreenDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 120 + angle, 0.04f, 10f, 0));

                BulletManager.ShootBullet(new Vector2(transform.position.x - 240f, transform.position.y), speed, j * 120 - angle + 180, -0.1f, 1, 0, BulletType.GreenDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 120 - angle + 180, 0.04f, 10f, 0));
            }

            angle += anglechange;
            anglechange -= 0.6f;
            speed -= 0.15f;
            delay++;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(4.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Shoots bubble starburst
    IEnumerator Pattern2()
    {
        float speed = 7f;
        BulletType[] shotTypes = { BulletType.GreenBubble, BulletType.CyanDarkBubble };
        for (int i = 0; i < 4; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            int numShots = 8 + i * 2;
            float angle = Random.Range(0, 360);

            for (int j = 0; j < numShots; j++)
            {
                BulletManager.ShootBullet(transform.position, speed+j%2, angle + j * (360/numShots), -0.02f, 3, 0, shotTypes[j%2]);
                BulletManager.AddAction(new BulletAction(60, true, 0, 0, 0.04f, 12f, 0));
            }
            b.MoveRandomly();
            speed--;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Surrounding pattern
    IEnumerator Pattern3()
    {

        Player p = GameObject.FindObjectOfType<Player>();

        for(int i = 0; i < 6; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(3f);
                ChooseRandomPattern();
                yield break;
            }

            float x = p.transform.position.x;
            float y = p.transform.position.y;
            float r = 400 + i * 80;
            int numShots = 5 + i * 2;
            float angle = Random.Range(0f, 360f);

            for(int j = 0; j < numShots; j++){
                BulletManager.ShootBullet(new Vector2(x + Mathf.Cos(angle * Mathf.Deg2Rad) * r, y + Mathf.Sin(angle * Mathf.Deg2Rad) * r), 0, angle + 180, 0.02f, 3f, 0, BulletType.CyanArrow);
                angle += 360f / numShots;
            }
            yield return new WaitForSeconds(0.8f);
        }

        yield return new WaitForSeconds(4f);
        ChooseRandomPattern();
        yield break;
    }

    // Curving spiral
    IEnumerator Pattern4()
    {
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 5; j++){
                BulletManager.ShootBullet(transform.position, 2, angle+ j * 72, 0.06f, 20f, -0.7f, BulletType.GreenShard);
                BulletManager.ShootBullet(transform.position, 4, angle + j * 72 + 12, 0.06f, 20f, -0.7f, BulletType.CyanShard);
            }
            angle += Random.Range(0f, 14f);
            yield return new WaitForSeconds(0.15f);
        }

        b.MoveRandomly();
        ChooseRandomPattern();
        yield break;
    }

    // Curving rings
    IEnumerator Pattern5()
    {
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 3; i++)
        {

            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(2f);
                ChooseRandomPattern();
                yield break;
            }
            int numShot = 12 + i * 4;
            for (int j = 0; j < numShot; j++)
            {
                BulletManager.ShootBullet(transform.position, Random.Range(2f,3f), angle + j * 360/numShot, 0.02f, 20f, -0.7f, BulletType.GreenShard);
                BulletManager.AddAction(new BulletAction(90, true, 0, 0, 0, 0, 0.7f));
                BulletManager.ShootBullet(transform.position, Random.Range(2f, 3f), angle + (j+0.5f) * 360 / numShot, 0.02f, 20f, 0.7f, BulletType.CyanShard);
                BulletManager.AddAction(new BulletAction(90, true, 0, 0, 0, 0, -0.7f));
            }
            angle += Random.Range(0f, 14f);
            b.MoveRandomly();
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2f);

        ChooseRandomPattern();
        yield break;
    }

    // Shoots waving pattern of pink dots from two sides
    IEnumerator Pattern6()
    {

        float angle = 90f;
        float anglechange = 13f;
        float speed = 11f;
        int delay = 60;

        for (int i = 0; i < 25; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(3f);
                ChooseRandomPattern();
                yield break;
            }
            for (int j = 0; j < 6; j++)
            {
                BulletManager.ShootBullet(new Vector2(transform.position.x + 240f, transform.position.y), speed, j * 60 + angle, -0.12f, 1, 0, BulletType.PinkDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 60 + angle, 0.05f, 3f, 0));

                BulletManager.ShootBullet(new Vector2(transform.position.x - 240f, transform.position.y), speed, j * 60 - angle + 180, -0.12f, 1, 0, BulletType.PinkDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 60 - angle + 180, 0.05f, 3f, 0));
            }

            angle -= anglechange;
            anglechange -= 1.6f;
            speed -= 0.2f;
            delay++;

            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(4f);
        ChooseRandomPattern();
        yield break;
    }
}
