
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TeslaManager : MonoBehaviour
{
	//PUBLIC
	public float maxEnergy = 200f;
	public float consumptionRate = 40f;
	public float rechargeRate = 20f;

	public float minChargeTime = 1f;
	public float cooldownDelay = 2f;

	//PRIVATE
	private GameObject teslaPrefabPhase_1;
	private GameObject teslaPrefabPhase_2;

	private GameObject teslaObjPhase_1;

	private Transform forwardPos;

	private float currEnergy;
	private float storedDamage;

	private bool isCharging;
	private bool isOnInitialActivate;
	private bool isOnCooldownDelay;
	private bool isOnCooldown;

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
		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();
		origin = energyBar.localPosition;

		energySprites = Resources.LoadAll<Sprite>("GUI_Assets/EnergyIcons");
		energyImg = GameObject.Find("EnergyImg").GetComponent<Image>();
		blinkCoroutine = null;

		//init prefabs
		teslaPrefabPhase_1 = Resources.Load<GameObject>("PlayerBullets/TeslaPhase_1");
		teslaPrefabPhase_2 = Resources.Load<GameObject>("PlayerBullets/TeslaPhase_2");

		forwardPos = transform.Find ("GunF");

		currEnergy = maxEnergy;
		storedDamage = 0f;

		isCharging = false;
		isOnInitialActivate = false;
		isOnCooldownDelay = false;
		isOnCooldown = false;

		//get handle on audio source for secondary ready
		readyAudio = GetComponents<AudioSource>()[1];
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		if(Time.timeScale != 1f) return;

		handleEnergyBar();

		//if the weapon is charging...
		if(isCharging)
		{
			//reduce energy down to min, then no longer charging
			currEnergy -= consumptionRate * Time.deltaTime;
			if(currEnergy < 0f)
			{
				currEnergy = 0f;
				isCharging = false;

				//start cooldown delay
				isOnCooldownDelay = true;
				StartCoroutine(handleCoolDownDelay());

				enterWeaponPhase_2();
			}
		}

		//if on cooldown...
		if(isOnCooldown)
		{
			//increase energy up to max
			currEnergy += rechargeRate * Time.deltaTime;
			if(currEnergy >= maxEnergy)
			{
				currEnergy = maxEnergy;
				isOnCooldown = false;
			}
		}

		//if the secondary input has been pressed, we have energy, and it's not already charging or on cooldown...
		if((Input.GetButtonDown("Secondary") || Input.GetButtonDown("XBOX_B") || Input.GetButtonDown("XBOX_Y")) && currEnergy > 0f && !isCharging && !isOnCooldown)
		{
			//enter the weapon's first phase
			if(teslaObjPhase_1 == null)
			{
				//handle the initial charge
				isCharging = isOnInitialActivate = true;
				StartCoroutine(handleInitialCharge());

				//create phase 1 obj
				Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, -50f);
				teslaObjPhase_1 = Instantiate(teslaPrefabPhase_1, spawnPos, Quaternion.identity) as GameObject;
				teslaObjPhase_1.transform.parent = transform;
			}
		}

		//if the secondary input is not being pressed and it's not still on the initial charge...
		if(!(Input.GetButton("Secondary") || Input.GetButton("XBOX_B") || Input.GetButton("XBOX_Y")) && !isOnInitialActivate)
		{
			//stop charging
			isCharging = false;

			enterWeaponPhase_2();
		}
	}

//--------------------------------------------------------------------------------------------

	void enterWeaponPhase_2()
	{
		//if a phase 1 object is present...
		if(teslaObjPhase_1 != null)
		{
			//extract phase 1 obj stored damage, destroy it
			TeslaPhase_1 phaseOneScript = teslaObjPhase_1.GetComponent<TeslaPhase_1>();
			storedDamage = phaseOneScript.getStoredDamage();
			phaseOneScript.turnOff();

			//if any damage stored, enter phase 2
			if(storedDamage > 0f)
			{
				Vector3 spawnPos = new Vector3(forwardPos.position.x, forwardPos.position.y, 50f);
				GameObject objPhase_2 = Instantiate(teslaPrefabPhase_2, spawnPos, Quaternion.identity) as GameObject;
				objPhase_2.transform.parent = forwardPos;

				objPhase_2.GetComponent<TeslaPhase_2>().setDamage(storedDamage);
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
		else if(localScale.y == 0f && blinkCoroutine != null)
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

	IEnumerator handleInitialCharge()
	{
		yield return new WaitForSeconds(minChargeTime);
		isOnInitialActivate = false;

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
