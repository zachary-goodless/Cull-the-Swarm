
using UnityEngine;
using System.Collections;

public class TeslaPhase_1 : MonoBehaviour
{
	//PUBLIC
	public float growDuration = 0.5f;
	public float delayBetweenTicks = 0.01f;

	//PRIVATE
	float storedDamage;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		storedDamage = 0;

		StartCoroutine(handleGrow());
		StartCoroutine(handleRotate());
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerEnter2D(Collider2D other)
	{
		OnTriggerStay2D(other);
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Bullet")
		{
			//TODO -- other is enemy bullet -- reset bullet, store damage
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleGrow()
	{
		float remainingTime = growDuration;

		while(remainingTime > 0f)
		{
			//grow the area's size
			Vector3 scale = transform.localScale;
			scale.x = scale.y += delayBetweenTicks * 750;
			transform.localScale = scale;

			//wait for a short time
			yield return new WaitForSeconds(delayBetweenTicks);

			//reduce time remaining
			remainingTime -=delayBetweenTicks;
		}

		yield break;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleRotate()
	{
		while(true)
		{
			transform.RotateAround(transform.position, Vector3.forward, -5f);
			yield return new WaitForSeconds(delayBetweenTicks);
		}
	}

//--------------------------------------------------------------------------------------------

	public float getStoredDamage(){ return storedDamage; }
}
