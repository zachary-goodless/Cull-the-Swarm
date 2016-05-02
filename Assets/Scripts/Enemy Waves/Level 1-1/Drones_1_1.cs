﻿using UnityEngine;
using System.Collections;

public class Drones_1_1 : MonoBehaviour {
	//We'll need to assign this with .AddComponent in the spawns.

	public string pattern;
	bool ongoing;

	//JUSTIN
	public bool canFire = true;
	public float fireDisabledCooldown = 0f;
	//JUSTIN

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	//JUSTIN
	public void setFireDisabled(float fdc)
	{
		if(canFire)
		{
			canFire = false;
			fireDisabledCooldown = fdc;

			StartCoroutine(handleFireDisabledCooldown());
		}
	}

	IEnumerator handleFireDisabledCooldown()
	{
		yield return new WaitForSeconds(fireDisabledCooldown);
		canFire = true;

		yield break;
	}
	//JUSITN

    public void Start1() {
        StartCoroutine(DroneWave1());
    }
	
	//Sput your coroutines here.

	public IEnumerator DroneWave1(){
        int loops = 10;
        float cooldown = 0.8f;

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < loops; i++) {
			if(canFire)
			{
				Vector2 pos = transform.position;
            	BulletManager.ShootBullet(pos, 2, BulletManager.AngleToPlayerFrom(pos), 0.05f, 12, 0, BulletType.BlueDot);
			}
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
			if(canFire)
			{
           		Vector2 pos = transform.position;
            	BulletManager.ShootBullet(pos, 12, Random.Range(260,280), -0.2f, 0, 0, BulletType.RedDot);
            	BulletManager.AddAction(new BulletAction(180, false, 0, 270, 0.02f, 4, 0));
                BulletManager.AddAction(new BulletAction(1, BulletType.RedArrow));
            }
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
			if(canFire)
			{
            	Vector2 pos = transform.position;
            	BulletManager.ShootBullet(pos, 12, angle, -0.5f, 3, 0, BulletType.GreenShard);
            	angle += 124.3f;
			}
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
				if(canFire)
				{
              		BulletManager.ShootBullet(pos, 2, 90, 0.2f, 12, -curve, BulletType.CyanBlade);
                	BulletManager.ShootBullet(pos, 2, 270, 0.2f, 12, curve, BulletType.CyanBlade);
				}
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
				if(canFire && pos.y > -50)
				{
                	BulletManager.ShootBullet(pos, 10, ang + j * 360f / ringnum, -0.4f, 4, 0, BulletType.RedDarkDot);
                	BulletManager.ShootBullet(pos, 14, ang + (j + 0.5f) * 360f / ringnum, -0.4f, 8, 0, BulletType.BlueDarkDot);
				}
            }
            yield return new WaitForSeconds(cooldown);
        }

    }

    public void Start6() {
        StartCoroutine(DroneWave6());
    }

    public IEnumerator DroneWave6() {
        int ringnum = 6;

        yield return new WaitForSeconds(Random.Range(0.5f,2f));
        Vector2 pos = transform.position;
        float ang = BulletManager.AngleToPlayerFrom(pos);
        for (int j = 0; j < ringnum; j++) {
			if(canFire)
			{
            	BulletManager.ShootBullet(pos, 1, ang + j * 360f / ringnum, 0.04f, 4, 0, BulletType.GreenDarkOrb);
			}
        }

    }

	public IEnumerator DroneWave7() {
		 

		yield return new WaitForSeconds (Random.Range (1f, 3f));
		for(int i = 0; i < 6; i++)
        {
            if (canFire)
            {
                BulletManager.ShootBullet(transform.position, 1, BulletManager.AngleToPlayerFrom(transform.position) + i * 60, 0.05f, 20f, 0, BulletType.RedDarkBlade);
            }

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
			if(canFire)
			{
            	Vector2 pos = transform.position;
            	BulletManager.ShootBullet(pos, 12, angle, -0.5f, 6, 0, BulletType.CyanShard);
            	angle = (angle + 90) % 360;
			}
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
			if(canFire)
			{
           		Vector2 pos = transform.position;
            	BulletManager.ShootBullet(pos, 12, angle, -0.5f, 6, 0, BulletType.YellowShard);
            	angle = (angle + 180) % 360;
			}
            yield return new WaitForSeconds(cooldown);
        }

    }

	public IEnumerator TurretWave4() {
		int loops = 10;

		yield return new WaitForSeconds (Random.Range (1f, 3f));
		Vector2 pos = transform.position;
		for(int i = 0; i < loops; i++)
        {
            if (canFire)
            {
                BulletManager.ShootBullet(pos, 4, 270, BulletType.WhiteDarkDot);
                BulletManager.ShootBullet(pos, 4, 90, BulletType.WhiteDarkDot);
            }
			yield return new WaitForSeconds (0.1f);
		}
	
	}
		

	//drones will have prefix 'd', turrets will have prefix 't'

	public void StartWaveNum(string num){
		switch (num) {
		case "d1":
			StartCoroutine (DroneWave1 ());
			break;
		case "d4":
			StartCoroutine (DroneWave4 ());
			break;
		case "d5":
			StartCoroutine (DroneWave5 ());
			break;
		case "d6":
			StartCoroutine (DroneWave6 ());
			break;
		case "d7":
			StartCoroutine (DroneWave7 ());
			break;
		case "d23":
			StartCoroutine (DroneWave23 ());
			break;
		case "t1":
			StartCoroutine (TurretWave1 ());
			break;
		case "t2":
			StartCoroutine (TurretWave2 ());
			break;
		case "t3":
			StartCoroutine (TurretWave3 ());
			break;
		case "t4":
			StartCoroutine (TurretWave4 ());
			break;
		default:
			break;
		}

	}
}
