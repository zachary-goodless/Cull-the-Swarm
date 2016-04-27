
using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	//PUBLIC
	public static float shakeAmount;
	public static float maxShakeAmount;

	//PRIVATE
	Vector3 originalPos;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		originalPos = transform.position;

		shakeAmount = 0f;
		maxShakeAmount = 25f;
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		if(shakeAmount > 0f)
		{
			transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeAmount -= 0.75f;
		}
		else
		{
			transform.position = originalPos;
		}
	}

//--------------------------------------------------------------------------------------------

	public static void shakeCamera()
	{
		shakeAmount = maxShakeAmount;
	}
}
