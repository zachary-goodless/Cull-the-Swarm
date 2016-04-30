
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class EMPManager : MonoBehaviour
{ 
	//PUBLIC
	public float maxEnergy = 150f;
	public float rechargeRate = 10f;

	//PRIVATE
	private GameObject empAreaPrefab;

	private float currEnergy;
	private bool isOnCooldown;

	Player playerScript;

	RectTransform energyBar;
	Vector3 origin;

	Sprite[] energySprites;
	Image energyImg;
	Coroutine blinkCoroutine;

	bool isStart = true;
	AudioSource readyAudio;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		// get handle on player script
		playerScript = GetComponent<Player> ();
		if(playerScript.chassisQuick) 
		{
			rechargeRate *= playerScript.cooldownBoost;
		}

		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();
		origin = energyBar.localPosition;

		energySprites = Resources.LoadAll<Sprite>("GUI_Assets/EnergyIcons");
		energyImg = GameObject.Find("EnergyImg").GetComponent<Image>();
		blinkCoroutine = null;

		//init emp area prefab
		empAreaPrefab = Resources.Load<GameObject>("PlayerBullets/EMPArea");

		currEnergy = maxEnergy;
		isOnCooldown = false;

		//get handle on audio source for secondary ready
		readyAudio = GetComponents<AudioSource>()[1];
	}

//--------------------------------------------------------------------------------------------

	void Update ()
	{
		if(Time.timeScale != 1f) return;

		handleEnergyBar();

		//if on cooldown...
		if(isOnCooldown)
		{
			//increase energy up to max, then no longer on cooldown
			currEnergy += rechargeRate * Time.deltaTime;
			if(currEnergy >= maxEnergy)
			{
				currEnergy = maxEnergy;
				isOnCooldown = false;
			}
		}

		//if secondary key pressed and we're not on cooldown...
		if((Input.GetButtonDown("Secondary") || Input.GetButtonDown("XBOX_B") || Input.GetButtonDown("XBOX_Y")) && !isOnCooldown && Time.timeScale == 1f)
		{
			currEnergy = 0f;
			isOnCooldown = true;

			//spawn emp area object
			GameObject weaponEffect = Instantiate(empAreaPrefab, transform.position, Quaternion.identity) as GameObject;
			weaponEffect.transform.parent = transform;
		}
	}

//--------------------------------------------------------------------------------------------

	void handleEnergyBar()
	{
		//update energy bar fill according to max energy
		Vector3 localScale = energyBar.localScale;
		localScale.y = currEnergy / maxEnergy;
		energyBar.localScale = localScale;

		//energy bar position is an offset from its start point that is some percentage of half the height
		energyBar.localPosition = origin + new Vector3(0f, energyBar.rect.height * 0.5f * localScale.y, 0f);

		//start blink if at full energy and not already blinking
		if(localScale.y == 1f && blinkCoroutine == null)
		{
			if(isStart){ isStart = false; }
			else{ readyAudio.Play(); }

			energyImg.sprite = energySprites[1];
			blinkCoroutine = StartCoroutine(handleImgBlink());
		}
		else if(localScale.y < 1f && blinkCoroutine != null)
		{
			energyImg.sprite = energySprites[0];
			StopCoroutine(blinkCoroutine);
			blinkCoroutine = null;
		}
	}

//--------------------------------------------------------------------------------------------

	public IEnumerator handleImgBlink()
	{
		while(true)
		{
			energyImg.sprite = energySprites[1];
			yield return new WaitForSeconds(0.7f);

			energyImg.sprite = energySprites[0];
			yield return new WaitForSeconds(0.15f);
		}
	}
}
