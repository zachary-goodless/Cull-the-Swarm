
using UnityEngine;
using System.Collections;

public class GrazeArea : MonoBehaviour
{
	//PUBLIC
	public bool isForHolo = false;

	//PRIVATE
	Score scoreHandle = null;

	Transform playerTransform = null;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		scoreHandle = GameObject.Find("Score").GetComponent<Score>();

		playerTransform = GameObject.Find("Player").transform;
		if(playerTransform != null)
		{
			transform.position = playerTransform.position;
		}
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		if(!isForHolo)
		{
			if(playerTransform != null)
			{
				transform.position = playerTransform.position;
			}
		}
	}

//--------------------------------------------------------------------------------------------

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Bullet" || other.tag == "EnemyHit")
		{
			if(scoreHandle != null)
			{
				scoreHandle.handleGraze();
			}
		}
	}
}
