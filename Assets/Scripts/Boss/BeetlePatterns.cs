using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeetlePatterns : MonoBehaviour {

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

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
        for(int i = 0; i < 120; i++)
        {
            transform.Translate(new Vector3(0, -700f / 120f, 0));
            yield return null;
        }
        ChooseRandomPattern();
    }

    void ChooseRandomPattern()
    {
        if(b.health < 0) { return; }
        int chosenAttack;

        if (attackQueue.Count == 0 || phaseJustChanged) {
            attackQueue.Clear();
            Debug.Log("Refreshing attack queue for phase " + currentPhase);
            if (currentPhase == 0) {
                attackQueue.Add(1);
                attackQueue.Add(2);
            } else if (currentPhase == 1) {
                attackQueue.Add(1);
                attackQueue.Add(2);
                attackQueue.Add(3);
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
        }

    }

    // Random spread of red and green orbs
    IEnumerator Pattern1()
    {
        float minSpeed = 2;
        float maxSpeed = 8;

        for(int i = 0; i < 80; i++)
        {
            float angle = Random.Range(0f, 360f);
            BulletManager.ShootBullet(transform.position, Random.Range(minSpeed, maxSpeed), angle, BulletType.GreenOrb);
            BulletManager.ShootBullet(transform.position, Random.Range(minSpeed, maxSpeed), angle+180, BulletType.RedDot);
        }
        yield return new WaitForSeconds(4f);
        ChooseRandomPattern();
        yield break;
    }

    // Yellow beam straight down
    IEnumerator Pattern2()
    {
        for (int i = 0; i < 60; i++)
        {
            if (phaseJustChanged)
            {
                ChooseRandomPattern();
                yield break;
            }
            if(i == 30)
            {
                b.MoveRandomly();
            }
            for (int j = 0; j < 3; j++)
            {
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(i + j*120 * Mathf.Deg2Rad) * 150f, transform.position.y + Mathf.Sin(i + j * 120 * Mathf.Deg2Rad) * 60f);
                BulletManager.ShootBullet(pos, 0, 270, 0.5f, 30, 0, BulletType.YellowBlade);
            }
            yield return new WaitForSeconds(0.05f);
        }
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 30; i++)
        {
            BulletManager.ShootBullet(transform.position, 6, angle + i * 12, BulletType.YellowArrow);
        }
        yield return new WaitForSeconds(1.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Green arrow clumps
    IEnumerator Pattern3()
    {
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 20; i++)
        {
            BulletManager.ShootBullet(transform.position, 8 + i / 10f, angle, BulletType.GreenDarkBubble);
            for (int j = 0; j < 10; j++)
            {
                BulletManager.ShootBullet(transform.position, 8 - j / 2f + i / 10f, angle+Random.Range(-j*4,j*4), BulletType.GreenDarkBlade);

            }
            angle += Random.Range(30f, 60f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1f);

        ChooseRandomPattern();
        yield break;
    }

    // LASER!!!
    IEnumerator Pattern4()
    {
        int i = 0;
        float angle = 30;
        float anglechange = 3;
        while(b.health > 0)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++) {
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(i + j * 120 * Mathf.Deg2Rad) * 120f, transform.position.y + Mathf.Sin(i + j * 120 * Mathf.Deg2Rad) * 120f);
                BulletManager.ShootBullet(pos, 0, angle + k*120, 0.5f, 10, 0, BulletType.YellowBlade);
                }
            }

            BulletManager.ShootBullet(transform.position, 8, -angle*7, -0.05f, 4, 0, BulletType.PurpleDot);
            i++;
            anglechange = Mathf.Min(anglechange + 0.1f, 9f);
            angle += anglechange;
            yield return new WaitForSeconds(0.20f);
        }
    }
}
