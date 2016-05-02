using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipPatterns : MonoBehaviour
{

    public GameObject trueLastBoss;
    public GameObject firstSong;
    public GameObject secondSong;
    [HideInInspector]
    public float songDelay = 0f;

    bool phaseJustChanged = false;
    int currentPhase = 0;
    bool bossDead = false;
    List<int> attackQueue;
    Boss b;

    public DialogueBox dialog;  //JUSTIN

    // Use this for initialization
    void Start()
    {
        b = GetComponent<Boss>();
        b.tiltEnabled = false;
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

        trueLastBoss.SetActive(true);
        yield break;
    }

    IEnumerator SwitchSongs()
    {
        AudioSource song1 = firstSong.GetComponent<AudioSource>();
        AudioSource song2 = secondSong.GetComponent<AudioSource>();
        for (float v = 100; v >= 0; v--)
        {
            song1.volume = v / 100f;
            yield return new WaitForSeconds(0.05f);
        }
        song1.Stop();

        while (songDelay < 7.5f)
        {
            songDelay += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        song2.Play();
        Debug.Log("Done switching songs.");
        gameObject.SetActive(false);
        yield break;
    }

    IEnumerator FlyIn()
    {
        //JUSTIN
        Boss.isOnBossStart = true;
        Coroutine co;
        yield return new WaitForSeconds(1.5f);
        co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Oh, no. Please not him..."));
        yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
        //JUSTIN

        for (int i = 0; i < 120; i++)
        {
            transform.Translate(new Vector3(0, -700f / 120f, 0));
            yield return null;
        }

        //JUSTIN
        co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "I believe you two know each other from academy. Chad Trey-Blake is the finest pilot to ever grace the skies, let alone our organization."));
        yield return dialog.WaitForSecondsOrSkip(3f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.DOUCHE, "What's shakin'?"));
        yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "That ship of yours looks pretty familiar..."));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.DOUCHE, "You could say that."));
        yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.DOUCHE, "Except yours was a prototype for what mine is. It's like yours, but better."));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Idle talk. It's all down to who's the better pilot."));
        yield return dialog.WaitForSecondsOrSkip(1.5f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "And between the two of us, if I'm remembering Academy correctly, that was me!"));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        co = StartCoroutine(dialog.handleDialogue(3f, Characters.DOUCHE, "We'll see about that!"));
        yield return dialog.WaitForSecondsOrSkip(2f); if (co != null) StopCoroutine(co);
        Boss.isOnBossStart = false;
        //JUSTIN

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
        if (b.health < 0) { return; }
        int chosenAttack;

        if (attackQueue.Count == 0 || phaseJustChanged)
        {
            attackQueue.Clear();
            Debug.Log("Refreshing attack queue for phase " + currentPhase);
            if (currentPhase == 0)
            {
                attackQueue.Add(1);
                attackQueue.Add(2);
                attackQueue.Add(3);
                attackQueue.Add(4);
            }
            else if (currentPhase == 1)
            {
                attackQueue.Add(5);
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
        }

    }

    // Repellets
    IEnumerator Pattern1()
    {

        for (int i = 0; i < 32; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 4 == 0 && i > 4)
            {
                float x = transform.position.x + Random.Range(-180f, 180f);
                float y = transform.position.y + Random.Range(-30f, 30f);
                Vector2 pos = new Vector2(x, y);
                for (int j = 0; j < 4; j++)
                {
                    BulletManager.ShootBullet(pos, 6f + j / 2f, BulletManager.AngleToPlayerFrom(pos), 0.1f, 10f, 0f, BulletType.PurpleDarkBlade);
                }
            }

            if (i % 15 == 0)
            {
                Vector2 pt = BulletManager.PlayerPosition();
                b.MoveTo(new Vector2(pt.x, Mathf.Clamp(pt.y + 100f, 300f, 0f)));
            }

            BulletManager.ShootBullet(new Vector2(transform.position.x - 96f, transform.position.y), 8f + (i % 4), 270f, BulletType.RedFire);
            BulletManager.ShootBullet(new Vector2(transform.position.x + 96f, transform.position.y), 8f + (i % 4), 270f, BulletType.RedFire);

            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1f);
        ChooseRandomPattern();
        yield break;
    }

    // EMP
    IEnumerator Pattern2()
    {
        b.MoveTo(new Vector2(0, 200));
        b.ParticleBurst();
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 30; i++)
        {

            BulletManager.ShootBullet(transform.position, 9f, BulletManager.AngleToPlayerFrom(transform.position) + i * 12f + 6, -0.1f, 2f, 0f, BulletType.GreenDarkWave);
            if (i % 2 == 0)
            {
                BulletManager.ShootBullet(transform.position, 1f, BulletManager.AngleToPlayerFrom(transform.position) + i * 12f, 0.1f, 8f, 0f, BulletType.GreenWave);
            }
        }
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Pesticide
    IEnumerator Pattern3()
    {
        yield return new WaitForSeconds(1f);
        float wave = 0f;


        for (int i = 0; i < 90; i++)
        {
            if (phaseJustChanged)
            {
                yield return new WaitForSeconds(1f);
                ChooseRandomPattern();
                yield break;
            }

            if (i % 15 == 0)
            {
                b.MoveRandomly();
            }

            BulletManager.ShootBullet(transform.position, 6f, 270f + Mathf.Cos(wave * Mathf.Deg2Rad) * 70f, BulletType.GreenDot);
            BulletManager.ShootBullet(transform.position, 8f, 270f + Mathf.Cos((wave+15f) * Mathf.Deg2Rad) * 85f, BulletType.YellowDarkOrb);

            wave += 13.2f;
            yield return new WaitForSeconds(0.07f);
        }
        yield return new WaitForSeconds(0.5f);
        ChooseRandomPattern();
        yield break;
    }

    // Laser
    IEnumerator Pattern4()
    {
        yield return new WaitForSeconds(1f);
        float sign;
        if(Random.value < 0.5)
        {
            sign = 1;
        } else
        {
            sign = -1;
        }

        Vector2 startPos = new Vector2(800 * sign, 200);
        Vector2 endPos = new Vector2(800 * -sign, 300);

        b.MoveTo(startPos);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 30; i++)
        {

            if (i == 5)
            {
                b.MoveToSlow(endPos);
            }

            for (int j = 0; j < 10; j++)
            {

                BulletManager.ShootBullet(transform.position, 3f + j*1.5f, 90f, BulletType.YellowDarkBlade);
                BulletManager.ShootBullet(transform.position, 3f + j * 1.5f, 270f, BulletType.YellowDarkBlade);
            }

            yield return new WaitForSeconds(0.1f);
        }
        b.MoveRandomly();
        yield return new WaitForSeconds(2f);
        ChooseRandomPattern();
        yield break;
    }

    // Spiral
    IEnumerator Pattern5()
    {

        yield return new WaitForSeconds(1f);
        b.MoveTo(new Vector2(0, 100));
        b.ParticleBurst();
        yield return new WaitForSeconds(1f);

        float angle = 60;
        float t = 0.1f;
        int tTimer = 0;

        while(b.health > 0)
        {
            BulletManager.ShootBullet(transform.position, 5, angle, BulletType.RedDarkArrow);
            BulletManager.ShootBullet(transform.position, 6, angle * 1.1f, BulletType.PinkDarkArrow);
            BulletManager.ShootBullet(transform.position, 7, angle * 1.2f, BulletType.PurpleDarkArrow);
            BulletManager.ShootBullet(transform.position, 8, angle * 1.3f, BulletType.BlueDarkArrow);

            angle += 11.3f;

            if(t > 0.04f)
            {
                if(tTimer == 30)
                {
                    t -= 0.01f;
                    tTimer = 0;
                }
                else
                {
                    tTimer++;
                }
            }

            yield return new WaitForSeconds(t);
        }
        yield break;
    }
}
