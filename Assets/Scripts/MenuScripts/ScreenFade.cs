using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour {

	Image fader;
	//Or Public string toLevel?

	// Use this for initialization
	void Start () {
		fader = GetComponent<Image> ();
		// StartCoroutine ("FadeToBlack");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public IEnumerator FadeToBlack(){

		gameObject.SetActive(true);

		Color color = fader.color;
		bool fading = true;
		float alphaVal = 0;

		while (fading) {
			if (color.a < 1) {
				alphaVal += .05f;
			} else {
				alphaVal = 1;
				fading = false;
			}

			color.a = alphaVal;
			fader.color = color;
			yield return new WaitForSeconds (.00001f);
		}
		yield return null;
	}

	public IEnumerator FadeFromBlack()
	{
		yield return new WaitForSeconds(0.5f);

		Color color = fader.color;
		float alphaVal = 1;

		bool fading = true;
		while(fading)
		{
			if(color.a > 0)
			{
				alphaVal -= 0.05f;
			}
			else
			{
				alphaVal = 0;
				fading = false;
			}

			color.a = alphaVal;
			fader.color = color;

			yield return new WaitForSeconds(0.00001f);
		}

		gameObject.SetActive(false);	//disable to allow menu interaction
		yield return null;
	}
}
