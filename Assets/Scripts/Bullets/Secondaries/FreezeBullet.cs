
using UnityEngine;
using System.Collections;

public class FreezeBullet : MonoBehaviour
{
	//PUBLIC
	public float duration = 3f;

	//PRIVATE
	int depthRemaining = 0;

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

	IEnumerator handleDuration()
	{
		yield return new WaitForSeconds(duration);

		Destroy(gameObject);
		yield break;
	}

//--------------------------------------------------------------------------------------------

	public void setDepthRemaining(int d){ depthRemaining = d; }
}
