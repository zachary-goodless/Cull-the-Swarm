using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour {

	Image fader;
	float alphaVal;
	public bool finished;
	public bool fading;
	//Or Public string toLevel?

	// Use this for initialization
	void Start () {
		fader = GetComponent<Image> ();
		alphaVal = 0;
		fading = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (fading) {
		
			Color color = fader.color;

			if (color.a < 1) {
				alphaVal += .01f;
			} else {
				alphaVal = 1;
				finished = true;
			}

			color.a = alphaVal;
			fader.color = color;
		}

	}

	public void Fade(){
		fading = true;
	}
}
