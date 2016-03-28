
using UnityEngine;
using System.Collections;

public class EMPArea : MonoBehaviour
{
	//PUBLIC
	public float duration = 1f;
	public float delayBetweenTicks = 0.01f;

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
		OnTriggerStay2D(other);
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerStay2D(Collider2D other)
	{
		//if this hits an enemy...
		if(other.tag == "EnemyHit")
		{
			//TODO -- grab enemy script, turn off canfire bool
		}

		//TODO -- if other is enemy bullet?
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
