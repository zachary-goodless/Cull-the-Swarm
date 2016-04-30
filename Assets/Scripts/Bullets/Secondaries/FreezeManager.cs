
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class FreezeManager : MonoBehaviour
{
	//PUBLIC
	public float maxEnergy = 200f;
	public float rechargeRate = 20f;
	public float consumptionRate = 50f;

	public float spinUpTime = 2f;
	public float cooldownDelay = 2f;

	//PRIVATE
	private GameObject freezeAreaPrefab;
	private GameObject freezeAreaInstance;

	private float currEnergy;

	private bool isOnSpinup;
	private bool isActive;
	private bool isOnCooldown;
	private bool isOnCooldownDelay;

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

		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();
		origin = energyBar.localPosition;

		energySprites = Resources.LoadAll<Sprite>("GUI_Assets/EnergyIcons");
		energyImg = GameObject.Find("EnergyImg").GetComponent<Image>();
		blinkCoroutine = null;

		//init prefab
		freezeAreaPrefab = Resources.Load<GameObject>("PlayerBullets/FreezeArea");

		currEnergy = maxEnergy;

		isOnSpinup = false;
		isActive = false;
		isOnCooldown = false;
		isOnCooldownDelay = false;

		//get handle on audio source for secondary ready
		readyAudio = GetComponents<AudioSource>()[1];
	}

//--------------------------------------------------------------------------------------------

	void Update ()
	{
		if(Time.timeScale != 1f) return;

		handleEnergyBar();

		//if button is pressed, and is at max energy, and not on spinup...
		if((Input.GetButtonDown("Secondary") || Input.GetButtonDown("XBOX_B") || Input.GetButtonDown("XBOX_Y")) && currEnergy == maxEnergy && !isOnSpinup)
		{
			//start spin up
			isOnSpinup = true;
			StartCoroutine(handleSpinup());
		}

		//if the weapon is active...
		if(isActive)
		{
			//reduce energy down to min
			currEnergy -= consumptionRate * Time.deltaTime;

			//enter cooldown delay if the weapon hasn't already
			if(currEnergy < 0f && !isOnCooldownDelay)
			{
				currEnergy = 0f;

				isActive = false;
				isOnCooldownDelay = true;

				StartCoroutine(handleCoolDownDelay());

				//destroy the area obj if it hasn't already been
				if(freezeAreaInstance != null)
				{
					//remove area obj freeze ability and delete
					freezeAreaInstance.GetComponent<FreezeArea>().turnOff();
				}

				//for each freeze bullet...
				foreach(FreezeBullet fb in GameObject.FindObjectsOfType<FreezeBullet>())
				{
					//diable its freeze ability
					fb.canMakeMore = false;
				}
			}
		}

		//if the weapon is cooling down...
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

//--------------------------------------------------------------------------------------------

	IEnumerator handleSpinup()
	{
		//create area obj
		Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, -50f);
		freezeAreaInstance = Instantiate(freezeAreaPrefab, spawnPos, Quaternion.identity) as GameObject;
		freezeAreaInstance.transform.parent = transform;

		yield return new WaitForSeconds(spinUpTime);

		isOnSpinup = false;
		isActive = true;

		yield break;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleCoolDownDelay()
	{
		yield return new WaitForSeconds(cooldownDelay);

		isOnCooldownDelay = false;
		isOnCooldown = true;

		yield break;
	}
}
