using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootTest : MonoBehaviour {
    float angle = 0;
    int timer = 0;
    
    int count = 3;
    int rate = 12;
    BulletType[] shotList = { BulletType.BlueArrow, BulletType.RedArrow, BulletType.GreenArrow };
    public AudioClip sfx;
    public GameObject text;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (timer % rate == 0) {
            angle = Random.Range(240, 300);


            for (int i = 0; i < count; i++) {
                BulletManager.ShootBullet(new Vector2(Random.Range(-800,800), 540),  10 + i*4, angle, -0.2f, 6, 0, shotList[i]);

            }
            GetComponent<AudioSource>().PlayOneShot(sfx);
        }

        timer++;
    }
}

