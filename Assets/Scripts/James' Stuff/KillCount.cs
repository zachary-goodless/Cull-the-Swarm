using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class KillCount : MonoBehaviour {

	public int score;
	public int health;
	private Text text;

	// Use this for initialization
	void Start () {
		score = 0;
		health = 5;
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Killcount:\n" + score + "/20\nHealth:\n" + health + "/5";
		/*if (score >= 20) {
			SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
		}*/
	}
}
