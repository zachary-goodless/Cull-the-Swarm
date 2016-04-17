using UnityEngine;
using System.Collections;

public class Splat : MonoBehaviour {

	SpriteRenderer sr;
	bool finished;
	Color color;

	// Use this for initialization
	void Start () {
		sr = GetComponentInChildren<SpriteRenderer> ();
		transform.localScale = new Vector3 (.01f, .01f, 1);
		transform.eulerAngles = new Vector3(0,0,Random.Range(0f,360f));
		color.r = Random.Range (0f, 1f);
		color.g = Random.Range (.6f, 1f);
		color.b = Random.Range (.0f, .016f);
		color.a = 1;
		sr.color = color;
		finished = false;
		Debug.Log ("alpha splat: " + sr.color.a);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < 1 || transform.localScale.y < 1) {
			transform.localScale += new Vector3 (Random.Range (.04f, .05f), Random.Range (.005f, .02f), 0);
		} else {
			finished = true;
		}
		if (finished) {
			float alphaVal = sr.color.a - .01f;
			if (alphaVal <= 0) {
				Destroy (gameObject);
			} else {
				color.a = alphaVal;
				sr.color = color;
			}
		}



	}
}
