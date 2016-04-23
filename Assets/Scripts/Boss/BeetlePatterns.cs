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
                attackQueue.Add(3);
            } else if (currentPhase == 1) {
                attackQueue.Add(1);
                attackQueue.Add(2);
                attackQueue.Add(3);
                attackQueue.Add(5);
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
        }

    }

    // Random spread of red and green orbs
    IEnumerator Pattern1()
    {
        float minSpeed = 2f;
        float maxSpeed = 7f;

        for(int i = 0; i < 70; i++)
        {
            float angle = Random.Range(0f, 360f);
            BulletManager.ShootBullet(transform.position, Random.Range(minSpeed, maxSpeed), angle, BulletType.RedDot);
        }

        for (int i = 0; i < 50; i++)
        {
            float angle = Random.Range(0f, 360f);
            BulletManager.ShootBullet(transform.position, Random.Range(minSpeed, maxSpeed-1f), angle, BulletType.GreenOrb);
        }
        yield return new WaitForSeconds(2f);
        b.MoveRandomly();
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Yellow beam straight down
    IEnumerator Pattern2()
    {
        b.MoveRandomly();
        yield return new WaitForSeconds(0.5f);
        b.ParticleBurst();
        yield return new WaitForSeconds(1f);

        float r = 20;
        for (int i = 0; i < 60; i++)
        {
            if (phaseJustChanged)
            {
                ChooseRandomPattern();
                yield break;
            }
            if(i > 20) {
                r += 8;
            }
            for (int j = 0; j < 3; j++)
            {
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(i + j*120 * Mathf.Deg2Rad) * r, transform.position.y + Mathf.Sin(i + j * 120 * Mathf.Deg2Rad) * r / 3f);
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
        for (int i = 0; i < 18; i++)
        {
            BulletManager.ShootBullet(transform.position, 8 + i / 10f, angle, BulletType.GreenDarkBubble);
            for (int j = 0; j < 14; j++)
            {
                BulletManager.ShootBullet(transform.position, 8 - j / 2f + i / 10f, angle+Random.Range(-j*4,j*4), BulletType.GreenDarkBlade);

            }
            angle += Random.Range(30f, 60f);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(1f);

        ChooseRandomPattern();
        yield break;
    }

    // LASER!!!
    IEnumerator Pattern4()
    {
        int i = 0;
        float angle = 90;
        float anglechange = 3;
        while(b.health > 0)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++) {
                Vector2 pos = new Vector2(transform.position.x + Mathf.Cos(i + j * 120 * Mathf.Deg2Rad) * 180f, transform.position.y + Mathf.Sin(i + j * 120 * Mathf.Deg2Rad) * 180f);
                BulletManager.ShootBullet(pos, 0, angle + k*120, 0.4f, 8, 0, BulletType.YellowBlade);
                }
            }

            BulletManager.ShootBullet(transform.position, 8, -angle*7, -0.05f, 2f, 0, BulletType.PurpleDot);
            i++;
            anglechange = Mathf.Min(anglechange + 0.1f, 9f);
            angle += anglechange;
            yield return new WaitForSeconds(0.20f);
        }
    }

    // 
    IEnumerator Pattern5()
    {
        float angle = Random.Range(0f, 360f);
        for (int i = 0; i < 15; i++)
        {

            for (int j = 0; j < 6; j++)
            {
                BulletManager.ShootBullet(transform.position, 12f, 105f + i * 9f, BulletType.PurpleArrow);
                BulletManager.AddAction(new BulletAction(30 + j * 12, false, 0f, 270f, 0.03f, 15f, 0));
                BulletManager.ShootBullet(transform.position, 12f, 75f - i * 9f, BulletType.PurpleArrow);
                BulletManager.AddAction(new BulletAction(30 + j * 12, false, 0f, 270f, 0.03f, 15f, 0));

            }
            if(i%3 == 0) { BulletManager.ShootBullet(transform.position, 18f, BulletManager.AngleToPlayerFrom(transform.position), -0.5f, 3f, 0, BulletType.GreenDarkOrb); }

            
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(3f);

        ChooseRandomPattern();
        yield break;
    }
}
