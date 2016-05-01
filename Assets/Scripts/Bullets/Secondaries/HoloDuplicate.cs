
using UnityEngine;
using System.Collections;

public class HoloDuplicate : MonoBehaviour
{
	//PUBLIC
	public AudioSource onSound;
	public AudioSource offSound;

	//PRIVATE
	bool blinking = false;
	MeshRenderer[] meshList;

	Coroutine onSoundCoroutine;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		meshList =  GetComponentsInChildren<MeshRenderer>();
		onSoundCoroutine = StartCoroutine(handleOnSound());
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		Blink();
	}

//--------------------------------------------------------------------------------------------

	public void turnOff()
	{
		//stop on sound, play off sound
		StopCoroutine(onSoundCoroutine);
		offSound.Play();

		//destroy after short delay
		Destroy(gameObject, 0.1f);
	}

//--------------------------------------------------------------------------------------------

	void Blink()
	{
		blinking = true;
		foreach (MeshRenderer m in meshList)
		{
			if (m)
			{
				m.material.SetColor("_Color", new Color(0.3f, 0.3f, 1f, 1f));
			}
		}

		Invoke("Reveal", 0.1f);
	}

	void Reveal()
	{
		foreach (MeshRenderer m in meshList)
		{
			if (m)
			{
				m.material.SetColor("_Color", Color.white);
			}
		}
			
		blinking = false;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleOnSound()
	{
		while(true)
		{
			onSound.Play();
			yield return new WaitForSeconds(0.25f);
		}
	}
}
