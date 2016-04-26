using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MothPatterns : MonoBehaviour {

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

        float angle = 90f;
        float anglechange = 13f;
        float speed = 10f;
        int delay = 60;

        for(int i = 0; i < 50; i++)
        {
            if (phaseJustChanged)
            {
                ChooseRandomPattern();
                yield break;
            }
            for (int j = 0; j < 3; j++)
            {
                BulletManager.ShootBullet(new Vector2(transform.position.x + 240f, transform.position.y), speed, j * 120 + angle, -0.1f, 1, 0, BulletType.GreenDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 120 + angle, 0.05f, 10f, 0));

                BulletManager.ShootBullet(new Vector2(transform.position.x - 240f, transform.position.y), speed, j * 120 - angle + 180, -0.1f, 1, 0, BulletType.GreenDot);
                BulletManager.AddAction(new BulletAction(delay, false, 1, j * 120 - angle + 180, 0.05f, 10f, 0));
            }

            angle += anglechange;
            anglechange -= 0.6f;
            speed -= 0.15f;
            delay++;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(3f);
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
                ChooseRandomPattern();
                yield break;
            }

            int numShots = 8 + i * 2;
            float angle = Random.Range(0, 360);

            for (int j = 0; j < numShots; j++)
            {
                BulletManager.ShootBullet(transform.position, speed+j%2, angle + j * (360/numShots), -0.02f, 3, 0, shotTypes[j%2]);
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
                ChooseRandomPattern();
                yield break;
            }

            float x = p.transform.position.x;
            float y = p.transform.position.y;
            float r = 400 + i * 80;
            int numShots = 6 + i * 3;
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
            if (phaseJustChanged)
            {
                ChooseRandomPattern();
                yield break;
            }
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
}
