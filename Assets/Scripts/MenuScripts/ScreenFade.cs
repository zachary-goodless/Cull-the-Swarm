using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour {

	Image fader;
	public bool finished;
	//Or Public string toLevel?

	// Use this for initialization
	void Start () {
		fader = GetComponent<Image> ();
		StartCoroutine ("FadeToBlack");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator FadeToBlack(){
		
		Color color = fader.color;
		bool fading = true;
		float alphaVal = 0;

		while (fading) {
			if (color.a < 1) {
				alphaVal += .01f;
			} else {
				alphaVal = 1;
				fading = false;
				finished = true;
			}

			color.a = alphaVal;
			fader.color = color;
			yield return new WaitForSeconds (.00001f);
		}
		yield return null;
	}
}
