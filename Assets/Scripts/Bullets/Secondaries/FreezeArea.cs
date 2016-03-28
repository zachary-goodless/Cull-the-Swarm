
using UnityEngine;
using System.Collections;

public class FreezeArea : MonoBehaviour
{
	//PUBLIC
	public float spinupDuration = 1.25f;
	public float growDuration = 0.25f;

	public float delayBetweenTicks = 0.01f;

	//PRIVATE
	int depthRemaining = 10;

	GameObject freezeBulletPrefab;
	Light spinupLight;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		freezeBulletPrefab = Resources.Load<GameObject>("PlayerBullets/FreezeBullet");
		//spinupLight = GetComponentInChildren<Light>();

		StartCoroutine(handleSpinup());
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
			//TODO -- other is enemy bullet -- reset bullet, spawn freeze bullet
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleSpinup()
	{
		//TODO -- add light to child object

		//handle glow spinup
		float remainingTime = spinupDuration;
		while(remainingTime > 0f)
		{
			//TODO -- handle light

			//wait for a short time
			yield return new WaitForSeconds(delayBetweenTicks);

			//reduce time remaining
			remainingTime -=delayBetweenTicks;
		}

		//handle short grow period
		remainingTime = growDuration;
		while(remainingTime > 0f)
		{
			//grow the area's size
			Vector3 scale = transform.localScale;
			scale.x = scale.y += delayBetweenTicks * 1250;
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
}
