using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootTestNormal : MonoBehaviour {
    float angle = 0;
    int timer = 0;
    
    public int count = 5;
    public int rate = 60;
    public AudioClip sfx;
    public GameObject text;

	bool onScreen;
	public MeshRenderer mr;

    // Use this for initialization
    void Start () {
		onScreen = true;
	}

    // Update is called once per frame
    void Update() {

		if (mr.isVisible) {
			onScreen = true;
		} else {
			onScreen = false;
		}
        
		if (onScreen) {
			if (timer % rate == 0) {
                angle = Random.Range (0, 360);


				for (int i = 0; i < count; i++) {
					BulletManager.ShootBullet (transform.position, 20, angle + i * 360 / count, -0.5f, 2, 0,BulletType.PinkOrb);
					BulletManager.AddAction (new BulletAction (60, true, 0, 0, 0.7f, 18, 0));
				}
				GetComponent<AudioSource> ().PlayOneShot (sfx);
			}
		}

        timer++;
    }


}
