﻿
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public enum Characters
{
	NULL = 		-1,

	ROGER =		0,
	COLONEL = 	1,
	STAMPER = 	2,
	MARTHA = 	3,
	DOUCHE =	4
}

public class DialogueBox : MonoBehaviour
{
	//PUBLIC
	public Image mSpeakerImg;
	public Text mSpeakerName;
	public Text mDialogue;

	//PRIVATE
	float timeBetweenTicks = 0.01f;

	bool isActive = false;
	bool isSkipping = false;

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
		isActive = true;

		//for the length of the content string...
		for(int i = 0; i < content.Length; ++i)
		{
			//if skip key pressed...
			if(Input.GetKeyDown(KeyCode.Tab))
			{
				//add all dialogue and exit early
				mDialogue.text = content;
				isSkipping = true;
				break;
			}

			//add the content string to the dialogue text one character at a time
			mDialogue.text += content.Substring(i, 1);

			//update remaining duration
			duration -= Time.deltaTime;
			yield return new WaitForSeconds(timeBetweenTicks);
		}

		isActive = false;

		if(!isSkipping)
		{
			yield return new WaitForSeconds(duration);
			gameObject.SetActive(false);
		}
			
		isSkipping = false;
		yield break;
	}

//--------------------------------------------------------------------------------------------

	public float waitTime;
	public IEnumerator WaitForSecondsOrSkip(float duration, Coroutine co)
	{
		//this handles skippable waits (such as between dialogue)
		waitTime = duration;
		while(waitTime > 0f)
		{
			//if the skip button is pressed and the dialogue box is not filling in content...
			if(Input.GetKeyDown(KeyCode.Tab) && !isActive)
			{
				//set object to inactive, break, and stop the previous coroutine
				waitTime = 0f;
				StopCoroutine(co);
			}

			yield return new WaitForSeconds(Time.deltaTime);
			waitTime -= Time.deltaTime;
		}

		gameObject.SetActive(false);
		yield break;
	}
}
