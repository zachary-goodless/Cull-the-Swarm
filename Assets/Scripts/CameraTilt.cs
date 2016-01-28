using UnityEngine;
using System.Collections;

public class CameraTilt : MonoBehaviour {

    GameObject p;

	// Use this for initialization
	void Start () {
        p = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(0, p.transform.position.x / 100, 0));
	}
}
