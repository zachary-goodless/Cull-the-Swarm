using UnityEngine;
using System.Collections;

public class Drones_1_1 : MonoBehaviour {
	//We'll need to assign this with .AddComponent in the spawns.

	public string pattern;
	bool ongoing;

	// Use this for initialization
	void Start () {
		ongoing = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!ongoing){
			StartCoroutine (pattern);
		}
	}
	
	//Sput your coroutines here.
	IEnumerator Test(){
		//not sure if you are going to keep the same pattern for the duration of each wave/give multiple patterns to things; if so we'll need to change up how we do this a bit. 
		/*bool condition;
		 * float seconds;
		 * ongoing = true;
		 * While(condition){
		 * Do Stuff!
		 * yield return new WaitForSeconds(seconds);
		 * 
		 * }
		 * ongoing = false;
		 */
		yield break;
	}
	
}
