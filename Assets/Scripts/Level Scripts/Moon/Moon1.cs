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
	public DialogueBox dialog;

	void Start () {
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
		StartCoroutine ("LevelLayout");

	}

	IEnumerator LevelLayout(){

		StartCoroutine(sf.FadeFromBlack());

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
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(600,400,50),250,-.8f,-.8f,100,600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Ten (3 sec afterwards)- 2 drones from background from left top side of the screen. Move towards the bottom right side of the screen.
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(-600,400,50),250,.8f,-.8f,100,-600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Eleven (3 sec afterwards)- 2 drones from background from right top side of the screen. Move towards the bottom left side of the screen.
		waves.StartSpawnFromBackground(drone,2,1.5f,2,new Vector3(600,400,50),250,-.8f,-.8f,100,600,-300,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Snails rise from below, then dive at player
		waves.StartSpawnDive(snail,5,1.5f,200,6,150,0,1,120,0,waves.downScreen,Quaternion.Euler(0,0,180),0,0,"");
		yield return new WaitForSeconds (7f);

		//Wave Twelve (7 sec afterwards)- 1 worm down form top
		waves.StartSpawnWorms(1,7,0,.5f,30,250,1,300,0,waves.upScreen,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Thirteen ( 3 sec later)- 2 turret column wide, 4 turrets tall move slowly from the center of the screen towards the bottom.
		waves.StartSpawnLinear(turret,4,1.5f,150,0,-1,150,-150,waves.upScreen,Quaternion.identity,0,0,"");
		waves.StartSpawnLinear(turret,4,1.5f,150,0,-1,150,150,waves.upScreen,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (5f);

		//Wave Fourteen (5 secs later)- one worm on right side of the screen moves its way down to the bottom of the screen.
		waves.StartSpawnWorms(1,7,0,-.05f,0,250,1,200,waves.rightScreen,200,Quaternion.Euler(0,0,270),0,0,"");
		waves.StartSpawnWorms(1,7,0,.05f,0,250,1,200,waves.leftScreen,-200,Quaternion.Euler(0,0,90),0,0,"");
		yield return new WaitForSeconds (4f);

		//Wave Seventeen (4 sec later)- 3 drones move from top of the screen towards the bottom left of the screen.
		waves.StartSpawnLinear(drone,3,0,250,-.5f,-1,100,-300,waves.upScreen,Quaternion.identity,300,0,"");
		yield return new WaitForSeconds (2f);

		//Wave Eighteen(2 sec later)- 3 drones move from the top of the screen towards the bottom right of the screen.
		waves.StartSpawnLinear(drone,3,0,250,.5f,-1,100,-300,waves.upScreen,Quaternion.identity,300,0,"");
		yield return new WaitForSeconds (3f);

		//Wave Nineteen (3 sec later)- 3 drones from the center of the screen and move towards the bottom of the screen.
		waves.StartSpawnLinear(drone,3,0,250,0,-1,100,-300,waves.upScreen,Quaternion.identity,300,0,"");
		yield return new WaitForSeconds (2f);

		//Wave Twenty (2 sec later)- 1 turret from left side of the screen.
		waves.StartSpawnLinear(turret,1,0,150,1,0,150,waves.leftScreen,150,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (1f);

		//Wave Twenty-One (1 sec later)- 3 drones from right side of the screen
		waves.StartSpawnLinear(drone,3,1,250,-1,0,100,waves.rightScreen,300,Quaternion.identity,0,-300,"");
		yield return new WaitForSeconds (2f);

		//Wave Twenty-Two (2 sec later)- 1 turret from right side of the screen.
		waves.StartSpawnLinear(turret,1,0,150,-1,0,150,waves.rightScreen,-150,Quaternion.identity,0,0,"");
		yield return new WaitForSeconds (1f);

		//Wave Twenty-Three (1 sec later)- 3 drones from left side of the screen. 
		waves.StartSpawnLinear(drone,3,1,250,1,0,100,waves.leftScreen,-300,Quaternion.identity,0,300,"");

		yield return new WaitForSeconds (16f);

		//JUSTIN
		StartCoroutine(sf.FadeToBlack());
		yield return new WaitForSeconds(2f);
		//JUSTIN

		finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
		yield break;
	}
}
