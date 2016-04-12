
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public enum Characters
{
	NULL = 		-1,

	ROGER =		0,
	COLONEL = 	1,
	STAMPER = 	2,
	MARTHA = 	4,
	DOUCHE =	5
}

public class DialogueBox : MonoBehaviour
{
	//PUBLIC
	public Image mSpeakerImg;
	public Text mSpeakerName;
	public Text mDialogue;

	//PRIVATE
	float timeBetweenTicks = 0.01f;

	private Sprite[] speakerSprites = null;
	private string[] speakerNames = new string[5]
	{
		"ROGER",
		"COLONEL CUNNINGHAM",
		"STAMPER",
		"MARTHA",
		"CHAD"
	};

//--------------------------------------------------------------------------------------------

	public IEnumerator handleDialogue(float duration, Characters character, string content)
	{
		//load the speaker sprites if we haven't already
		if(speakerSprites == null)
		{
			//TODO -- init speaker sprites
		}

		if(character != Characters.NULL)
		{
			//mSpeakerImg.sprite = speakerSprites[(int)character];		//TODO -- temp comment out
			mSpeakerName.text = speakerNames[(int)character];
		}

		mDialogue.text = "";
		gameObject.SetActive(true);

		//for the length of the content string...
		for(int i = 0; i < content.Length; ++i)
		{
			//add the content string to the dialogue text one character at a time
			mDialogue.text += content.Substring(i, 1);

			//update remaining duration
			duration -= timeBetweenTicks;
			yield return new WaitForSeconds(timeBetweenTicks);
		}

		//show the dialogue for the remaining duration
		yield return new WaitForSeconds(duration);

		gameObject.SetActive(false);

		yield return null;
	}
}
