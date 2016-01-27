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
            for (int i = 0; i < 12; i++)
            {
                GameObject bt = btm.ShootBullet(transform.position, 12, angle + i * 360/12);
                bt.GetComponent<Bullet>().AddAction(new BulletAction(20, true, -1, 30));
                bt.GetComponent<Bullet>().AddAction(new BulletAction(20, true, -1, -90));
            }
            angle += 2f;
        }

        timer++;
    }
}
