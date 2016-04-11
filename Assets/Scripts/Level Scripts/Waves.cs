using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour{
	public GameObject drone;
	public GameObject turret;
	public GameObject wormHead;
	public GameObject wormBod;
	public GameObject wormTail;

	//float values for above the screen, left of the screen, etc.
	public float upScreen;
	public float downScreen;
	public float leftScreen;
	public float rightScreen;

	void Start(){
		upScreen = transform.position.y;
		downScreen = -1 * upScreen;
		leftScreen = -1250;
		rightScreen = 1250;
	}

	//So far, these waves are specific to a certain kind fo enemy. Need to figure out how to fix that.

	//Bods is # of segments including head, Pos is the position of the head, rot is the rotation of the head
	GameObject MakeWorm(int bods, Vector3 pos, float rot){
		//Puts the segments behind the proper place;
		Vector3 backPush;
		if (rot % 360 <= 45 || rot % 360 > 315) {
			backPush = new Vector3 (0, 70, 0);
		} else if (rot % 360 <= 135 && rot % 360 > 45) {
			backPush = new Vector3 (-70, 0, 0);
		} else if (rot % 360 <= 225 && rot % 360 > 135) {
			backPush = new Vector3 (0, -70, 0);
		} else {
			backPush = new Vector3 (70, 0, 0);
		}

		GameObject head = Instantiate (wormHead, pos, Quaternion.Euler (new Vector3(0,0,rot))) as GameObject;
		head.GetComponent<Movement> ().screenDeath = false;
		WormScript ws = head.GetComponent <WormScript>();
		for (int i = 1; i < bods-1; i++) {
			GameObject bod = Instantiate(wormBod, pos + backPush*i, Quaternion.Euler(new Vector3(0,0,rot-180))) as GameObject;
			ws.segments.Add (bod.transform);
			WormBod wBod = bod.GetComponent<WormBod> ();
			wBod.head = head;

		}
		GameObject tail = Instantiate(wormTail, pos + backPush*(bods-1), Quaternion.Euler(new Vector3(0,0,rot-180))) as GameObject;
		ws.segments.Add (tail.transform);
		WormBod wTail = tail.GetComponent<WormBod> ();
		wTail.head = head;

		return head;
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	IEnumerator Template(){
		GameObject temp;
		Movement m;

		/*Any parameters defined here will be in the order they are assigned in the function
		 * 
		 * 
		*/

		Debug.Log ("Linear");
		temp = Instantiate (drone, new Vector3 (-500, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Just sets the linear boolean to true
		m.SetLinear();
		//So, general stats: speed, x direction, y direction, life time, and health
		//Since both x and y != 0, this will move diagonally
		//You'll need to call this function whenver instantiating something with this movement script
		m.SetGeneral(200,.5f,-.5f,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("SinWave");
		temp = Instantiate (drone, new Vector3 (0, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Set amplitude and period
		m.SetSin(10,1);
		//You'll still need x/y direction; the sin will travel appropriately.
		//However, I don;t have anything set up for both x and y not being 0, so that won;t move
		m.SetGeneral(200,0,-1,40,50);

		//just making sure horizontal works too
		temp = Instantiate (drone, new Vector3 (rightScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetSin(10,1);
		m.SetGeneral(200,-1,0,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("Quadratic");
		temp = Instantiate (drone, new Vector3 (leftScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Currently an empty function; not sure how to do quadratic movement/how we would use it
		//Just does linear movement for now
		m.SetQuadratic ();
		m.SetGeneral(200,1,0,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("Oscillation");
		temp = Instantiate (drone, new Vector3 (leftScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Oscillation: this MUST be used in tandem with another movement function (preferably linear movement)
		//Stats: the speed at which is oscillates, the bounds to which it can osciallte (in positive an negative direction from whichever exis it's oscillating on), 
		//vertical (true) or horizontal(false) oscillation, and starting in the x or y positive (true) or negative(false) direction, depending on vertical or horizontal oscillation.
		m.SetOsc(100,200,true,true);
		m.SetLinear();
		m.SetGeneral(200,1,0,40,50);

		//Vertical example
		temp = Instantiate (drone, new Vector3 (0, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetOsc(100,200,false,true);
		m.SetLinear();
		m.SetGeneral(200,0,-1,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("NoseFollow");
		temp = Instantiate (drone, new Vector3 (0, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Leftover from last script; rotates the enemy and has it move in the direction it's facing
		//Kinda useful if you want curves paterns, or pseudo-sine waves
		//Stats: The rotational increment from each update (in degrees), and the range that it can rotate (if set to zero, the program assumes no range)
		m.SetFollow(1f,50);
		//x direction does nothing, y direction decides to move i the direction of the top of the prefab (-1), or the bottom (1)
		m.SetGeneral(200,0,1,40,50);

		//Example with no range
		temp = Instantiate (drone, new Vector3 (200, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetFollow(.2f,0);
		m.SetGeneral(200,0,1,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("DiveAtPlayer");
		temp = Instantiate (drone, new Vector3 (leftScreen, 400, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Stats: speed at which to dive, and time (in seconds) to wait to dive
		//Currently only moves horizontally, but this can become a function that adds on to other functions
		//(e.g. trave in a sin wave, then dive at player) with some tweaking
		m.SetDiveAtPlayer(400,5);
		//Currently only x direction is relevant
		m.SetGeneral(200,1,0,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("TopToSide");
		temp = Instantiate (drone, new Vector3 (0, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Stats: time until it changes direction
		m.SetTopToSide (5);
		//Need both x and y directions; fairly straightforward.
		m.SetGeneral(200,-1,-1,40,50);

		//testing opposite directions
		temp = Instantiate (drone, new Vector3 (0, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetTopToSide (5);
		m.SetGeneral(200,1,1,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("SideToBottom");
		temp = Instantiate (drone, new Vector3 (leftScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//basically the same as the last one, but start horizontal and changes to vertical
		//stats: time until it changes direction
		m.SetSideToBottom (5);
		//need both x and y directions
		m.SetGeneral(200,1,-1,40,50);

		//testing different direction
		temp = Instantiate (drone, new Vector3 (rightScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetSideToBottom (5);
		m.SetGeneral(200,-1,1,40,50);

		yield return new WaitForSeconds (5);

		Debug.Log ("FromBackground");
		//this will need to start in a different z position
		temp = Instantiate (drone, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//Stats: Time (in seconds) for from below, and target position it goes to
		m.SetFromBackground (2,  new Vector3 (0,0,50));
		//After it comes up, it will move linearly, so x and y directions are important
		m.SetGeneral(200,-1,-1,40,50);

		/*temp = Instantiate (drone, new Vector3 (800, 0, 1000), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//curious to see what coming from above looks like
		m.SetFromBackground (100, new Vector3(200,0,0));
		m.SetGeneral(200,-1,-1,40,50);
		*/

		yield return new WaitForSeconds (5);

		/*Debug.Log ("Change");
		//EXPERIMENTAL: changing behaviors after a set amount to time
		temp = Instantiate (drone, new Vector3 (-800, 0, 0), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		//linear to sine
		m.SetLinear();
		m.SetSin (100, 20);
		m.SetWaitTime (5, new int[1] {1});
		m.SetGeneral(200,1,0,40,50);
		*/

		yield break;
	}

	public void StartTemplate(){
		StartCoroutine ("Template");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	IEnumerator TestGround(){
		//initialize stuff
		GameObject temp;
		EnemyMovement em;

		temp = MakeWorm (6, new Vector3 (rightScreen, 0, 50),270);
		em = temp.GetComponent<EnemyMovement> ();
		em.followNose = true;
		em.hasDest = false;
		em.hasRange = true;
		em.rotInc = .5f;
		em.rotRange = 40;
		em.dirY = 1;

		yield break;
	}

	public void StartTest(){
		StartCoroutine ("TestGround");
	}

	//=====================================================================================================================================================
	//																UNIVERSAL FUNCTIONS
	//=====================================================================================================================================================

	//Everything should be fairly self-explanatory;
	//Spacing is the time between spawns
	//xInc and yInc is the physical space between enmies

	IEnumerator SpawnLinear(GameObject enemy, int numEnemies, float spacing, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX+xInc*i,startY+yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetLinear ();
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnLinear(GameObject enemy, int numEnemies, float spacing, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnLinear (enemy, numEnemies, spacing, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnSin(GameObject enemy, int numEnemies, float spacing, float amplitude, float period, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX+xInc*i,startY+yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetSin (amplitude, period);
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnSin(GameObject enemy, int numEnemies, float spacing, float amplitude, float period, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnSin (enemy, numEnemies, spacing, amplitude, period, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnOsc(GameObject enemy, int numEnemies, float spacing, float oscSpeed, float bounds, bool vertical, bool posDir, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX + xInc*i,startY + yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetLinear ();
			m.SetOsc(oscSpeed,bounds,vertical,posDir);
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnOsc(GameObject enemy, int numEnemies, float spacing, float oscSpeed, float bounds, bool vertical, bool posDir, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnOsc (enemy, numEnemies, spacing, oscSpeed, bounds, vertical, posDir, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnFollow(GameObject enemy, int numEnemies, float spacing, float rotInc, float rotRange, float speed, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX + xInc*i,startY + yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetFollow (rotInc, rotRange);
			m.SetGeneral (speed, 0, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnFollow(GameObject enemy, int numEnemies, float spacing, float rotInc, float rotRange, float speed, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnFollow (enemy, numEnemies, spacing, rotInc, rotRange, speed, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnDive(GameObject enemy, int numEnemies, float spacing, float diveSpeed, float diveTime, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX+yInc*i,startY+yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetDiveAtPlayer (diveSpeed, diveTime);
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnDive(GameObject enemy, int numEnemies, float spacing, float diveSpeed, float diveTime, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnDive (enemy, numEnemies, spacing, diveSpeed, diveTime, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnTopToSide(GameObject enemy, int numEnemies, float spacing, float changeTime, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX+xInc*i,startY+yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetTopToSide (changeTime);
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnTopToSide(GameObject enemy, int numEnemies, float spacing, float changeTime, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnTopToSide (enemy, numEnemies, spacing, changeTime, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnSideToBottom(GameObject enemy, int numEnemies, float spacing, float changeTime, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX + xInc*i,startY + yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetSideToBottom(changeTime);
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnSideToBottom(GameObject enemy, int numEnemies, float spacing, float changeTime, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnSideToBottom (enemy, numEnemies, spacing, changeTime, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	IEnumerator SpawnFromBackground(GameObject enemy, int numEnemies, float spacing, float upTime, Vector3 destination, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = Instantiate(enemy, new Vector3(startX + xInc*i,startY + yInc*i,50), rotation) as GameObject;
			Movement m = temp.GetComponent<Movement> ();
			m.SetFromBackground (upTime, destination);
			m.SetGeneral (speed, xDir, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnFromBackground(GameObject enemy, int numEnemies, float spacing, float upTime, Vector3 destination, float speed, float xDir, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnFromBackground (enemy, numEnemies, spacing, upTime, destination, speed, xDir, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	//maybe do one for worms?
	//Might add a vaiable for spacing the enemies out along x or y axes
	//By default, this is going to use Movement.SetFollow();

	IEnumerator SpawnWorms(int numEnemies, int numSegments, float spacing, float rotInc, float rotRange, float speed, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = MakeWorm (numSegments, new Vector3 (startX + xInc*i, startY + yInc*i, 50), rotation.eulerAngles.z);
			Movement m = temp.GetComponent<Movement> ();
			m.SetFollow (rotInc, rotRange);
			m.SetGeneral (speed, 0, yDir, 40, health);

			yield return new WaitForSeconds(spacing);
		}
		yield break;
	}

	public void StartSpawnWorms(int numEnemies, int numSegments, float spacing, float rotInc, float rotRange, float speed, float yDir, float health, float startX, float startY, Quaternion rotation, float xInc, float yInc){
		StartCoroutine (SpawnWorms (numEnemies, numSegments, spacing, rotInc, rotRange, speed, yDir, health, startX, startY, rotation, xInc, yInc));
	}

	//=====================================================================================================================================================
	//																	CITY 2
	//=====================================================================================================================================================

	//Starting City 2; new function style.
	//One worm from each side
	//Not quite sure how to make worms work witht he new functions, though...

	//This one can actually just be a funtion;
	public void StartDoubleWorms(){
		GameObject temp;
		Movement m;

		temp = MakeWorm (6, new Vector3 (rightScreen, 0, 5), 270);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.2f, 20);
		m.screenDeath = false;
		//m.doesTilt = false;

		temp = MakeWorm (6, new Vector3 (leftScreen, 0, 5), 90);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.2f, 20);
		m.screenDeath = false;
		//m.doesTilt = false;

	}

	//Big W formation (may be more of an m);

	IEnumerator BigW(GameObject enemy){
		GameObject temp;
		Movement m;

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (500, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (-500, 400, 50));

		yield return new WaitForSeconds (1.5f);

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (600, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (350, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (-350, 400, 0));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (-600, 400, 50));

		yield return new WaitForSeconds (1.5f);

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (700, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (200, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (-200, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (-700, 400, 50));

		yield return new WaitForSeconds (1.5f);


		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (800, 400, 50));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (-800, 400, 0));

		temp = Instantiate (enemy, new Vector3 (0, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 50);
		m.SetFromBackground (2f, new Vector3 (0, 400, 50));

		yield break;

	}

	public void StartBigW(GameObject enemy){
		StartCoroutine (BigW (enemy));
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------


	//Left-Right alternating (6)
	IEnumerator LR6Alternate(GameObject enemy){
		GameObject temp;
		Movement m;

		temp = Instantiate (enemy, new Vector3 (leftScreen, 600, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (500, 1, 0, 30, 20);
		m.SetLinear ();

		yield return new WaitForSeconds (2);

		temp = Instantiate (enemy, new Vector3 (rightScreen, 400, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (500, -1, 0, 30, 20);
		m.SetLinear ();

		yield return new WaitForSeconds (2);

		temp = Instantiate (enemy, new Vector3 (leftScreen, 100, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (500, 1, 0, 30, 20);
		m.SetLinear ();

		yield return new WaitForSeconds (2);

		temp = Instantiate (enemy, new Vector3 (rightScreen, -100, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (500, -1, 0, 30, 20);
		m.SetLinear ();

		yield return new WaitForSeconds (2);

		temp = Instantiate (enemy, new Vector3 (leftScreen, -500, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (500, 1, 0, 30, 20);
		m.SetLinear ();

		yield return new WaitForSeconds (2);

		temp = Instantiate (enemy, new Vector3 (rightScreen, -600, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (500, -1, 0, 30, 20);
		m.SetLinear ();

		yield break;
	}

	public void StartLR6Alternate(GameObject enemy){
		StartCoroutine (LR6Alternate (enemy));
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	public void FiveWormsTop(){
		GameObject temp;
		Movement m;

		temp = MakeWorm (6, new Vector3 (-800, upScreen, 50), 0);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;
		//m.doesTilt = false;

		temp = MakeWorm (6, new Vector3 (-400, upScreen, 50), 0);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;
		//m.doesTilt = false;

		temp = MakeWorm (6, new Vector3 (0, upScreen, 50), 0);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;
		//m.doesTilt = false;

		temp = MakeWorm (6, new Vector3 (400, upScreen, 50),0);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;
		//m.doesTilt = false;

		temp = MakeWorm (6, new Vector3 (800, upScreen, 50), 0);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;
		//m.doesTilt = false;

	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//4 from below
	IEnumerator FourFromBelow(GameObject enemy){
		GameObject temp;
		Movement m;

		temp = Instantiate (enemy, new Vector3 (-800, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 5);

		temp = Instantiate (enemy, new Vector3 (-400, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 7);

		temp = Instantiate (enemy, new Vector3 (400, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 7);

		temp = Instantiate (enemy, new Vector3 (800, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 5);

		yield return new WaitForSeconds (3);

		temp = Instantiate (enemy, new Vector3 (-800, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 7);

		temp = Instantiate (enemy, new Vector3 (-400, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 5);

		temp = Instantiate (enemy, new Vector3 (400, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 5);

		temp = Instantiate (enemy, new Vector3 (800, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetDiveAtPlayer (400, 7);

		yield break;
	}

	public void StartFourFromBelow(GameObject enemy){
		StartCoroutine (FourFromBelow(enemy));
	}


	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	public void FiveFromBeloW(GameObject enemy){
		GameObject temp;
		Movement m;

		temp = Instantiate (enemy, new Vector3 (-600, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (-300, downScreen-200, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (0, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (300, downScreen-200, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (600, downScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 30, 40);
		m.SetLinear ();

	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	IEnumerator FlanktasticFour(GameObject enemy){
		GameObject temp;
		Movement m;

		int i = 0;

		while (i < 4) {
			temp = Instantiate (enemy, new Vector3 (-600, downScreen, 50), Quaternion.identity) as GameObject;
			m = temp.GetComponent<Movement> ();
			m.SetGeneral (300, 0, 1, 30, 40);
			m.SetLinear ();

			temp = Instantiate (enemy, new Vector3 (600, downScreen, 50), Quaternion.identity) as GameObject;
			m = temp.GetComponent<Movement> ();
			m.SetGeneral (300, 0, 1, 30, 40);
			m.SetLinear ();

			i++;

			yield return new WaitForSeconds (1);
		}

		yield break;
	}

	public void StartFlanktasticFour(GameObject enemy){
		StartCoroutine (FlanktasticFour(enemy));
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	public void TwoWormsBelow(){
		GameObject temp;
		Movement m;

		temp = MakeWorm (4, new Vector3 (-150, downScreen, 50), 180);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;

		temp = MakeWorm (4, new Vector3 (150, downScreen, 50), 180);
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (200, 0, 1, 45, 100);
		m.SetFollow (.5f, 50);
		m.screenDeath = false;

	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	public void UpLeftRight(GameObject enemy){
		GameObject temp;
		Movement m;

		temp = Instantiate (enemy, new Vector3 (leftScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (400, 1, 0, 30, 30);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (0, upScreen, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (400, 0, -1, 30, 30);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (rightScreen, 0, 50), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (400, -1, 0, 30, 30);
		m.SetLinear ();
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	public void TopW(GameObject enemy){
		GameObject temp;
		Movement m;

		temp = Instantiate (enemy, new Vector3 (-600, upScreen + 200, 0), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (-300, upScreen, 0), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (0, upScreen + 200, 0), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (300, upScreen, 0), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 40);
		m.SetLinear ();

		temp = Instantiate (enemy, new Vector3 (600, upScreen + 200, 0), Quaternion.identity) as GameObject;
		m = temp.GetComponent<Movement> ();
		m.SetGeneral (300, 0, -1, 30, 40);
		m.SetLinear ();
	}

	//=====================================================================================================================================================
	//																	CITY 1
	//=====================================================================================================================================================
	/*
	//Space invaders-style wave 
	IEnumerator WaveOne(){

		GameObject temp;
		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;

		WaveManager wm = wmTemp.GetComponent<WaveManager> ();
		EnemyMovement em;


		//Set up wavemanager
		wm.hasDest = true;
		wm.enemies = new List<GameObject>();
		wm.waveSet = false;

		//Start spawns
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start1();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 150, 0);
		em.wm = wm;
		em.flightTime = 2;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-250, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start1();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		em.flightTime = 2;

		temp = Instantiate (drone, new Vector3 (250, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start1();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 250, 0);
		em.wm = wm;
		em.flightTime = 2;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start1();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		em.flightTime = 2;

		temp = Instantiate (drone, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start1();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = true;
		em.dest = new Vector3 (temp.transform.position.x, 350, 0);
		em.wm = wm;
		em.flightTime = 2;

		yield break;
	}

	public void StartOne(){
		StartCoroutine ("WaveOne");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//Left side sloping wave that travels down while moving left and right
	IEnumerator WaveTwo(){

		GameObject temp;
		EnemyMovement em;

		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();

		wm.hasDest = false;
		wm.right = true;

		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.wm = wm;
		//temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-450, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (-150, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;
		wm.waveSet = true;

		yield break;

	}

	public void StartTwo(){
		StartCoroutine ("WaveTwo");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//Right side sloping wave that travels down while moving left and right
	IEnumerator WaveTwoPointFive(){

		GameObject temp;
		EnemyMovement em;

		GameObject wmTemp = Instantiate (waveManager, transform.position, Quaternion.identity) as GameObject;
		WaveManager wm = wmTemp.GetComponent<WaveManager> ();

		wm.hasDest = false;
		wm.right = false;

		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (450, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (150, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;

		yield return new WaitForSeconds(.5f);

		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        wm.enemies.Add(temp);
		em = temp.GetComponent<EnemyMovement> ();
		em.wm = wm;
		em.hasDest = false;
		//temp.transform.eulerAngles = new Vector3(0f,0f,-90f);
		em.vert = true;
		em.dirY = -1;
		em.oscillate = true;
		wm.waveSet = true;

		yield break;
	}

	public void StartTwoPointFive(){
		StartCoroutine ("WaveTwoPointFive");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//3 turrets travel down screen
	IEnumerator TurretWaveOne(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret1();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (turret, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret1();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret1();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        yield break;
	}

	public void StartTurretWaveOne(){
		StartCoroutine ("TurretWaveOne");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//Double sine wave from each side. Right now they collide with eachother; should I fix this?
	IEnumerator DroneSineWave(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (1000, 200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1000, -200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		temp = Instantiate (drone, new Vector3 (1200, 200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1200, -200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		yield return new WaitForSeconds(3f);
		temp = Instantiate (drone, new Vector3 (1000, 200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1000, -200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		temp = Instantiate (drone, new Vector3 (1200, 200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1200, -200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		yield return new WaitForSeconds(3f);
		temp = Instantiate (drone, new Vector3 (1000, 200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1000, -200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		temp = Instantiate (drone, new Vector3 (1200, 200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = -1f;
		temp = Instantiate (drone, new Vector3 (-1200, -200, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start4();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.sine = true;
		em.dirX = 1f;
		yield break;
	}
	public void StartDroneSineWave(){
		StartCoroutine ("DroneSineWave");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//Alternating from left and right top corners to right and left bottom corners
	IEnumerator DroneCrossWave(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = 1.5f;
		em.dirY = -1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start5();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.hori = true;
		em.dirX = -1.5f;
		em.dirY = -1f;
		yield break;
	}
	public void StartDroneCrossWave(){
		StartCoroutine ("DroneCrossWave");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//Spawns drones in a semicircle at the top of the map0
	IEnumerator DronesInnerCircle(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = .8f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (50, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,30))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-50, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-30))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (100, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-100, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-45))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (150, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,60))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		temp = Instantiate (drone, new Vector3 (-150, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-60))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1;
		yield break;
	}
	public void StartDronesInnerCircle(){
		StartCoroutine ("DronesInnerCircle");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//spawns drones in quarter circles on the upper left and right corners
	IEnumerator DronesCornerCircles(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-60))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,60))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-45))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		yield return new WaitForSeconds(.5f);
		temp = Instantiate (drone, new Vector3 (1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,-30))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		temp = Instantiate (drone, new Vector3 (-1000, transform.position.y, 0), Quaternion.Euler(new Vector3(0,0,30))) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start6();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.rotInc = 0;
		em.dirY = 1f;
		yield break;
	}
	public void StartDronesCornerCircles(){
		StartCoroutine ("DronesCornerCircles");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//spawns turrets every 2 seconds on alternating sides of the screen
	IEnumerator AlternateTurrets(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret2();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret2();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret2();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret2();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret2();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield return new WaitForSeconds(2f);
		temp = Instantiate (turret, new Vector3 (-500, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret2();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -.5f;
		yield break;
	}

	public void StartAlternateTurrets(){
		StartCoroutine ("AlternateTurrets");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//Two collumns of turrets surrounded by drones
	IEnumerator DronesAndTurrets(){
		GameObject temp;
		EnemyMovement em;
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield return new WaitForSeconds(1.5f);
		temp = Instantiate (drone, new Vector3 (-600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = -.05f;
		temp = Instantiate (turret, new Vector3 (300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
        em.dirY = -0.8f;
        temp = Instantiate (drone, new Vector3 (0, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -1f;
		temp = Instantiate (turret, new Vector3 (-300, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().StartTurret3();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.vert = true;
		em.dirY = -0.8f;
		temp = Instantiate (drone, new Vector3 (600, transform.position.y, 0), Quaternion.identity) as GameObject;
        temp.AddComponent<Drones_1_1>();
        temp.GetComponent<Drones_1_1>().Start23();
        em = temp.GetComponent<EnemyMovement> ();
		em.hasDest = false;
		em.followNose = true;
		em.hasRange = false;
		em.dirY = 1f;
		em.rotInc = .05f;
		yield break;
	}

	public void StartDronesAndTurrets(){
		StartCoroutine ("DronesAndTurrets");
	}

	//-----------------------------------------------------------------------------------------------------------------------------------------------------

	//City-2 START!...yah, I think I might just split these into different scripts...We're nearing 1000 lines per level
	/*IEnumerator DoubleWorms(){
		//initialize stuff
		GameObject temp;
		EnemyMovement em;

		temp = MakeWorm (6, new Vector3 (1250, 0, 0),new Vector3(0,0,270));
		em = temp.GetComponent<EnemyMovement> ();
		em.followNose = true;
		em.hasDest = false;
		em.hasRange = true;
		em.rotInc = .5f;
		em.rotRange = 40;
		em.dirY = 1;

		temp = MakeWorm (6, new Vector3 (-1250, 0, 0),new Vector3(0,0,90));
		em = temp.GetComponent<EnemyMovement> ();
		em.followNose = true;
		em.hasDest = false;
		em.hasRange = true;
		em.rotInc = .5f;
		em.rotRange = 40;
		em.dirY = 1;

		yield break;
	}

	public void StartDoubleWorms(){
		StartCoroutine ("DoubleWorms");
	}*/
}
