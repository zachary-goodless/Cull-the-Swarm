
using UnityEngine;
using System.Collections;

public class TeslaPhase_1 : MonoBehaviour
{
	//PUBLIC
	public float growDuration = 0.5f;
	public float delayBetweenTicks = 0.01f;

	public float damageTick = 5f;

	public bool isActive;

	//PRIVATE
	float storedDamage;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		isActive = true;

		storedDamage = 0;

		StartCoroutine(handleGrow());
		StartCoroutine(handleRotate());
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerEnter2D(Collider2D other)
	{
		//other is bullet...
		if(other.tag == "Bullet" && isActive)
		{
			//reset bullet, grow damage
			BulletManager.DeleteBullet(other.gameObject);
			storedDamage += damageTick;
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

	public void turnOff()
	{
		isActive = false;
		StartCoroutine(handleTurnOff());
	}
	IEnumerator handleTurnOff()
	{
		while(transform.localScale.x > 0f)
		{
			//grow the area's size
			Vector3 scale = transform.localScale;
			scale.x = scale.y -= delayBetweenTicks * 1000;
			if(scale.x < 0f)
			{
				scale.x = scale.y = 0f;
			}

			transform.localScale = scale;

			//wait for a short time
			yield return new WaitForSeconds(delayBetweenTicks);
		}

		Destroy(gameObject);
		yield break;
	}

//--------------------------------------------------------------------------------------------

	public float getStoredDamage(){ return storedDamage; }
}
