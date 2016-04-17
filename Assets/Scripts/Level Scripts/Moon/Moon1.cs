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


		yield return new WaitForSeconds (16f);

		sf.Fade ();
		finished.handleLevelCompleted((SceneIndex)SceneManager.GetActiveScene().buildIndex);
		yield break;
	}
}
