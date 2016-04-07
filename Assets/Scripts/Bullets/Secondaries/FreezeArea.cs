
using UnityEngine;
using System.Collections;

public class FreezeArea : MonoBehaviour
{
	//PUBLIC
	public float spinupDuration = 1.25f;
	public float growDuration = 0.25f;

	public float delayBetweenTicks = 0.01f;

	public int maxRecursion = 5;

	//PRIVATE
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
		//if other is bullet...
		if(other.tag == "Bullet")
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
