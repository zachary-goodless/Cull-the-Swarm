using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	float speed;
	float evasionSpeed;
	float evasionTime;
	float timer;
	float movementTimer;
	float evasionBounds;
	float startX;
	public bool right;
	bool retreat;
	public Vector3 dest;
	public bool destReached;
	public bool hasDest;
	public WaveManager wm;

	public float health;

	float lifeTime;
	float lifeTimer;
	//public KillCount kc;

	public float flightTime;
	public float rotInc;
	public float rotRange;
	public bool hasRange;
	bool rotUp;
	float rotMax;
	float rotMin;
	float degrees;

	public MeshRenderer mr;

	//flight paths:
	public bool vert;
	public bool hori;
	public bool sine;
	public bool followNose;
	public bool oscillate;
	public float dirX;
	public float dirY;

	public bool beenSeen;

	Vector3 nullZ;

	void Awake(){
		vert = false;
		hori = false;
		sine = false;
		followNose = false;
		oscillate = false;
		lifeTime = 40;
		lifeTimer = 0;
	}

	// Use this for initialization
	void Start () {
		speed = 200;
		degrees = 360;
		evasionSpeed = 150;
		evasionTime = 10;
		timer = 0;
		startX = transform.position.x;
		evasionBounds = 150;
		retreat = false;
		movementTimer = 0;

		beenSeen = false;
		//kc = GameObject.Find("KillCount").GetComponent<KillCount>();

		//Vector 3 that prevents movement on z due to rotation
		nullZ = new Vector3 (1, 1, 0);

		if (hasRange) {
			rotMax = (transform.eulerAngles.z + rotRange)%360;
			rotMin = (transform.eulerAngles.z - rotRange)%360;
		}
		rotUp = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		if (mr.isVisible) {
			beenSeen = true;
		}
		if (hasDest) {
			if (!destReached) {
				//transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * speed);
				movementTimer += Time.deltaTime;
				transform.position = new Vector3 (Mathf.SmoothStep (transform.position.x, dest.x, movementTimer / flightTime), Mathf.SmoothStep (transform.position.y, dest.y, movementTimer / flightTime), 0f);
				if (transform.position == dest ) {
					Debug.Log ("Destination reached");
					destReached = true;
					movementTimer = 0;
				}
			}
			if (wm.waveSet) {
				Evasion ();
				if (!retreat) {
					if (timer < evasionTime) {
						timer += Time.deltaTime;
					} else {
						retreat = true;
					}
				}
			}
			if (retreat) {
				transform.position += new Vector3 (0, Time.deltaTime * speed, 0);
			}
		} else {
			if(vert){
				Debug.Log ("vert going through");
				transform.position += new Vector3 (0, speed *dirY* Time.deltaTime,0f);
			}
			if (hori) {
				transform.position += new Vector3 (speed * dirX * Time.deltaTime, 0f, 0f);
			}
			if (followNose) {
				if (rotUp) {
					transform.eulerAngles += new Vector3 (0f, 0f, rotInc);
				} else {
					transform.eulerAngles -= new Vector3 (0f, 0f, rotInc);
				}
				if (hasRange) {
					if ((transform.eulerAngles.z > rotMax && rotMax > rotMin) || (transform.eulerAngles.z > rotMax && transform.eulerAngles.z < rotMin && rotUp)) {
						rotUp = false;
					} else if ((transform.eulerAngles.z < rotMin && rotMax > rotMin) || (transform.eulerAngles.z > rotMax && transform.eulerAngles.z < rotMin && !rotUp)) {
						rotUp = true;
					}
				}
				transform.position += transform.up* -1 * speed * Time.deltaTime*dirY;
				transform.position = new Vector3(transform.position.x,transform.position.y,0);
			}
			if (sine) {
				SineWave ();
			}
			if (oscillate) {
				if (wm) {
					if (wm.waveSet) {
						Evasion ();
					}
				} else {
					Evasion ();
				}
			}


		}
		transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.position.x / 20, transform.eulerAngles.z));
		lifeTimer += Time.deltaTime;
		if (health <= 0) {
			//kc.score++;
			if (wm) {
				wm.enemies.Remove (gameObject);
			}
			Destroy (gameObject);
		}
		if (lifeTimer < lifeTime) {
			lifeTimer += Time.deltaTime;
		} else {
			if (wm) {
				wm.enemies.Remove (gameObject);
			}
			Destroy (gameObject);
		}
		if (!mr.isVisible && beenSeen) {
			if (wm) {
				wm.enemies.Remove (gameObject);
			}
			Destroy (gameObject);
		}
	}

	void SineWave(){
		float amplitude = 10;
		float period = 2;
		Vector3 currentPos = transform.position;
		currentPos.x += Time.deltaTime * speed*dirX;

		float degPerSec = 360.0f / period; 
		degrees = Mathf.Repeat (degrees + (Time.deltaTime * degPerSec*dirX), 360.0f);
		float rads = degrees * Mathf.Deg2Rad;

		Vector3 offset = new Vector3 (0.0f, amplitude*Mathf.Sin (rads), 0.0f);

		transform.position = currentPos + offset;
	}

	void Evasion(){
		if (mr.isVisible) {
			movementTimer += Time.deltaTime;
			if (right) {
				if (transform.position.x < startX + evasionBounds) {
					transform.position = new Vector3 (Mathf.SmoothStep (transform.position.x, startX + evasionBounds, movementTimer / 1), transform.position.y, 0f);
					//transform.position += new Vector3 (Time.deltaTime * evasionSpeed, 0, 0);
				} else {
					right = false;
					movementTimer = 0;
				}
			} else {
				if (transform.position.x > startX - evasionBounds) {
					transform.position = new Vector3 (Mathf.SmoothStep (transform.position.x, startX - evasionBounds, movementTimer / 1), transform.position.y, 0f);
					//transform.position += new Vector3 (Time.deltaTime * -1 * evasionSpeed, 0, 0);
				} else {
					right = true;
					movementTimer = 0;
				}
			}
		}
	}

}
