using UnityEngine;
using System.Collections;

public class Drones_1_1 : MonoBehaviour {
	//We'll need to assign this with .AddComponent in the spawns.

	public string pattern;
	bool ongoing;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Start1() {
        StartCoroutine(DroneWave1());
    }
	
	//Sput your coroutines here.

	public IEnumerator DroneWave1(){
        int loops = 10;
        float cooldown = 0.8f;

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            BulletManager.ShootBullet(pos, 2, BulletManager.AngleToPlayerFrom(pos), 0.05f, 12, 0, BulletType.BlueDot);
            yield return new WaitForSeconds(cooldown);
        }

	}

    public void Start23() {
        //StartCoroutine(DroneWave23());
    }
    
    public IEnumerator DroneWave23() {
        int loops = 10;
        float cooldown = 0.3f;

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            BulletManager.ShootBullet(pos, 12, Random.Range(260,280), -0.15f, 0, 0, BulletType.RedDot);
            BulletManager.AddAction(new BulletAction(240, false, 0, 270, 0.02f, 4, 0));
            yield return new WaitForSeconds(cooldown);
        }

    }

    public void StartTurret1() {
        StartCoroutine(TurretWave1());
    }

    public IEnumerator TurretWave1() {
        float angle = Random.Range(0, 360);
        int loops = 60;
        float cooldown = 0.035f;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            BulletManager.ShootBullet(pos, 12, angle, -0.5f, 3, 0, BulletType.GreenShard);
            angle += 124.3f;
            yield return new WaitForSeconds(cooldown);
        }

    }

    public void Start4() {
        StartCoroutine(DroneWave4());
    }

    public IEnumerator DroneWave4() {
        int loops = 5;
        int linelength = 3;
        float cooldown = 0.7f;

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            float curve = Random.Range(-0.5f, 0.5f);
            for (int j = 0; j < linelength; j++) {
                BulletManager.ShootBullet(pos, 2, 90, 0.2f, 12, -curve, BulletType.CyanDot);
                BulletManager.ShootBullet(pos, 2, 270, 0.2f, 12, curve, BulletType.CyanDot);
                yield return new WaitForSeconds(linelength * 0.02f);
            }
            yield return new WaitForSeconds(cooldown - linelength * 0.02f);
        }

    }

    public void Start5() {
        StartCoroutine(DroneWave5());
    }

    public IEnumerator DroneWave5() {
        int loops = 2;
        int ringnum = 5;
        float cooldown = 2f;

        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            float ang = Random.Range(0, 360);
            for (int j = 0; j < ringnum; j++) {
                BulletManager.ShootBullet(pos, 12, ang + j * 360f / ringnum, -0.4f, 4, 0, BulletType.RedDarkDot);
                BulletManager.ShootBullet(pos, 16, ang + (j + 0.5f) * 360f / ringnum, -0.4f, 8, 0, BulletType.BlueDarkDot);
            }
            yield return new WaitForSeconds(cooldown);
        }

    }

    public void Start6() {
        StartCoroutine(DroneWave6());
    }

    public IEnumerator DroneWave6() {
        int ringnum = 3;

        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        Vector2 pos = transform.position;
        float ang = BulletManager.AngleToPlayerFrom(pos);
        for (int j = 0; j < ringnum; j++) {
            BulletManager.ShootBullet(pos, 1, ang + j * 360f / ringnum, 0.04f, 4, 0, BulletType.GreenDarkDot);
        }

    }

    public void StartTurret2() {
        StartCoroutine(TurretWave2());
    }

    public IEnumerator TurretWave2() {
        float angle = 45;
        int loops = 120;
        float cooldown = 0.1f;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            BulletManager.ShootBullet(pos, 12, angle, -0.5f, 6, 0, BulletType.CyanShard);
            angle = (angle + 90) % 360;
            yield return new WaitForSeconds(cooldown);
        }

    }

    public void StartTurret3() {
        StartCoroutine(TurretWave3());
    }

    public IEnumerator TurretWave3() {
        float angle = 0;
        int loops = 60;
        float cooldown = 0.2f;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < loops; i++) {
            Vector2 pos = transform.position;
            BulletManager.ShootBullet(pos, 12, angle, -0.5f, 6, 0, BulletType.YellowShard);
            angle = (angle + 180) % 360;
            yield return new WaitForSeconds(cooldown);
        }

    }
}
