using UnityEngine;
using System.Collections;

public class HoloDuplicate : MonoBehaviour {
    GameObject holo;

	// Use this for initialization
	void Start ()
    {
        holo = Resources.Load("PlayerBullets/Hologram") as GameObject;
    }

   public void SpawnHolo()
    {
        GameObject p = GameObject.FindWithTag("Player");
        GameObject.Instantiate(holo, new Vector3(p.transform.position.x, p.transform.position.y, 0f), Quaternion.Euler(p.transform.eulerAngles));
        holo.transform.position = p.transform.position;
        Invoke("Destroy(gameObject)", 5);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
