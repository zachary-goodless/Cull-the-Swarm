
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundsHandler : MonoBehaviour, ISelectHandler
{
	//PUBLIC
	public AudioSource[] audio;

	//PRIVATE
	//

//--------------------------------------------------------------------------------------------

	void Start()
	{
		audio = GetComponents<AudioSource>();
	}

//--------------------------------------------------------------------------------------------

	public void OnSelect(BaseEventData eventData)
	{
		if(audio.Length == 0) return;
		audio[0].Play();
	}

//--------------------------------------------------------------------------------------------

	public void OnClick()
	{
		if(audio.Length == 0) return;
		audio[1].Play();
	}
}
