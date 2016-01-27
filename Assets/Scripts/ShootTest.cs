using UnityEngine;
using System.Collections;

public class ShootTest : MonoBehaviour {

    BulletManager btm;
    float angle;
    int timer;

	// Use this for initialization
	void Start () {
        btm = GameObject.FindGameObjectWithTag("BulletManager").GetComponent<BulletManager>();
        angle = 0;
        timer = 0;
	}

    // Update is called once per frame
    void Update() {

        if (timer % 8 == 0) {
            for(int i = 0; i < 12; i++)
            btm.ShootBullet(transform.position, 7f, angle + i*Mathf.PI/6);
            angle += 0.2f;
        }

        timer++;
    }
}
