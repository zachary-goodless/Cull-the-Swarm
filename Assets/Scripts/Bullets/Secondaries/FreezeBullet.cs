
using UnityEngine;
using System.Collections;

public class FreezeBullet : MonoBehaviour
{
	//PUBLIC
	public float duration = 3f;
	public float explosionDuration = 0.25f;

	public float damage = 50f;

	//PRIVATE
	int recursionLevel = 0;

	bool isExploding = false;

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

		OnTriggerStay2D(other);
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerStay2D(Collider2D other)
	{
		//if explosion is active and other is enemy...
		if(isExploding && other.tag == "EnemyHit")
		{
			other.gameObject.GetComponentInParent<EnemyMovement>().health -= damage;
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleDuration()
	{
		//exist for duration, then explode for damage
		yield return new WaitForSeconds(duration);

		StartCoroutine(handleExplosion());
		yield break;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleExplosion()
	{
		//set state, grow circle collider
		isExploding = false;
		GetComponent<CircleCollider2D>().radius *= 3;

		yield return new WaitForSeconds(explosionDuration);

		//destroy at end of explosion duration
		Destroy(gameObject);

		yield break;
	}

//--------------------------------------------------------------------------------------------

	public void setRecursionLevel(int d){ recursionLevel = d; }
}
