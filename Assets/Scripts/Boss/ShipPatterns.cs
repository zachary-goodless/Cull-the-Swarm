using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipPatterns : MonoBehaviour {

    public GameObject trueLastBoss;
    public GameObject firstSong;
    public GameObject secondSong;

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

	// Use this for initialization
	void Start () {
        b = GetComponent<Boss>();
        b.tiltEnabled = false;
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

    public void TriggerNextBoss()
    {
        StartCoroutine(TriggerNextBossCo());
    }

    public IEnumerator TriggerNextBossCo()
    {
        Debug.Log("Starting transition coroutine.");
        StartCoroutine(SwitchSongs());
        StartCoroutine(FlyAway());
        // to-do: explosion effect when going offscreen

        trueLastBoss.GetComponent<CircleCollider2D>().enabled = true;
        trueLastBoss.GetComponent<Boss>().enabled = true;
        trueLastBoss.GetComponent<QueenPatterns>().enabled = true;
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
        yield break;
    }

    IEnumerator SwitchSongs()
    {
        AudioSource song1 = firstSong.GetComponent<AudioSource>();
        AudioSource song2 = secondSong.GetComponent<AudioSource>();
        for(float v = 100; v >= 0; v--)
        {
            song1.volume = v/100f;
            yield return new WaitForSeconds(0.03f);
        }
        song1.Stop();
        song2.Play();
        Debug.Log("Done switching songs.");
        yield break;
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


    IEnumerator FlyAway()
    {
        for (int i = 0; i < 360; i++)
        {
            transform.Translate(new Vector3(0, 1500f / 360f, 0));
            yield return null;
        }
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
            } else if (currentPhase == 1) {
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

        for(int i = 0; i < 40; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 4 == 0 && i > 4) {
                float x = transform.position.x + Random.Range(-120f, 120f);
                float y = transform.position.y + Random.Range(-30f, 30f);
                Vector2 pos = new Vector2(x, y);
                for (int j = 0; j < 4; j++){
                    BulletManager.ShootBullet(pos, 6f + j / 2f, BulletManager.AngleToPlayerFrom(pos), BulletType.PurpleDarkArrow);
                }
            }

            if (i%15 == 0){
                Vector2 pt = GameObject.FindGameObjectWithTag("Player").transform.position;
                b.MoveTo(new Vector2(pt.x, Mathf.Clamp(pt.y + 100f,300f,0f)));
            }
            
            BulletManager.ShootBullet(new Vector2(transform.position.x-96f,transform.position.y),10f + (i % 4),270f,BulletType.RedFire);
            BulletManager.ShootBullet(new Vector2(transform.position.x+96f,transform.position.y),10f + (i % 4), 270f,BulletType.RedFire);

            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }
}
