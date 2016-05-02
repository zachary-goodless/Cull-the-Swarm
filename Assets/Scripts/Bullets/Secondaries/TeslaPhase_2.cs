
using UnityEngine;
using System.Collections;

public class TeslaPhase_2 : MonoBehaviour
{
	//PUBLIC
	public float duration = 1f;

	//PRIVATE
	float damage = 0f;
	bool isOnCooldown;

	float maxScale = 1000f;

	float scaleIncrement = 0f;
	float maxScaleIncrement = 300f;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		transform.localScale = new Vector3(2f, transform.localScale.y, transform.localScale.z);
		StartCoroutine(handleDuration());
	}

//--------------------------------------------------------------------------------------------

	void Update ()
	{
		//if not at max size...
		if(transform.localScale.y < maxScale)
		{
			//grow the growth rate
			scaleIncrement += 5f;
			if(scaleIncrement > maxScaleIncrement)
			{
				scaleIncrement = maxScaleIncrement;
			}

			//change scale and move forward accordingly
			transform.localScale += new Vector3(0f, scaleIncrement, 0f);
			transform.position += new Vector3 (0f, scaleIncrement * 0.5f, 0f);
		}
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerEnter2D (Collider2D other)
	{
		OnTriggerStay2D(other);
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerStay2D (Collider2D other)
	{
		//if enemy hit, not on cooldown, and damage has been set...
		if(other.tag == "EnemyHit" && !isOnCooldown && damage != 0f)
		{
			other.gameObject.GetComponentInParent<Movement>().health -= damage;
			other.gameObject.GetComponentInParent<Movement> ().Blink();
			StartCoroutine(handleCooldown());
		}
		else if(other.tag == "WormPart" && !isOnCooldown && damage != 0f)
		{
			other.gameObject.GetComponent<WormBod> ().mov.health -= damage;
			other.gameObject.GetComponentInParent<WormBod> ().Blink();
			StartCoroutine(handleCooldown());
		}
		else if (other.tag == "Boss"  && !isOnCooldown && damage != 0f)
		{
			other.gameObject.GetComponent<Boss>().DealDamage(damage);
			StartCoroutine(handleCooldown());
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleCooldown()
	{
		isOnCooldown = true;
		yield return new WaitForSeconds (.5f);

		isOnCooldown = false;

		yield break;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleDuration()
	{
		yield return new WaitForSeconds(duration);

		Destroy(gameObject);
		yield break;
	}

//--------------------------------------------------------------------------------------------

	public void setDamage(float d){ damage = d; }
}
