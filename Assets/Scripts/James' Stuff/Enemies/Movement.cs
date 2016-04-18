using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	//Movement Types
	bool linearMov;
	bool sinMov;
	bool quadMov;
	bool oscMov;
	bool followNose; //Likely to be replaced by quadratic(?); leaving it in for now
	bool dive;
	bool topToSide;
	bool sideToBottom;
	bool fromBackground;

	//General Stats
	public float speed;
	float lifeTime;
	float dirX;
	float dirY;
	public float health;

	//sin stats
	float amplitude;
	float period;

	float degrees;

	//quadratic stats


	//oscillation stats
	bool vertical;
	float oscSpeed;
	float bounds;
	bool posDir;

	Vector3 startPos;
	float movementTimer;

	//followNose stats
	public float rotInc;
	public float rotRange;

	bool startRot;
	float rotMax;
	float rotMin;

	//Dive Stats
	public float diveSpeed;
	float diveTime;

	bool rotSet;
	bool diveSet;
	float diveTimer;
	Transform player;
	Vector3 targetPos;

	//Top to side stats
	float yDest;

	//Side to bottom stats
	float xDest;

	float changeTime;
	float changeTimer;
	float turnTimer;

	//From Background stats
	float upTime;
	Vector3 destination;

	float upTimer;
	Vector3 curPos;
	CircleCollider2D col;

	//Array that holds behavior types, just in case you want the thing to wait a bit to start doing something;
	//int[] behaviors;

	float lifeTimer;

	public MeshRenderer mr;

	public bool beenSeen;
	public bool screenDeath;

	public bool doesTilt;

	Vector3 nullZ;

	//JUSTIN
	Score scoreHandle;
	//JUSTIN

	MeshRenderer[] mesh;

	public GameObject[] splat;

	bool blinking;

	void Awake(){
		lifeTimer = 0;
		linearMov = false;
		sinMov = false;
		quadMov = false;
		oscMov = false;
		followNose = false;
		dive = false;
		topToSide = false;
		sideToBottom = false;
		fromBackground = false;
		blinking = false;

		diveSpeed = 0;

		col = GetComponent<CircleCollider2D> ();

		doesTilt = true;
		screenDeath = true;

		mesh = GetComponentsInChildren<MeshRenderer> ();

		//JUSTIN
		//get handle to score HUD object
		scoreHandle = GameObject.Find("Score").GetComponent<Score>();
		//JUSTIN
	}

	// Use this for initialization
	void Start () {
		beenSeen = false;
		//Vector 3 that prevents movement on z due to rotation
		nullZ = new Vector3 (1, 1, 0);

	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		if (mr.isVisible) {
			beenSeen = true;
		}
		if (linearMov) {
			HandleLinear ();
		}
		if (sinMov) {
			HandleSin ();
		}
		if (quadMov) {
			HandleQuadratic ();
		}
		if (oscMov) {
			HandleOsc ();
		}
		if (followNose) {
			HandleFollow();
		}
		if (dive) {
			HandleDive ();
		}
		if (topToSide) {
			HandleTopToSide ();
		}
		if (sideToBottom) {
			HandleSideToBottom ();
		}
		if (fromBackground) {
			HandleFromBackground ();
		}
		if (doesTilt) {
			handleTilt ();
		}
		HandleLife ();

	}

	void handleTilt(){
		transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.position.x / 20, transform.eulerAngles.z));
	}

	public void Blink(){

		blinking = true;

		foreach (MeshRenderer m in mesh) {
			if (m) {
				m.material.SetColor ("_Color", Color.red);
			}
		}

		Invoke ("Reveal", .1f);
	}

	void Reveal(){
		
		foreach (MeshRenderer m in mesh) {
			if (m) {
				m.material.SetColor ("_Color", Color.white);
			}
		}

		blinking = false;

	}

	void HandleLife(){
		lifeTimer += Time.deltaTime;
		if (health <= 0) {

			//JUSTIN
			//assign point value
			PointVals enemyType;
			if(name.StartsWith("Drone"))		//if drone...
			{
				enemyType = PointVals.DRONE;
			}
			else if(name.StartsWith("Turret"))	//if bagworm...
			{
				enemyType = PointVals.BAGWORM;
			}
			else 								//if other enemy...
			{
				enemyType = PointVals.OTHER;
			}

			//apply change to score
			scoreHandle.handleEnemyDefeated(enemyType);
			//JUSTIN

			Instantiate (splat [Random.Range (0, splat.Length)], transform.position, Quaternion.identity);

			//kc.score++;
			Destroy (gameObject);
		}
		//If it's alive too long, destroy it
		if (lifeTimer < lifeTime) {
			lifeTimer += Time.deltaTime;
		} else {
			Destroy (gameObject);
		}
		//If it's been on and hten off screen, destroy it
		if (!mr.isVisible && beenSeen && screenDeath && !blinking) {
			Destroy (gameObject);
		}
	}


	//New Script

	//Set/handle functions
	//Most of these will take a speed parameter...currently default speed is 200, so keep that in mind
	public void SetLinear(/*float linSpeed, float horDir, float verDir*/){
		/*speed = linSpeed;
		dirX = horDir;
		dirY = verDir;*/
		linearMov = true;
	}

	void HandleLinear(){
		transform.position += new Vector3 (speed * dirX * Time.deltaTime, speed *dirY* Time.deltaTime,0f);
	}

	/* Question: variable speeds.
	 * Should each movement type have its own speed,
	 * or should everything share the same speed?
	 * If the latter, I'll just make a SetSpeed 
	 * function, otherwise I'll need to make a bunch
	 * of variables
	 */

	//if the latter:
	public void SetGeneral(float genSpeed, float horDir, float verDir, float mLifeTime, float mHealth){
		speed = genSpeed;
		dirX = horDir;
		dirY = verDir;
		lifeTime = mLifeTime;
		health = mHealth;
	}

	//if we're not doing a general, this will need x and y components
	public void SetSin(/*float sinSpeed,*/ float mAmplitude, float mPeriod){
		//speed = sinSpeed;
		amplitude = mAmplitude;
		period = mPeriod;
		//I think this is just always the case?
		degrees = 360;

		sinMov = true;
	}

	//Unless we don't actually move it forward? I guess we could do it with linear movement and just use the offset with this?
	//For now I'll just continue with what we had.
	void HandleSin(){
		Vector3 currentPos = transform.position;
		Vector3 offset = Vector3.zero;
		float degPerSec;
		float rads;
		//These are sin wave functions I found online; can't explain them super well.
		if (dirX != 0 && dirY == 0) {
			currentPos.x += Time.deltaTime * speed * dirX;

			degPerSec = 360.0f / period; 
			degrees = Mathf.Repeat (degrees + (Time.deltaTime * degPerSec * dirX), 360.0f);
			rads = degrees * Mathf.Deg2Rad;

			offset = new Vector3 (0.0f, amplitude * Mathf.Sin (rads), 0.0f);

		} else if (dirY != 0 && dirX == 0) {
			currentPos.y += Time.deltaTime * speed * dirY;

			degPerSec = 360.0f / period; 
			degrees = Mathf.Repeat (degrees + (Time.deltaTime * degPerSec *dirY), 360.0f);
			rads = degrees * Mathf.Deg2Rad;

			offset = new Vector3 (amplitude * Mathf.Sin (rads), 0.0f, 0.0f);

		}
		//if we want to make something sin along diagonally, we'll have to figure that out
		//Alternatively, give something an empty parent object and have it oscillate along its localPosition while moving the parent diagonally
		else if (dirY != 0 && dirX != 0) {

		}

		transform.position = currentPos + offset;
	}

	public void SetQuadratic(){

		quadMov = true;
	}

	void HandleQuadratic(){

		transform.position += new Vector3 (speed * dirX * Time.deltaTime, speed *dirY* Time.deltaTime,0f);
	}

	/* vertical will cause the enemies to go up and down when true or left and right when false,
	 * posDir lets the enemies know to start oscilating in the negative (down, left; if false) or positive(up, right; if true)
	 * direction when they first start moving.
	 */

	public void SetOsc(float mOscSpeed, float mBounds, bool mVertical, bool mPosDir){
		oscSpeed = mOscSpeed;
		bounds = mBounds;
		vertical = mVertical;
		posDir = mPosDir;

		startPos = transform.position;
		movementTimer = 0;
		oscMov = true;
	}

	void HandleOsc(){
		//if (mr.isVisible) {
		movementTimer += Time.deltaTime;
		if (!vertical) {
			if (posDir) {
				if (transform.position.x < startPos.x + bounds) {
					transform.position = new Vector3 (Mathf.SmoothStep (transform.position.x, startPos.x  + bounds, movementTimer / 1), transform.position.y, 0f);
				} else {
					posDir = false;
					movementTimer = 0;
				}
			} else {
				if (transform.position.x > startPos.x - bounds) {
					transform.position = new Vector3 (Mathf.SmoothStep (transform.position.x, startPos.x  - bounds, movementTimer / 1), transform.position.y, 0f);
				} else {
					posDir = true;
					movementTimer = 0;
				}
			}
		} else {
			if (posDir) {
				if (transform.position.y < startPos.y  + bounds) {
					transform.position = new Vector3 (transform.position.x, Mathf.SmoothStep (transform.position.y, startPos.y + bounds, movementTimer / 1), 0f);
				} else {
					posDir = false;
					movementTimer = 0;
				}
			} else {
				if (transform.position.y > startPos.y - bounds) {
					transform.position = new Vector3 (transform.position.x, Mathf.SmoothStep (transform.position.y, startPos.y - bounds, movementTimer / 1), 0f);
				} else {
					posDir = true;
					movementTimer = 0;
				}
			}
		}
		//}
	}

	//RotInc is the degree amount the enemy rotates on each update
	//It can be positive or negative
	public void SetFollow(float mRotInc, float mRotRange){
		rotInc = mRotInc;
		rotRange = mRotRange;

		rotRange %= 360;
		rotRange = Mathf.Abs (rotRange);
		if (rotRange != 0) {
			rotMax = (transform.eulerAngles.z + rotRange)%360;
			rotMin = ((transform.eulerAngles.z+360) - rotRange)%360;
			Debug.Log ("Rotmin: " + rotMin);
			Debug.Log ("Rotmax: " + rotMax);
		}
		startRot = true;
		followNose = true;
	}

	void HandleFollow(){
		if (startRot) {
			transform.eulerAngles += new Vector3 (0f, 0f, rotInc);
		} else {
			transform.eulerAngles -= new Vector3 (0f, 0f, rotInc);
		}
		if (rotRange != 0) {
			if ((transform.eulerAngles.z > rotMax && rotMax > rotMin) || (transform.eulerAngles.z > rotMax && transform.eulerAngles.z < rotMin && startRot)) {
				startRot = false;
			} else if ((transform.eulerAngles.z < rotMin && rotMax > rotMin) || (transform.eulerAngles.z > rotMax && transform.eulerAngles.z < rotMin && !startRot)) {
				startRot = true;
			}
		}
		transform.position += transform.up* -1 * speed * Time.deltaTime*dirY;
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
	}

	public void SetDiveAtPlayer(float mDiveSpeed, float mDiveTime/*, float mDirX*/){
		diveSpeed = mDiveSpeed;
		diveTime = mDiveTime;
		//dirX = mDirX;

		diveSet = false;
		rotSet = false;
		diveTimer = 0;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		dive = true;
	}

	void HandleDive(){
		diveTimer += Time.deltaTime;
		if (diveTimer > diveTime && !diveSet) {
			diveSet = true;
			targetPos = player.position;
		} else if (diveSet) {
			Vector3 dir = targetPos - transform.position;
			if (!rotSet) {
				transform.rotation = Quaternion.LookRotation (Vector3.forward, dir);
				rotSet = true;
			}
			transform.position += transform.up * diveSpeed * Time.deltaTime;
			transform.position = new Vector3(transform.position.x,transform.position.y,0);
		} else {
			transform.position += new Vector3 (speed * dirX * Time.deltaTime, speed * dirY * Time.deltaTime,0f);
		}
	}

	public void SetTopToSide(/*float mDirX, float mDirY,*/ float mChangeTime){
		/*dirX = mDirX;
		dirY = mDirY;*/
		changeTime = mChangeTime;

		changeTimer = 0;
		turnTimer = 0;
		topToSide = true;
	}

	void HandleTopToSide(){
		changeTimer += Time.deltaTime;
		if (changeTimer < changeTime) {
			transform.position += new Vector3 (0f, speed * dirY * Time.deltaTime, 0f);
		} else {
			turnTimer += Time.deltaTime;
			if (dirX > 0) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0f, 0f, 90f), turnTimer);
			} else {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (0f, 0f, 270f), turnTimer);
			}
			transform.position += transform.up* -1 * speed * Time.deltaTime;
		}
	}

	public void SetSideToBottom(/*float mDirX, float mDirY,*/ float mChangeTime){
		/*dirX = mDirX;
		dirY = mDirY;*/
		changeTime = mChangeTime;

		changeTimer = 0;
		turnTimer = 0;
		sideToBottom = true;
	}

	void HandleSideToBottom(){
		changeTimer += Time.deltaTime;
		if (changeTimer < changeTime) {
			transform.position += new Vector3 (speed * dirX * Time.deltaTime, 0f, 0f);
		} else {
			turnTimer += Time.deltaTime;
			if (dirX > 0) {
				dirX -= 1;
			} else {
				dirX = 0;
			}
			transform.position += new Vector3 (speed * dirX * Time.deltaTime, 0f, 0f);

			transform.position += new Vector3 (0f, speed * dirY * Time.deltaTime, 0f);
		}
	}

	public void SetFromBackground(/*float mDirX, float mDirY,*/ float mUpTime, Vector3 mDestination){
		/*dirX = mDirX;
		dirY = mDirY;*/
		upTime = mUpTime;
		destination = mDestination;

		col = GetComponent<CircleCollider2D> ();
		col.enabled = false;
		upTimer = 0;
		curPos = transform.position;
		transform.localScale = new Vector3 (.01f, .01f, .01f);
		fromBackground = true;
	}

	void HandleFromBackground(){
		if (/*!destReached && (transform.localScale.z < 1 || Vector2.Distance(new Vector2(transform.position.x,transform.position.y),new Vector2(destination.x,destination.y)) > 10)*/ upTimer < upTime  ) {
			upTimer += Time.deltaTime;
			transform.localScale = Vector3.Lerp (Vector3.zero, new Vector3 (1, 1, 1), upTimer / upTime);
			transform.position = Vector3.Lerp (curPos, destination, upTimer / upTime);
		} else{
			if (!col.enabled) {
				col.enabled = true;
			}
			transform.localScale = new Vector3(1,1,1);
			transform.position += new Vector3 (speed * dirX * Time.deltaTime, speed *dirY* Time.deltaTime,0f);
		}
	}

	//Instead of the whole wavemanager biz, maybe a simple wait time function?
	//Here's a half-thought-up example
	public void SetWaitTime(float waitTime, int[] b){
		//most of the these would be classwide varibles, but I'm just putting them in here now
		//Since we're spawning everything on a timer, it makes sense to just tell each spawn to wait the right amount of time to start their behaviors

		//is it ok to do this in a function that you call on awake/start?
		StartCoroutine(WaveSet(waitTime, b));

		//I recommend using this last if you're gonna use it
	}

	//So this just sets things to true after the proper amount time.
	//The case numbers you see in the switch are the ones you'll want to set in the int array in SetWaitTime() for their corresponding behavior
	IEnumerator WaveSet(float time, int[] behaviors){
		//you'll still ned to assign the appropriate behaviors/their values, so this will need to be someWhere
		yield return null;
		for (int i = 0; i < behaviors.Length; i++) {
			switch (behaviors [i]) {
			case 0:
				linearMov = false;
				break;
			case 1:
				sinMov = false;
				break;
			case 2:
				quadMov = false;
				break;
			case 3:
				oscMov = false;
				break;
			case 4:
				followNose = false;
				break;
			default:
				break;
			}
			//yield return null;
		}
		yield return new WaitForSeconds (time);
		for (int i = 0; i < behaviors.Length; i++) {
			switch (behaviors [i]) {
			case 0:
				linearMov = true;
				break;
			case 1:
				sinMov = true;
				break;
			case 2:
				quadMov = true;
				break;
			case 3:
				oscMov = true;
				break;
			case 4:
				followNose = true;
				break;
			default:
				break;
			}
			//yield return null;
		}
		yield break;
	}
}

