
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

		//TODO -- init prefabs

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
					//TODO -- delete area obj
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
		yield return new WaitForSeconds(spinUpTime);

		isOnSpinup = false;
		isActive = true;

		//TODO -- create area obj

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
