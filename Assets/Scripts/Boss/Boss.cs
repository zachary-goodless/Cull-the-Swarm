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

    // Use this for initialization
    void Start ()
    {
        ScreenFade fadeScript;
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
        mesh.transform.localRotation = Quaternion.Euler(new Vector3(meshStartingAngle.x, meshStartingAngle.y + transform.position.x / 20f, meshStartingAngle.z + Mathf.Sin(Time.time * 2) * 4));
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

		ScreenFade fadeScript;
		fadeScript = GameObject.FindObjectOfType<ScreenFade>();
		fadeScript.Fade();
		yield return new WaitForSeconds(2f);

        levelEnd.GetComponent<LevelCompleteHandler>().handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

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
        float ey = Mathf.Clamp(sy + Random.Range(-50, 50), 100, 300);

        for (int i = 1; i <= 50; i++)
        {
            if (moveCounter != currentMove) { yield break; }
            transform.position = new Vector3(Mathf.SmoothStep(sx, ex, i / 50f), Mathf.SmoothStep(sy, ey, i / 50f), 0);
            yield return null;
        }
    }
}
