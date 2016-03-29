
using UnityEngine;
using System.Collections;

public class FreezeBullet : MonoBehaviour
{
	//PUBLIC
	public float duration = 3f;

	//PRIVATE
	int recursionLevel = 0;

	GameObject freezeBulletPrefab;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		freezeBulletPrefab = Resources.Load<GameObject>("PlayerBullets/FreezeBullet");

		StartCoroutine(handleDuration());
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerEnter2D(Collider2D other)
	{
		//if other is bullet and we're not at the bottom recursion level...
		if(other.tag == "Bullet" && recursionLevel != 0)
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
				freezeBulletScript.setRecursionLevel(recursionLevel - 1);
			}
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleDuration()
	{
		yield return new WaitForSeconds(duration);

		//TODO -- explode?

		Destroy(gameObject);
		yield break;
	}

//--------------------------------------------------------------------------------------------

	public void setRecursionLevel(int d){ recursionLevel = d; }
}
