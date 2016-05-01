
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class HoloManager : MonoBehaviour
{
	//PUBLIC
	public float maxEnergy = 200f;
	public float rechargeRate = 10f;
	public float consumptionRate = 40f;

	public float cooldownDelay = 2f;

	//PRIVATE
	private float currEnergy;

	bool isActive;
	bool isOnCooldownDelay;
	bool isOnCooldown;

	GameObject holoPrefab;
	GameObject holoObj;

	Player playerScript;

	RectTransform energyBar;
	Vector3 origin;

	Sprite[] energySprites;
	Image energyImg;
	Coroutine blinkCoroutine;

	bool isStart = true;
	AudioSource readyAudio;

//--------------------------------------------------------------------------------------------

	void Start()
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

		currEnergy = maxEnergy;

		isActive = false;
		isOnCooldownDelay = false;
		isOnCooldown = false;

		//init prefab
		holoPrefab = Resources.Load<GameObject>("PlayerBullets/Duplicate");

		//get handle on audio source for secondary ready
		readyAudio = GetComponents<AudioSource>()[1];
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		if(Time.timeScale != 1f) return;

		handleEnergyBar();

		//if button is pressed, and is at max energy, and not on spinup...
		if((Input.GetButtonDown("Secondary") || Input.GetButtonDown("XBOX_B") || Input.GetButtonDown("XBOX_Y")) && currEnergy == maxEnergy)
		{
			//spawn holo object
			isActive = true;
			holoObj = GameObject.Instantiate(holoPrefab, transform.position, Quaternion.identity) as GameObject;
		}

		//if currently active...
		if(isActive)
		{
			holoObj.transform.rotation = playerScript.mesh.transform.rotation;

			//reduce energy down to min
			currEnergy -= consumptionRate * Time.deltaTime;

			//enter cooldown delay if the weapon hasn't already
			if(currEnergy < 0f && !isOnCooldownDelay)
			{
				currEnergy = 0f;
				isActive = false;
				isOnCooldownDelay = true;

				StartCoroutine(handleCoolDownDelay());
				holoObj.GetComponent<HoloDuplicate>().turnOff();
			}
		}

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

	IEnumerator handleCoolDownDelay()
	{
		yield return new WaitForSeconds(cooldownDelay);

		isOnCooldownDelay = false;
		isOnCooldown = true;

		yield break;
	}
}
