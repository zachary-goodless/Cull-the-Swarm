
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class EMPManager : MonoBehaviour
{
	//PUBLIC
	public float maxEnergy = 150f;
	public float rechargeRate = 10f;

	public RectTransform energyBar;

	//PRIVATE
	private GameObject empAreaPrefab;

	private float currEnergy;
	private bool isOnCooldown;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();

		//init emp area prefab
		empAreaPrefab = Resources.Load<GameObject>("PlayerBullets/EMPArea");

		currEnergy = maxEnergy;
		isOnCooldown = false;
	}

//--------------------------------------------------------------------------------------------

	void Update ()
	{
		//update energy bar fill according to max energy
		Vector3 localScale = energyBar.localScale;
		localScale.y = currEnergy / maxEnergy;
		energyBar.localScale = localScale;

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
		if(Input.GetButtonDown("Secondary") && !isOnCooldown)
		{
			currEnergy = 0f;
			isOnCooldown = true;

			//spawn emp area object
			GameObject weaponEffect = Instantiate(empAreaPrefab, transform.position, Quaternion.identity) as GameObject;
			weaponEffect.transform.parent = transform;
		}
	}
}

