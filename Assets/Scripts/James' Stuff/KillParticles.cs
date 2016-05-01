
using UnityEngine;
using System.Collections;

public class KillParticles : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(handleLifetime());
	}

	IEnumerator handleLifetime()
	{
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);

		yield break;
	}
}
