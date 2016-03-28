
using UnityEngine;
using System.Collections;

public class EMPArea : MonoBehaviour
{
	//PUBLIC
	public float duration = 1.5f;
	public float delayBetweenTicks = 0.00001f;

	//PRIVATE
	//

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		StartCoroutine(handleGrow());
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleGrow()
	{
		float remainingTime = duration;

		yield break;
	}
}
