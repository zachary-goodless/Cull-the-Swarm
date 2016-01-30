using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject model;
	public float TimeScale = 1f;

	// Make these into constants of another class later...
	float stageWidth = 1500f;
	float stageHeight = 950f;
	float moveSpeed = 10f;
	float precisionSpeed = 5f;
	float shipTilt = 0f;

	public Transform gunR;
	public Transform gunL;
	float shootCool;
	float shootTimer;
	public GameObject bullet;

	// Use this for initialization
	void Start()
	{
		shootCool = .2f;
	}

	// Update is called once per frame
	void Update()
	{
		Time.timeScale = TimeScale;

		// Get input from controller.
		float hSpeed = Mathf.Round(Input.GetAxisRaw("Horizontal"));
		float vSpeed = Mathf.Round(Input.GetAxisRaw("Vertical"));

		// Slightly slower when moving diagonally.
		if (hSpeed != 0 && vSpeed != 0){
			hSpeed *= 0.7f;
			vSpeed *= 0.7f;
		}

		if (Input.GetButtonDown ("Primary")) {
			StartCoroutine ("Firing");
		}

		// Adjust for precision mode.
		if (Input.GetButton("Precision"))
		{
			hSpeed *= precisionSpeed;
			vSpeed *= precisionSpeed;
		}
		else
		{
			hSpeed *= moveSpeed;
			vSpeed *= moveSpeed;
		}

		shipTilt = shipTilt * 0.8f - (2 * hSpeed) * 0.2f;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x + hSpeed, -stageWidth / 2, stageWidth / 2),
			Mathf.Clamp(transform.position.y + vSpeed, -stageHeight / 2, stageHeight / 2),
			0
		);

		model.transform.rotation = Quaternion.Euler(0f, shipTilt, 0f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GetComponent<AudioSource>().Play();
	}

	void Shoot(){
		Instantiate (bullet, gunL.position, Quaternion.identity);
		Instantiate (bullet, gunR.position, Quaternion.identity);
	}

	IEnumerator Firing(){
		while(Input.GetButton("Primary")){
			Shoot();
			yield return new WaitForSeconds(shootCool);
		}
		yield break;
	}
}
