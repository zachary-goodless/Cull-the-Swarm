
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
	private Player playerScript; 
	//PUBLIC
	public float maxEnergy = 100f;
	public float rechargeRate = 20f;
	public float consumptionRate = 20f;


	public float cooldownDelay = 2f;

	public RectTransform energyBar;

	//PRIVATE


	private float currEnergy;

	private bool isActive;
	private bool isOnCooldown;
	private bool isOnCooldownDelay;

	//--------------------------------------------------------------------------------------------

	void Start ()
	{
		playerScript = GetComponent<Player> ();
		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();

		//init prefab
	

		currEnergy = maxEnergy;

		isActive = false;
		isOnCooldown = false;
		isOnCooldownDelay = false;
	}

	//--------------------------------------------------------------------------------------------

	void Update ()
	{
		if(Time.timeScale != 1f) return;

		//update energy bar fill according to max energy
		Vector3 localScale = energyBar.localScale;
		localScale.y = currEnergy / maxEnergy;
		energyBar.localScale = localScale;

		//if button is pressed, and is at max energy, and not on spinup...
		if(Input.GetButtonDown("Secondary") && currEnergy == maxEnergy )
		{
			isActive = true;
			//start spin up

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

			}
		}

		//if the weapon is cooling down...
		if(isOnCooldown)
		{
			if (playerScript.
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

	

	//--------------------------------------------------------------------------------------------

	IEnumerator handleCoolDownDelay()
	{
		yield return new WaitForSeconds(cooldownDelay);

		isOnCooldownDelay = false;
		isOnCooldown = true;

		yield break;
	}
}
