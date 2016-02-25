using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public GameObject mesh;
	public float TimeScale = 1f;

	// Make these into constants of another class later...
	float stageWidth = 1500f;
	float stageHeight = 950f;
	float moveSpeed = 10f;
	float precisionSpeed = 5f;
	float shipTilt = 0f;

	bool hitCool;
	public bool dead;
	int health;
	KillCount kc;

	Loadout loadout;

	// Use this for initialization
	void Start()
	{
		loadout = GameObject.FindGameObjectWithTag ("SaveManager").GetComponent<SavedGameManager> ().getCurrentGame ().getCurrentLoadout ();
		setLoadout ();

		hitCool = false;
		dead = false;
		health = 5;
		kc = GameObject.Find ("KillCount").GetComponent<KillCount> ();
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

		mesh.transform.rotation = Quaternion.Euler(0f, shipTilt, 0f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((other.tag == "Bullet" || other.tag == "EnemyHit")&& !hitCool) {
			GetComponent<AudioSource> ().Play ();
			OnDamage ();
		}
	}

	void OnDamage(){
		hitCool = true;
		health--;
		kc.health = health;
		if (health <= 0) {
			OnDeath ();
		} else {
			StartCoroutine ("HitCool");
			StartCoroutine ("Blink");
		}
	}

	IEnumerator HitCool(){
		yield return new WaitForSeconds (1f);
		hitCool = false;
		yield break;
	}

	IEnumerator Blink(){
		while (hitCool) {
			mesh.SetActive (!mesh.activeSelf);
			yield return new WaitForSeconds (.05f);
		}
		mesh.SetActive (true);
		yield break;
	}

	void OnDeath(){
		mesh.SetActive (false);
		dead = true;
		SceneManager.LoadScene ((int)SceneIndex.WORLD_MAP);
	}
	void setLoadout(){
		setPrimary ();
		setSecondary ();
		setChasis ();
	}
	void setPrimary(){
		switch(loadout.getPrimary()){
			case Loadout.LoadoutPrimary.NULL:
				Debug.Log ("Primary NULL");
				break;
		case Loadout.LoadoutPrimary.DEFAULT:
				Debug.Log ("Primary Set Bug Repellant");
				gameObject.AddComponent <StandardPrimary>();
				break;
			case Loadout.LoadoutPrimary.PRIMARY_1:
				Debug.Log ("Primary set No-Miss Swatter");
				gameObject.AddComponent <SwatterPrimary>();
				break;
			case Loadout.LoadoutPrimary.PRIMARY_2:
				Debug.Log ("Primary set Flamethrower");
				gameObject.AddComponent <FlamethrowerPrimary>();
				break;
			case Loadout.LoadoutPrimary.PRIMARY_3:
				Debug.Log ("Primary set Volt Lantern");
				gameObject.AddComponent <LaserPrimary>();
				break;
			case Loadout.LoadoutPrimary.PRIMARY_4:
				Debug.Log ("Primary set Bug Spray");
				gameObject.AddComponent <BugSprayPrimary>();
				break;
			default:
				Debug.Log ("Primary not in range");
				break;
		}
	}
	void setSecondary(){
		switch (loadout.getSecondary ()) {
			case Loadout.LoadoutSecondary.NULL:
				Debug.Log ("Secondary NULL");
				break;
		case Loadout.LoadoutSecondary.DEFAULT:
				Debug.Log ("Secondary set Phasing System");
				break;
			case Loadout.LoadoutSecondary.SECONDARY_1:
				Debug.Log ("Secondary set EMP Counter");
				break;
			case Loadout.LoadoutSecondary.SECONDARY_2:
				Debug.Log ("CSecondary set Mosquito Tesla Coil");
				break;
			case Loadout.LoadoutSecondary.SECONDARY_3:
				Debug.Log ("Secondary set Freeze Ray");
				break;
			case Loadout.LoadoutSecondary.SECONDARY_4:
				Debug.Log ("Secondary set Holo Duplicate");
				break;
			default:
				Debug.Log ("Secondary not in range");
				break;
		}
	}
	void setChasis(){
		switch (loadout.getChasis ()) {
			case Loadout.LoadoutChasis.NULL:
				Debug.Log ("Armor NULL");
				break;
			case Loadout.LoadoutChasis.DEFAULT:
				Debug.Log ("Armor Set Exterminator Chasis");
				break;
			case Loadout.LoadoutChasis.CHASIS_1:
				Debug.Log ("Armor set Booster Chasis");
				break;
			case Loadout.LoadoutChasis.CHASIS_2:
				Debug.Log ("Armor set Shrink Chasis");
				break;
			case Loadout.LoadoutChasis.CHASIS_3:
				Debug.Log ("Armor set Quick Chasis");
				break;
			case Loadout.LoadoutChasis.CHASIS_4:
				Debug.Log ("Armor set Final Chasis");
				break;
			default:
				Debug.Log ("Armor not in range");
				break;
		}
	}
}
