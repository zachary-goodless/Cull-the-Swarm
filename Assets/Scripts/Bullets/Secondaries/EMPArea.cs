
using UnityEngine;
using System.Collections;

public class EMPArea : MonoBehaviour
{
	//PUBLIC
	public float duration = 0.75f;
	public float delayBetweenTicks = 0.01f;

	public float enemyDisabledCooldown = 3f;

	//PRIVATE
	//

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		StartCoroutine(handleGrow());
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerEnter2D(Collider2D other)
	{
		//if this hits an enemy...
		if(other.tag == "EnemyHit")
		{
			//disable enemy's ability to fire bullets
			Drones_1_1 enemyFireScript = other.GetComponentInParent<Drones_1_1>();
			if(enemyFireScript != null)
			{
				enemyFireScript.setFireDisabled(enemyDisabledCooldown);
			}
		}

		//other is enemy bullet...
		else if(other.tag == "Bullet")
		{
			//reset bullet???
			BulletManager.DeleteBullet(other.gameObject);
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleGrow()
	{
		float remainingTime = duration;

		while(remainingTime > 0f)
		{
			//grow the area's size
			Vector3 scale = transform.localScale;
			scale.x = scale.y += delayBetweenTicks * 10000;
			transform.localScale = scale;

			//wait for a short time
			yield return new WaitForSeconds(delayBetweenTicks);

			//reduce remaining time
			remainingTime -=delayBetweenTicks;
		}

		Destroy(gameObject);

		yield break;
	}
}
