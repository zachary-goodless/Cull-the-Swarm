
using UnityEngine;
using System.Collections;

public class FreezeArea : MonoBehaviour
{
	//PUBLIC
	public float spinupDuration = 1.25f;
	public float growDuration = 0.25f;

	public float delayBetweenTicks = 0.01f;

	public int maxRecursion = 5;

	public bool isActive = true;

	//PRIVATE
	GameObject freezeBulletPrefab;

	GameObject sprite;
	Light spinupLight;
	CircleCollider2D collider;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		freezeBulletPrefab = Resources.Load<GameObject>("PlayerBullets/FreezeBullet");

		sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
		spinupLight = GetComponent<Light>();
		collider = GetComponent<CircleCollider2D>();

		StartCoroutine(handleSpinup());
		StartCoroutine(handleRotate());
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerEnter2D(Collider2D other)
	{
		//if other is bullet...
		if(other.tag == "Bullet" && isActive)
		{
			//grab bullet position
			Vector3 spawnPos = other.transform.position;

			//delete bullet and spawn freeze bullet
			BulletManager.DeleteBullet(other.gameObject);

			GameObject freezeBulletObj = Instantiate(freezeBulletPrefab, spawnPos, Quaternion.identity) as GameObject;

			//set freezebullet recursion level
			FreezeBullet freezeBulletScript = freezeBulletObj.GetComponent<FreezeBullet>();
			if(freezeBulletScript != null)
			{
				freezeBulletScript.setRecursionLevel(maxRecursion - 1);
			}
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleSpinup()
	{
		//handle glow spinup
		float remainingTime = spinupDuration;
		while(remainingTime > 0f)
		{
			spinupLight.range++;

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
			Vector3 scale = sprite.transform.localScale;
			scale.x = scale.y += delayBetweenTicks * 7.5f;
			sprite.transform.localScale = scale;

			collider.radius += delayBetweenTicks * 900f;

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
		while(sprite.transform.localScale.x > 0f)
		{
			//grow the area's size
			Vector3 scale = sprite.transform.localScale;
			scale.x = scale.y -= delayBetweenTicks * 12.5f;
			if(scale.x < 0f)
			{
				scale.x = scale.y = 0f;
			}

			sprite.transform.localScale = scale;

			//reduce light
			spinupLight.range -= spinupLight.range == 0 ? 0 : 1;

			//wait for a short time
			yield return new WaitForSeconds(delayBetweenTicks);
		}

		Destroy(gameObject);
		yield break;
	}
}
