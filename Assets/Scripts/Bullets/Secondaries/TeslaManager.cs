
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class TeslaManager : MonoBehaviour
{
	//PUBLIC
	public float maxEnergy = 200f;
	public float consumptionRate = 20f;

	public float minChargeTime = 1f;

	public RectTransform energyBar;

	//PRIVATE
	private GameObject teslaPrefabPhase_1;
	private GameObject teslaPrefabPhase_2;

	private GameObject teslaObjPhase_1;

	private float currEnergy;
	private float storedDamage;

	private bool isCharging;
	private bool isOnInitialActivate;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();

		//TODO -- init prefabs

		currEnergy = maxEnergy;
		storedDamage = 0f;

		isCharging = false;
		isOnInitialActivate = false;
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//update energy bar fill according to max energy
		Vector3 localScale = energyBar.localScale;
		localScale.y = currEnergy / maxEnergy;
		energyBar.localScale = localScale;

		//if the weapon is charging...
		if(isCharging)
		{
			//reduce energy down to min, then no longer charging
			currEnergy -= consumptionRate * Time.deltaTime;
			if(currEnergy < 0f)
			{
				currEnergy = 0f;
				isCharging = false;

				//enter the weapon's second phase
				if(teslaObjPhase_1 != null)
				{
					//TODO -- extract phase 1 obj stored damage, destroy it
					//TODO -- create phase 2 obj, pass it stored damage
				}
			}
		}

		//if the secondary input has been pressed, we have energy, and it's not already charging...
		if(Input.GetButtonDown("Secondary") && currEnergy > 0f && !isCharging)
		{
			//handle the initial charge
			isCharging = isOnInitialActivate = true;
			StartCoroutine(handleInitialCharge());

			//enter the weapon's first phase
			if(teslaObjPhase_1 == null)
			{
				//TODO -- create phase 1 obj...
			}
		}

		//if the secondary input is not being pressed and it's not still on the initial charge...
		if(!Input.GetButton("Secondary") && !isOnInitialActivate)
		{
			//stop charging
			isCharging = false;

			//store damage if the weapon's first phase is active
			if(teslaObjPhase_1 != null)
			{
				//TODO -- extract phase 1 obj stored damage, destroy it
				//TODO -- create phase 2 obj, pass it stored damage	???
			}
		}
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleInitialCharge()
	{
		yield return new WaitForSeconds(minChargeTime);
		isOnInitialActivate = false;

		yield break;
	}
}
