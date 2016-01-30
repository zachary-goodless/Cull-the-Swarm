using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootTest : MonoBehaviour {
    float angle = 0;
    int timer = 0;

    [Range(1, 120)]
    public int count = 5;
    [Range(1, 120)]
    public int rate = 60;
    public bool randomized = false;
    public bool curving = false;
    public AudioClip sfx;
    public GameObject text;


    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown("1"))
        {
            if (count < 80) {
                count++;
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (rate > 1)
            {
                rate--;
            }
        }

        if (timer % rate == 0) {

            if (randomized) {
                angle = Random.Range(0, 360);
            } else {
                angle += 444/count;
            }

            for (int i = 0; i < count; i++) {
                if (curving) {
                    float curveAmount = Random.Range(-0.5f, 0.5f);
                    BulletManager.ShootBullet(transform.position, 20, angle + i * 360 / count, -0.5f, 2, curveAmount);
                    BulletManager.AddAction(new BulletAction(60, true, 0, 0, 0.7f, 18, -curveAmount));
                } else {
                    BulletManager.ShootBullet(transform.position, 20, angle + i * 360 / count, -0.5f, 2, 0);
                    BulletManager.AddAction(new BulletAction(60, true, 0, 0, 0.7f, 18, 0));
                }
            }
            GetComponent<AudioSource>().PlayOneShot(sfx);
        }

        if (timer % 5 == 0) {
            //text.GetComponent<Text>().text = "Number of bullets onscreen: " + GameObject.FindGameObjectsWithTag("Bullet").Length + "\nFiring " + count + " bullets every " + rate + " frames.";
        }

        timer++;
    }


}
