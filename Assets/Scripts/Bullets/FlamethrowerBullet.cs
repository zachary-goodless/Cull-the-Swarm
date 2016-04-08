using UnityEngine;
using System.Collections;

public class FlamethrowerBullet : MonoBehaviour {

	//Flame changes color from the start
	Color blueFlame;
	Color orange;
	Color currentCol;
	string color;
	public Gradient gradient;

	//Keep track of sprite for color changing
	SpriteRenderer sr;

	Vector3 currentPos;

	//may be obsolete; for preventing the image roatation when its parent does.
	public GameObject sprite;

	//stats
	float speed;
	float period;
	public float degrees;
	float amplitude;
	float decRate;
	float incRate;
	float dmg;

	float timer;
	float lifeTime;
	float lerpVal;


	// Use this for initialization
	void Start () {
		sr = GetComponentInChildren<SpriteRenderer>();
		currentCol = sr.color;
		blueFlame = new Color (0.12941176f, 0.345098f, 0.972549f, 1f);
		orange = new Color (0.9568627f, 0.36862745f, 0f, 1f);
		decRate = 1f;
		incRate = 0f;
		speed = 700f;
		period = 1f;
		amplitude = 6f;
		timer = 0;
		lifeTime = 1;
		color = "blue";
		lerpVal = 0;
		dmg = 5;

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 1f) return;

		float deltaTime = Time.deltaTime;

		if (timer < lifeTime) {
			timer += Time.deltaTime;
		} else {
			Destroy (gameObject);
		}

		currentPos = transform.position;
	
		ColorCycle ();

		//Sinewave stuff
		currentPos.y += deltaTime * speed * decRate;

		float degPerSec = 360.0f / period; 
		degrees = Mathf.Repeat (degrees + (deltaTime * degPerSec), 360.0f);
		float rads = degrees * Mathf.Deg2Rad;

		Vector3 offset = new Vector3 (amplitude*incRate * Mathf.Sin (rads), 0.0f, 0.0f);

		transform.position = currentPos + offset;

		//Decrease speed and increase amplitude
		if (decRate > .1) {
			decRate -= .01f;
		}
		if (incRate < 1) {
			incRate += .02f;
		}

		//increase scale
		if (transform.localScale.x < 200 || transform.localScale.y < 200) {
			transform.localScale += new Vector3 (2f, 2f, 0f);
		}
		//sprite.transform.EulerAngles = new Vector3(0f,0f,0f);
		//kill when off screen
		if (!sr.isVisible) {
			Destroy (gameObject);
		}
	}

	void ColorCycle(){
		currentCol = sr.color;
		if (color == "blue") {
			currentCol = Color.Lerp (currentCol, Color.yellow, lerpVal);
			if (lerpVal >= 1) {
				color = "yellow";
				lerpVal = 0;
			}
			lerpVal += (Time.deltaTime/lifeTime)*3f;
		} else if (color == "yellow") {
			currentCol = Color.Lerp (currentCol, orange, lerpVal);
			if (lerpVal >= 1) {
				color = "orange";
				lerpVal = 0;
			}
			lerpVal += (Time.deltaTime/lifeTime)*3f;
		} else if (color == "orange" && lerpVal < 1) {
			currentCol = Color.Lerp (currentCol, Color.clear, lerpVal);
			lerpVal += (Time.deltaTime/lifeTime)*3f;
		}
		sr.color = currentCol;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "EnemyHit") {
			other.gameObject.GetComponentInParent<Movement> ().health -= dmg;
			Destroy (gameObject);
		}
	}
}
