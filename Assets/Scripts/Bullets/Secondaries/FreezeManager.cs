
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class FreezeManager : MonoBehaviour
{
	//PUBLIC
	public float maxEnergy = 200f;
	public float rechargeRate = 20f;
	public float consumptionRate = 40f;

	public float spinUpTime = 2f;
	public float cooldownDelay = 2f;

	public RectTransform energyBar;

	//PRIVATE
	private GameObject freezeAreaPrefab;
	private GameObject freezeAreaInstance;

	private float currEnergy;

	private bool isOnSpinup;
	private bool isActive;
	private bool isOnCooldown;
	private bool isOnCooldownDelay;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();

		//init prefab
		freezeAreaPrefab = Resources.Load<GameObject>("PlayerBullets/FreezeArea");

		currEnergy = maxEnergy;

		isOnSpinup = false;
		isActive = false;
		isOnCooldown = false;
		isOnCooldownDelay = false;
	}

//--------------------------------------------------------------------------------------------

	void Update ()
	{
		//update energy bar fill according to max energy
		Vector3 localScale = energyBar.localScale;
		localScale.y = currEnergy / maxEnergy;
		energyBar.localScale = localScale;

		//if button is pressed, and is at max energy, and not on spinup...
		if(Input.GetButtonDown("Secondary") && currEnergy == maxEnergy && !isOnSpinup)
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
					//delete area obj
					Destroy(freezeAreaInstance);
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

	IEnumerator handleSpinup()
	{
		//create area obj
		freezeAreaInstance = Instantiate(freezeAreaPrefab, transform.position, Quaternion.identity) as GameObject;
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
