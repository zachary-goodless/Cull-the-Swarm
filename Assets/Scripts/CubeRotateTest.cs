using UnityEngine;
using System.Collections;

public class CubeRotateTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(30f, 10f, 5f) * Time.deltaTime);
	}
}
