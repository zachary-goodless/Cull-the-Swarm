using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour {

	Color blueFlame;
	Color orange;
	Color current;

	SpriteRenderer sr;


	Vector3 currentPos;

	public GameObject sprite;

	float speed;
	float period;
	float degrees;
	float amplitude;
	float decRate;
	float incRate;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		blueFlame = new Color (0.12941176f, 0.345098f, 0.972549f, 1f);
		orange = new Color (0.9568627f, 0.36862745f, 0f, 1f);
		decRate = 1f;
		incRate = 0f;
		speed = 500f;
		period = 1f;
		amplitude = 20f;
		degrees = 0;

	}
	
	// Update is called once per frame
	void Update () {
		currentPos = transform.position;
		/*currentPos.y += Mathf.Sin (Time.time * 180f) * 0.5f;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		rb2D.AddForce (transform.Up * speed * decRate);*/

		float deltaTime = Time.deltaTime;

		currentPos.y += deltaTime * speed * decRate;

		float degPerSec = 360.0f / period; 
		degrees = Mathf.Repeat (degrees + (deltaTime * degPerSec), 360.0f);
		float rads = degrees * Mathf.Deg2Rad;

		Vector3 offset = new Vector3 (amplitude*incRate * Mathf.Sin (rads), 0.0f, 0.0f);

		transform.position = currentPos + offset;

		if (decRate > .1) {
			decRate -= .01f;
		}
		if (incRate < 1) {
			incRate += .03f;
		}

		if (transform.localScale.x < 100 || transform.localScale.y < 100) {
			transform.localScale += new Vector3 (1f, 1f, 0f);
		}
		//sprite.transform.EulerAngles = new Vector3(0f,0f,0f);
	}
}
