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

	public ParticleSystem ps;

	bool hitCool;
	public bool dead;
	int health;
	HealthBar hb;
	ScreenFade sf;

	Loadout loadout;

	//JUSTIN
	public PauseMenu pauseMenu;
	//JUSTIN

	// Use this for initialization
	void Start()
	{
		loadout = GameObject.FindGameObjectWithTag ("SaveManager").GetComponent<SavedGameManager> ().getCurrentGame ().getCurrentLoadout ();
		setLoadout ();

		hitCool = false;
		dead = false;
		health = 3;
		hb = GameObject.Find ("HealthBar").GetComponent<HealthBar> ();
		sf = GameObject.Find ("ScreenFade").GetComponent<ScreenFade> ();
		hb.health = health;	
	}

	// Update is called once per frame
	void Update()
	{
		//JUSTIN
		if(Input.GetButtonDown("Pause") && pauseMenu != null)
		{
			pauseMenu.togglePaused();
			return;
		}
		if(Time.timeScale != 1f || LevelCompleteHandler.isLevelComplete) return;
		//Time.timeScale = TimeScale;
		//JUSTIN

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

		if (!hitCool && !mesh.activeSelf) {
			mesh.SetActive (true);
		}

		mesh.transform.rotation = Quaternion.Euler(0f, shipTilt, 0f);

		if (sf.finished && dead) {
			SceneManager.LoadScene ((int)SceneIndex.WORLD_MAP);
		}
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
		hb.health = health;
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
		ParticleSystem.EmissionModule em = ps.emission;
		while (hitCool) {
			em.enabled = false;
			mesh.SetActive (!mesh.activeSelf);
			yield return new WaitForSeconds (.05f);
		}
		em.enabled = true;
		mesh.SetActive (true);
		yield break;
	}

	void OnDeath(){
		ParticleSystem.EmissionModule em = ps.emission;
		em.enabled = false;
		mesh.SetActive (false);
		dead = true;
		sf.Fade ();

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
		case Loadout.LoadoutPrimary.REPEL:
				Debug.Log ("Primary Set Bug Repellant");
				gameObject.AddComponent <StandardPrimary>();
				break;
			case Loadout.LoadoutPrimary.SWATTER:
				Debug.Log ("Primary set No-Miss Swatter");
				gameObject.AddComponent <SwatterPrimary>();
				break;
			case Loadout.LoadoutPrimary.FLAME:
				Debug.Log ("Primary set Flamethrower");
				gameObject.AddComponent <FlamethrowerPrimary>();
				break;
			case Loadout.LoadoutPrimary.VOLT:
				Debug.Log ("Primary set Volt Lantern");
				gameObject.AddComponent <LaserPrimary>();
				break;
			case Loadout.LoadoutPrimary.BUGSPRAY:
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
			case Loadout.LoadoutSecondary.EMP:
				Debug.Log ("Secondary set EMP Counter");
				gameObject.AddComponent<EMPManager>();
				break;
			case Loadout.LoadoutSecondary.PHASING:
				Debug.Log ("Secondary set Phasing System");
				break;
			case Loadout.LoadoutSecondary.TESLA:
				Debug.Log ("CSecondary set Mosquito Tesla Coil");
				gameObject.AddComponent<TeslaManager>();
				break;
			case Loadout.LoadoutSecondary.FREEZE:
				Debug.Log ("Secondary set Freeze Ray");
				gameObject.AddComponent<FreezeManager>();
				break;
			case Loadout.LoadoutSecondary.HOLOGRAM:
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
			case Loadout.LoadoutChasis.EXTERMINATOR:
				Debug.Log ("Armor Set Exterminator Chasis");
				break;
			case Loadout.LoadoutChasis.BOOSTER:
				Debug.Log ("Armor set Booster Chasis");
				break;
			case Loadout.LoadoutChasis.SHRINK:
				Debug.Log ("Armor set Shrink Chasis");
				break;
			case Loadout.LoadoutChasis.QUICK:
				Debug.Log ("Armor set Quick Chasis");
				break;
			case Loadout.LoadoutChasis.FINAL:
				Debug.Log ("Armor set Final Chasis");
				break;
			default:
				Debug.Log ("Armor not in range");
				break;
		}
	}
}
