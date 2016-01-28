using UnityEngine;
using System.Collections;

public class ShootTest : MonoBehaviour {
    float angle = 0;
    int timer = 0;

    [Range(1, 120)]
    public int count = 10;
    [Range(1, 120)]
    public int rate = 30;
    public bool randomized = false;
    public bool curving = false;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {

        if (timer % rate == 0) {

            if (randomized) {
                angle = Random.Range(0, 360);
            } else {
                angle += 444/count;
            }

            for (int i = 0; i < count; i++) {
                if (curving) {
                    BulletManager.ShootBullet(transform.position, 20, angle + i * 360 / count, -0.5f, 2, Random.Range(-0.5f,0.5f));
                } else {
                    BulletManager.ShootBullet(transform.position, 20, angle + i * 360 / count, -0.5f, 2, 0);
                }
                BulletManager.AddAction(new BulletAction(60, true, 0, 0, 1, 18, 0));
            }
            GetComponent<AudioSource>().Play();
        }

        if (timer % 120 == 0) {
            Debug.Log("Bullets per ring: " + count + "\nBullet fire rate: " + 60f / rate + " per second.");
            Debug.Log("Number of bullets onscreen: " + GameObject.FindGameObjectsWithTag("Bullet").Length);
        }

        timer++;
    }


}
