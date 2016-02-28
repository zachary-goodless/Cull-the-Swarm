using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
	Image fader;
	public bool finished;

	void Start ()
	{
		fader = GetComponent<Image> ();
	}
	
	public void Fade()
	{
		StartCoroutine ("FadeToBlack");
	}

	public IEnumerator FadeToBlack()
	{
		gameObject.SetActive(true);
		finished = false;

		Color color = fader.color;
		bool fading = true;
		float alphaVal = 0;

		while (fading)
		{
			if (color.a < 1)
			{
				alphaVal += .05f;
			}
			else
			{
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

	public IEnumerator FadeFromBlack()
	{
		yield return new WaitForSeconds(0.5f);
		finished = false;

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
				finished = true;
			}

			color.a = alphaVal;
			fader.color = color;

			yield return new WaitForSeconds(0.00001f);
		}

		gameObject.SetActive(false);	//disable to allow menu interaction
		yield return null;
	}
}
