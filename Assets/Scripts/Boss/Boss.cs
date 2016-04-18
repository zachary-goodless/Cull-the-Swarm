using UnityEngine;
using System.Collections;

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
    ScreenFade fadeScript;
    bool blinking = false;
    MeshRenderer[] meshList;
    int moveCounter = 0;

    // Use this for initialization
    void Start () {
        fadeScript = GameObject.FindObjectOfType<ScreenFade>();
        fadeScript.StartCoroutine(fadeScript.FadeFromBlack());
        meshList = GetComponentsInChildren<MeshRenderer>();
        phase = 0;
        maxPhase = healthThresholds.Length;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.position.x / 20, Mathf.Sin(Time.time*2) * 4));
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
        for (int i = 0; i < 30; i++)
        {

            Instantiate(splat[Random.Range(0, splat.Length)], new Vector2(transform.position.x + Random.Range(-100,100),transform.position.y+Random.Range(-100,100)), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    public void ParticleBurst()
    {
        ps.GetComponent<ParticleSystem>().Emit(100);
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
