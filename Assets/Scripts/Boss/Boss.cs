using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour {

    public float health;
    public float[] healthThresholds;
    [HideInInspector]
    public int phase;
    [HideInInspector]
    public int maxPhase;
    public GameObject mesh;
    public GameObject ps;
    public GameObject[] splat;
    bool blinking = false;
    MeshRenderer[] meshList;
    int moveCounter = 0;
    public GameObject levelEnd;
    Vector3 meshStartingAngle;
    [HideInInspector]
    public bool tiltEnabled;

	//JUSTIN
	public DialogueBox dialog;
	public static bool isOnBossStart;
	//JUSTIN

	ScreenFade fadeScript;

    // Use this for initialization
    void Start ()
    {
        fadeScript = GameObject.FindObjectOfType<ScreenFade>();
        fadeScript.StartCoroutine(fadeScript.FadeFromBlack());
        meshList = GetComponentsInChildren<MeshRenderer>();
        phase = 0;
        maxPhase = healthThresholds.Length;
        meshStartingAngle = mesh.transform.eulerAngles; ;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.position.x / 20, Mathf.Sin(Time.time*2) * 4));
        if (tiltEnabled)
        {
            mesh.transform.localRotation = Quaternion.Euler(new Vector3(meshStartingAngle.x, meshStartingAngle.y + transform.position.x / 20f, meshStartingAngle.z + Mathf.Sin(Time.time * 2) * 4));
        } else
        {
            mesh.transform.localRotation = Quaternion.Euler(new Vector3(meshStartingAngle.x, meshStartingAngle.y, meshStartingAngle.z + Mathf.Sin(Time.time * 4) * 4));
        }
    }

    public void DealDamage(float dmg)
    {
        health -= dmg;

        Blink();

        if (phase != maxPhase)
        {
            if (health <= healthThresholds[phase])
            {
                Debug.Log("New boss phase");
                ParticleBurst();
                GameObject.FindObjectOfType<Score>().handleEnemyDefeated(PointVals.BOSS_NEW_STAGE);
                phase++;
            }
        }

        if (health < 0) {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        for (int i = 0; i < 40; i++)
        {

            Instantiate(splat[Random.Range(0, splat.Length)], new Vector2(transform.position.x + Random.Range(-100,100),transform.position.y+Random.Range(-100,100)), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        GameObject.FindObjectOfType<Score>().handleEnemyDefeated(PointVals.BOSS_DEFEATED);

		StartCoroutine(DeathDialog());
    }

	//JUSTIN
	IEnumerator DeathDialog()
	{
		//handle pre-fade dialog
		dialog.isSkipping = false;
		yield return new WaitForSeconds(1.5f);
		SceneIndex currentLevel = (SceneIndex)SceneManager.GetActiveScene().buildIndex;
		Coroutine co;
		switch(currentLevel)
		{
		case SceneIndex.GAMEPLAY_TUTORIAL_3:	//scorption finishing dialog
			co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "I did it!"));
			yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.COLONEL, "I knew you had it in you, Roger."));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.COLONEL, "Now return to base for your briefing."));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			break;

		case SceneIndex.GAMEPLAY_1_3:			//beetle finishing dialog
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.STAMPER, "Nice work saving the city, Roger."));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "I'm gonna do another sweep of the area, try to squash any stragglers."));
			yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "See you guys back at base in a bit."));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			break;

		case SceneIndex.GAMEPLAY_2_3:			//moth finishing dialog
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "Roger! Finally, a good connection!"));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.STAMPER, "What happened?"));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.ROGER, "There was a giant moth. Whatever it was doing here must have been interfering with the satellite equipment in the area."));
			yield return dialog.WaitForSecondsOrSkip(2.5f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "But I took care of it."));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "Good work, Roger. Glad you're safe."));
			yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
			break;

		case SceneIndex.GAMEPLAY_3_3:			//spider finishing dialog
			co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "Nice work, Roger. The swarm won't be building a hive here any time soon."));
			yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
			co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "I'm just glad this one's dead. I can't deal with spiders this big."));
			yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
			break;

		case SceneIndex.GAMEPLAY_4_2:			//dark ship finishing dialog
			//TODO
			break;
		}

		//fade the screen
		fadeScript.Fade();
		yield return new WaitForSeconds(2f);

		//if we've already beaten this level, don't show plot-advancing dialogue
		SavedGameManager sgm = GameObject.FindObjectOfType<SavedGameManager>();
		if(sgm.getCurrentLevelHighscore(currentLevel) == 0)
		{
			//handle post-fade dialog
			dialog.isSkipping = false;
			switch(currentLevel)
			{
			case SceneIndex.GAMEPLAY_TUTORIAL_3:	//if on desert 3...

				co = StartCoroutine(dialog.handleDialogue(3f, Characters.COLONEL, "Roger, you did an excellent job defending our base from this swarm."));
				yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "Thank you, sir."));
				yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "We've determined that these creatures are alien, and landed here by a meteor to the south."));
				yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "My supervision is needed to deal with this, so I'm taking myself off of radio support."));
				yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "But I will be receiving your mission reports, and you're in capable hands with Martha and Stamper."));
				yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "Understood, sir."));
				yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(4f, Characters.COLONEL, "There are three areas where an invasion is imminent. I am giving you clearance to pursue these in any order you choose."));
				yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
				co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.COLONEL, "Good luck out there."));
				yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
				break;

			case SceneIndex.GAMEPLAY_1_3:			//if on level 1, 2, or 3 boss...
			case SceneIndex.GAMEPLAY_2_3:
			case SceneIndex.GAMEPLAY_3_3:
				
				//count middle levels that we've already cleared (if a level's highscore is nonzero, we've already cleared it)
				int midLevelsCleared = 0;
				midLevelsCleared += sgm.getCurrentLevelHighscore(SceneIndex.GAMEPLAY_1_3) == 0 ? 0 : 1;
				midLevelsCleared += sgm.getCurrentLevelHighscore(SceneIndex.GAMEPLAY_2_3) == 0 ? 0 : 1;
				midLevelsCleared += sgm.getCurrentLevelHighscore(SceneIndex.GAMEPLAY_3_3) == 0 ? 0 : 1;
				switch(midLevelsCleared)
				{
				case 0:		//haven't cleared any mid levels yet (this is our first one)
					co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Hey, Martha. Can you do me a favor?"));
					yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "Sure. What do you need exactly?"));
					yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Something about this invasion is making my head spin."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.ROGER, "Can you triangulate the source of the swarms based on where they're showing up?"));
					yield return dialog.WaitForSecondsOrSkip(2.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "That's a little above my paygrade, but sure."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Thank you. Try to keep this all under the table."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					break;

				case 1:		//1 level cleared (this is our second one)
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "How's our little project coming along, Martha?"));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "I'm making some headway. May I ask what this is all about?"));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.ROGER, "Honestly, there's a few things about this invasion that aren't making much sense."));
					yield return dialog.WaitForSecondsOrSkip(2.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(4f, Characters.ROGER, "For starters, have you noticed that these 'aliens' bear an uncanny resemblance to Earth insects?"));
					yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2f, Characters.MARTHA, "I have..."));
					yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(4f, Characters.ROGER, "The Colonel may be working under bad intel. But I don't want to bring it up until we have something more to show him."));
					yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.ROGER, "Or even..."));
					yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "Something to question him on?..."));
					yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "I didn't want to say that, but yes."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "I understand. If I find anything out, you'll be the first to know."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					break;

				case 2:		//2 levels cleared (this is our last one)
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.MARTHA, "Roger, I've finished triangulating the source of the invasion."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2f, Characters.ROGER, "What did you find?"));
					yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "Well, at first it checked out that the source was a moving location, but it was no meteor."));
					yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2.5f, Characters.MARTHA, "The invasion came from the moon."));
					yield return dialog.WaitForSecondsOrSkip(1.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "The moon? Didn't the government authorize a research base up there?"));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(4f, Characters.MARTHA, "I keep trying to get the Colonel on the horn, but none of the higher ups have been returning my calls."));
					yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Hmmm..."));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3f, Characters.ROGER, "Stamper, is this thing rated for space travel?"));
					yield return dialog.WaitForSecondsOrSkip(2f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(4f, Characters.STAMPER, "...It has rocket capability. But the hull was never tested for those speeds and pressures."));
					yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(3.5f, Characters.ROGER, "Well in that case, now seems like a good time to start trials."));
					yield return dialog.WaitForSecondsOrSkip(2.5f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2f, Characters.MARTHA, "What?!"));
					yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(2f, Characters.STAMPER, "You can't be serious."));
					yield return dialog.WaitForSecondsOrSkip(1f); if(co != null) StopCoroutine(co);
					co = StartCoroutine(dialog.handleDialogue(4f, Characters.ROGER, "I'm willing to bet my career I know where the Colonel's been. And I have a few questions for him..."));
					yield return dialog.WaitForSecondsOrSkip(3f); if(co != null) StopCoroutine(co);
					break;

				default:	//3 or undefined levels cleared (do nothing)
					break;
				}
				break;

			case SceneIndex.GAMEPLAY_4_2:
				//TODO
				break;

			default:
				break;
			}
		}

		//handle level completion panel
		levelEnd.GetComponent<LevelCompleteHandler>().handleLevelCompleted(currentLevel);
		yield break;
	}
	//JUSTIN

    public void ParticleBurst()
    {
        StartCoroutine(ParticleBurstCo());
    }

    IEnumerator ParticleBurstCo()
    {
        ps.GetComponent<ParticleSystem>().Emit(60);
        for (int i = 0; i < 60; i++)
        {
            ps.GetComponent<ParticleSystem>().Emit(5);
            yield return null;

        }
        ps.GetComponent<ParticleSystem>().Play();
    }

    public void Blink()
    {
        blinking = true;
        foreach (MeshRenderer m in meshList)
        {
            if (m)
            {
                m.material.SetColor("_Color", Color.red);
            }
        }

        Invoke("Reveal", .1f);
    }

    void Reveal()
    {
        foreach (MeshRenderer m in meshList)
        {
            if (m){
                m.material.SetColor("_Color", Color.white);
            }
        }

        blinking = false;

    }

    public void MoveRandomly()
    {
        StartCoroutine(MoveRandomlyCo());
    }

    IEnumerator MoveRandomlyCo()
    {
        moveCounter++;
        int currentMove = moveCounter;
        float sx = transform.position.x;
        float sy = transform.position.y;
        bool leftOrRight = (Random.Range(0, 2) == 0);
        float ex;
        if (leftOrRight) { ex = Mathf.Clamp(sx + Random.Range(100, 200), -400, 400); }
        else { ex = Mathf.Clamp(sx - Random.Range(100, 200), -400, 400); }
        float ey = Mathf.Clamp(sy + Random.Range(-50, 50), 80, 250);

        for (int i = 1; i <= 50; i++)
        {
            if (moveCounter != currentMove) { yield break; }
            transform.position = new Vector3(Mathf.SmoothStep(sx, ex, i / 50f), Mathf.SmoothStep(sy, ey, i / 50f), 0);
            yield return null;
        }
    }

    public void MoveTo(Vector2 dest)
    {
        StartCoroutine(MoveToCo(dest));
    }

    IEnumerator MoveToCo(Vector2 dest)
    {
        moveCounter++;
        int currentMove = moveCounter;
        float sx = transform.position.x;
        float sy = transform.position.y;
        float ex = dest.x;
        float ey = dest.y;

        for (int i = 1; i <= 30; i++)
        {
            if (moveCounter != currentMove) { yield break; }
            transform.position = new Vector3(Mathf.SmoothStep(sx, ex, i / 30f), Mathf.SmoothStep(sy, ey, i / 30f), 0);
            yield return null;
        }
    }
}
