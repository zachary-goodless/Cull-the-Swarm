using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Level1_1 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;

	public GameObject drone;
	public GameObject missile;
	public GameObject turret;

	void Start () {
		StartCoroutine ("LevelLayout");
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			waves.StartTest ();
		}
		if (Input.GetKeyDown (KeyCode.Y)) {
			waves.StartTemplate ();
		}
		if (sf.finished) {
			SceneManager.LoadScene ((int)SceneIndex.WORLD_MAP);
		}
	}

	IEnumerator LevelLayout(){
		yield return new WaitForSeconds (5f);
		//waves.StartOne ();
		waves.StartSpawnOsc(drone,2,0,300,200,false,true,200,0,-1,50,-250,waves.upScreen,Quaternion.identity,500,0,"d1");
		waves.StartSpawnOsc(drone,3,0,300,200,false,true,200,0,-1,50,-500,waves.upScreen+150,Quaternion.identity,500,0,"d1");
		yield return new WaitForSeconds (13f);
		//waves.StartTwo();
		waves.StartSpawnOsc(drone,5,0,300,200,false,true,200,0,-1,50,-600,waves.upScreen+150,Quaternion.identity,150,150,"d23");
		yield return new WaitForSeconds (5f);
		//waves.StartTwoPointFive();
		waves.StartSpawnOsc(drone,5,0,300,200,false,true,200,0,-1,50,600,waves.upScreen+150,Quaternion.identity,-150,150,"d23");
		yield return new WaitForSeconds (5f);
		//waves.StartTwo();
		waves.StartSpawnOsc(drone,5,0,300,200,false,true,200,0,-1,50,-600,waves.upScreen+150,Quaternion.identity,150,150,"d23");
		yield return new WaitForSeconds (5f);
		//waves.StartTwoPointFive();
		waves.StartSpawnOsc(drone,5,0,300,200,false,true,200,0,-1,50,600,waves.upScreen+150,Quaternion.identity,-150,150,"d23");
		yield return new WaitForSeconds (5f);
        //waves.StartOne();
		waves.StartSpawnOsc(drone,2,0,300,200,false,true,200,0,-1,50,-250,waves.upScreen,Quaternion.identity,500,0,"d1");
		waves.StartSpawnOsc(drone,3,0,300,200,false,true,200,0,-1,50,-500,waves.upScreen+150,Quaternion.identity,500,0,"d1");
        yield return new WaitForSeconds(8f);
        //waves.StartTurretWaveOne ();
		waves.StartSpawnLinear(turret,3,0,160,0,-1,60,-500,waves.upScreen,Quaternion.identity,500,0,"t1");
		yield return new WaitForSeconds (4f);
		//waves.StartDroneSineWave ();
		waves.StartSpawnSin(drone,3,3,10,1,250,1,0,50,-1000,0,Quaternion.identity,0,0,"d4");
		waves.StartSpawnSin(drone,3,3,10,1,250,-1,0,50,1000,0,Quaternion.identity,0,0,"d4");
		waves.StartSpawnSin(drone,3,3,10,1,250,-1,0,50,1200,0,Quaternion.identity,0,0,"d4");
		waves.StartSpawnSin(drone,3,3,10,1,250,1,0,50,-1200,0,Quaternion.identity,0,0,"d4");
		yield return new WaitForSeconds (15f);
		//waves.StartDroneCrossWave ();
		waves.StartSpawnLinear(drone,5,1f,160,1.5f,-1,50,-1000,waves.upScreen,Quaternion.identity,0,0,"d5");
		//waves.StartSpawnLinear(turret,5,0.5f,160,1.5,
		waves.StartSpawnLinear(drone,5,1f,160,-1.5f,-1,50,1000,waves.upScreen,Quaternion.identity,0,0,"d5");
		yield return new WaitForSeconds (10f);
		//waves.StartDronesAndTurrets ();
		waves.StartSpawnFollow (drone, 5, 1.5f, .05f, 0, 200, 1, 50, 600, waves.upScreen, Quaternion.identity, 0, 0,"d23");
		waves.StartSpawnLinear(turret,5,1.5f,160,0,-1,60,-300,waves.upScreen,Quaternion.identity,0,0,"t3");
		waves.StartSpawnLinear(drone,5,1.5f,200,0,-1,60,0,waves.upScreen,Quaternion.identity,0,0,"d23");
		waves.StartSpawnLinear(turret,5,1.5f,160,0,-1,60,300,waves.upScreen,Quaternion.identity,0,0,"t3");
		waves.StartSpawnFollow (drone, 5, 1.5f, -.05f, 0, 200, 1, 50, -600, waves.upScreen, Quaternion.identity, 0, 0,"d23");
		yield return new WaitForSeconds (10f);
		//waves.StartDronesInnerCircle ();
		waves.StartSpawnInnerCircle(drone,0,0,180,1,50,"d6");
		//waves.StartDronesCornerCircles ();
		waves.StartSpawnLCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnRCornerCircle(drone,0,0,200,1,50,"d6");
		//waves.StartAlternateTurrets ();
		waves.StartSpawnLinear(turret,3,2,180,0,-1,60,-500,waves.upScreen,Quaternion.identity,0,0,"t2");
		yield return new WaitForSeconds (2f);
		//waves.StartDronesInnerCircle ();
		waves.StartSpawnInnerCircle(drone,0,0,180,1,50,"d6");
		//waves.StartDronesCornerCircles ();
		waves.StartSpawnLCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnRCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnLinear(turret,3,2,180,0,-1,60,500,waves.upScreen,Quaternion.identity,0,0,"t2");
		yield return new WaitForSeconds (2f);
		//waves.StartDronesInnerCircle ();
		waves.StartSpawnInnerCircle(drone,0,0,180,1,50,"d6");
		//waves.StartDronesCornerCircles ();
		waves.StartSpawnLCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnRCornerCircle(drone,0,0,200,1,50,"d6");
		yield return new WaitForSeconds (4f);
		//waves.StartDronesInnerCircle ();
		waves.StartSpawnInnerCircle(drone,0,0,180,1,50,"d6");
		//waves.StartDronesCornerCircles ();
		waves.StartSpawnLCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnRCornerCircle(drone,0,0,200,1,50,"d6");
		yield return new WaitForSeconds (2f);
		//waves.StartDronesInnerCircle ();
		waves.StartSpawnInnerCircle(drone,0,0,180,1,50,"d6");
		//waves.StartDronesCornerCircles ();
		waves.StartSpawnLCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnRCornerCircle(drone,0,0,200,1,50,"d6");
		yield return new WaitForSeconds (2f);
		//waves.StartDronesInnerCircle ();
		waves.StartSpawnInnerCircle(drone,0,0,180,1,50,"d6");
		//waves.StartDronesCornerCircles ();
		waves.StartSpawnLCornerCircle(drone,0,0,200,1,50,"d6");
		waves.StartSpawnRCornerCircle(drone,0,0,200,1,50,"d6");
		yield return new WaitForSeconds (16f);
		sf.Fade ();
		yield break;
	}

	IEnumerator LevelLayout2(){
		//Timing and placement need adjustment, but here's an (incomplete) general outline of what was in the GDD
		yield return new WaitForSeconds(1f);
		waves.StartSpawnLinear (drone, 5, 1, 300, 1, -1, 50, waves.leftScreen, waves.upScreen, Quaternion.identity, 400, 0,"");
		waves.StartSpawnWorms (3, 5, 0, .5f, 30, 300, 1, 50, waves.leftScreen, 200, Quaternion.Euler (0, 0, 90), 0, -200,"");
		/*yield return new WaitForSeconds (5f);
		waves.StartDoubleWorms ();
		waves.StartBigW (drone);
		yield return new WaitForSeconds (10f);
		waves.StartLR6Alternate (missile);
		waves.FiveWormsTop ();
		yield return new WaitForSeconds (14f);
		waves.StartFourFromBelow (drone);
		yield return new WaitForSeconds (6.5f);
		waves.StartFlanktasticFour (drone);
		yield return new WaitForSeconds (1.5f);
		waves.FiveFromBeloW (turret);
		waves.TwoWormsBelow ();
		yield return new WaitForSeconds (4f);
		waves.UpLeftRight (missile);
		yield return new WaitForSeconds (8f);
		waves.TopW (drone);
		yield return new WaitForSeconds (1f);
		waves.TopW (drone);
		yield return new WaitForSeconds (1f);
		waves.TopW (drone);
		yield break;
		*/
	}

}
