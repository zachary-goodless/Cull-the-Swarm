using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScorpionPatterns : MonoBehaviour {

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
        if (b.health < 0) { return; }
        int chosenAttack;

        if (attackQueue.Count == 0 || phaseJustChanged) {
            attackQueue.Clear();
            Debug.Log("Refreshing attack queue for phase " + currentPhase);
            if (currentPhase == 0)
            {
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

    // Shoots a horizontal spread of yellow shard walls
    IEnumerator Pattern1()
    {
        float minSpeed = 1.5f;
        float maxSpeed = 5.5f;

        float leftSpeed = minSpeed;
        float rightSpeed = minSpeed;
        Vector2 startPos = transform.position;

        for(int i = 0; i < 80; i++)
        {
            if (phaseJustChanged)
            {
                ChooseRandomPattern();
                yield break;
            }
            if(i%5 == 0)
            {
                leftSpeed = Random.Range(minSpeed, maxSpeed);
                rightSpeed = Random.Range(minSpeed, maxSpeed);
                yield return new WaitForSeconds(0.15f);
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
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Shoots a red blade starburst
    IEnumerator Pattern2()
    {
        float angle = Random.Range(0, 360);
        for (int i = 0; i < 3; i++)
        {
            if (phaseJustChanged)
            {
                ChooseRandomPattern();
                yield break;
            }
            for (int j = 0; j < 36; j++)
            {
                BulletManager.ShootBullet(transform.position, 6f + Mathf.Sin(j * 50 * Mathf.Deg2Rad) * 3.5f, angle + j * 10, -0.02f, 2, 0, BulletType.RedBlade);
            }
            b.MoveRandomly();
            angle += 170;
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(1.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Shoots a reverse V shape from the left/right claws alternating
    IEnumerator Pattern3()
    {
        b.MoveRandomly();
        yield return new WaitForSeconds(0.5f);
        b.ParticleBurst();
        yield return new WaitForSeconds(1f);

        Vector2 left = new Vector2(transform.position.x - 300, 100);
        Vector2 right = new Vector2(transform.position.x + 300, 100);

        for (int i = -8; i <= 8; i++)
        {
            BulletManager.ShootBullet(left, 3f + Mathf.Abs(i / 2f), BulletManager.AngleToPlayerFrom(left) + i * (5f - Mathf.Abs(i) / 2f), BulletType.RedDot);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = -8; i <= 8; i++)
        {
            BulletManager.ShootBullet(right, 3f + Mathf.Abs(i / 2f), BulletManager.AngleToPlayerFrom(right) + i * (5f - Mathf.Abs(i) / 2f), BulletType.YellowDot);
        }
        yield return new WaitForSeconds(2f);
        for (int i = -8; i <= 8; i++)
        {
            BulletManager.ShootBullet(left, 3f + Mathf.Abs(i / 2f), BulletManager.AngleToPlayerFrom(left) + i * (5f - Mathf.Abs(i) / 2f), BulletType.RedDot);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = -8; i <= 8; i++)
        {
            BulletManager.ShootBullet(right, 3f + Mathf.Abs(i / 2f), BulletManager.AngleToPlayerFrom(right) + i * (5f - Mathf.Abs(i) / 2f), BulletType.YellowDot);
        }
        yield return new WaitForSeconds(2f);

        ChooseRandomPattern();
        yield break;
    }

    // Shoots random yellow "tail" patterns
    IEnumerator Pattern4()
    {
        for (int i = 0; i < 20; i++)
        {
            float angle = Random.Range(250f, 290f);
            float x = Random.Range(-800f, 800f);
            BulletManager.ShootBullet(new Vector2(x, 400), 8+i/10f, angle, BulletType.YellowDarkBlade);
            yield return null;
            for (int j = 1; j < 10; j++)
            {
                BulletManager.ShootBullet(new Vector2(x, 400), 8 - j / 4f + i / 10f, angle, BulletType.YellowDarkArrow);
                yield return null;
            }
        }

        b.MoveRandomly();
        ChooseRandomPattern();
        yield break;
    }
}
