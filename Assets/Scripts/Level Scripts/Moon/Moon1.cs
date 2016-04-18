using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Moon1 : MonoBehaviour {

	public Waves waves;
	public ScreenFade sf;

	public LevelCompleteHandler finished;

	public GameObject drone;
	public GameObject missile;
	public GameObject turret;
	public GameObject snail;

	void Start () {
		StartCoroutine ("LevelLayout");
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
	}

	IEnumerator LevelLayout(){

		yield return new WaitForSeconds (5f);

		//Wave One - 3 turrets evenly spaced along the top of the screen move slowly towards the bottom.
		waves.StartSpawnLinear(turret, 3, 0, 150, 0, -1, 150, -400,waves.upScreen,Quaternion.identity,400,0,"");
		yield return new WaitForSeconds (2f); 

		//Wave Two (2 sec afterwards)- 4 drones move linearly from the left side in between the two turrets towards the bottom of the screen.
		waves.StartSpawnLinear(drone, 4, 0, 250, 0, -1, 100, -600,waves.upScreen,Quaternion.identity, 150,0,"");
		yield return new WaitForSeconds (4f);

		//Wave Three (4 sec afterwards)- 4 drones move linearly from the right side in between the two turrets towards the bottom of the screen.
		waves.StartSpawnLinear(drone, 4, 0, 250, 0, -1, 100, 600,waves.upScreen,Quaternion.identity, -150,0,"");
		yield return new WaitForSeconds (4f);

		//Wave Five (4 secs afterwards)- 5 snails move linearly along the top of the screen from left to right
		waves.StartSpawnLinear(snail,5,1.5f,150,1,0,120,waves.leftScreen,400,Quaternion.Euler(0,0,90),0,0,"");
		yield return new WaitForSeconds (6f);

		//Wave Six (6 sec afterwards)- one worm moves along the bottom of the screen from right to left.
		waves.StartSpawnWorms(1,8,0,.5f,25,250,1,280,waves.leftScreen,-400,Quaternion.Euler(0,0,90),0,0,"");
		yield return new WaitForSeconds (6f);

		//Wave Six (6 sec afterwards)- one worm moves along the bottom of the screen slightly above the other one from left to right.
		waves.StartSpawnWorms(1,8,0,.5f,25,250,1,280,waves.leftScreen,-250,Quaternion.Euler(0,0,270),0,0,"");
		yield return new WaitForSeconds (7f);

		//Wave Seven ( 7 sec afterwards) - 5 turrets move along the left side of the screen moving slowly towards the bottom.
		waves.StartSpawnLinear(turret,5,1.5f,150,0,-1,150,-400,waves.upScreen,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (7f);

		//Wave Seven (7 sec afterwards)- 5 turrets move along the right side of the screen moving slowly towards the bottom.
		waves.StartSpawnLinear(turret,5,1.5f,150,0,-1,150, 400,waves.upScreen,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (5f);

		//Wave Eight (5 sec afterwards)- 2 drones from background from left top side of the screen. Move towards the bottom right side of the screen.
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(-600,400,50),250,.8f,-.8f,100,-600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Nine (3 sec afterwards)- 2 drones from background from right top side of the screen. Move towards the bottom left side of the screen.
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(600,-400,50),250,-.8f,-.8f,100,600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Ten (5 sec afterwards)- 2 drones from background from left top side of the screen. Move towards the bottom right side of the screen.
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(-600,400,50),250,.8f,-.8f,100,-600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Nine (3 sec afterwards)- 2 drones from background from right top side of the screen. Move towards the bottom left side of the screen.
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(600,-400,50),250,-.8f,-.8f,100,600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Snails rise from below, then dive at player
		waves.StartSpawnDive(snail,5,1.5f,200,6,150,0,1,120,0,waves.downScreen,Quaternion.Euler(0,0,180),0,0,"");
		yield return new WaitForSeconds (5f);

		//Wave Ten (3 sec afterwards)- 2 drones from background from left top side of the screen. Move towards the bottom right side of the screen.


		yield return new WaitForSeconds (16f);

		sf.Fade ();
		finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
		yield break;
	}
}
