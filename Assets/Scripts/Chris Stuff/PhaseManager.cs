
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
	
	//PUBLIC
	public float maxEnergy = 100f;
	public float rechargeRate = 20f;
	public float consumptionRate = 50f;


	public float cooldownDelay = 2f;

	public RectTransform energyBar;

	//PRIVATE
	private Player playerScript; 

	private float currEnergy;

	public bool isActive;
	private bool isOnCooldown;
	private bool isOnCooldownDelay;
    bool blinking = false;
    MeshRenderer[] meshList;

    //--------------------------------------------------------------------------------------------

    void Start ()
	{

		// get handle on player script
		playerScript = GetComponent<Player> ();
		//get handle on energy bar
		energyBar = GameObject.Find("EnergyBar").GetComponent<RectTransform>();
        meshList =  playerScript.GetComponentsInChildren<MeshRenderer>();

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
		if(Input.GetButtonDown("Secondary") && currEnergy > 10 && !isOnCooldown)
		{
			isActive = true;


		}
		else if(!Input.GetButton("Secondary") && !isOnCooldown)
		{
			
			isActive = false;

		}

		//if the weapon is active...
		if (isActive) {
            Blink();

            currEnergy -= consumptionRate * Time.deltaTime;
			//enter cooldown delay if the weapon hasn't already
			if (currEnergy <= 0f && !isOnCooldownDelay) {
				currEnergy = 0f;

				isActive = false;
				isOnCooldownDelay = true;

				StartCoroutine (handleCoolDownDelay ());

				//destroy the area obj if it hasn't already been

			}
		} 
		else if (currEnergy < maxEnergy && !isOnCooldown)
		{
			if (playerScript.chassisQuick) 
			{
				currEnergy += rechargeRate * Time.deltaTime;
			} 
			else
			{
				currEnergy += rechargeRate * Time.deltaTime;
			}
		} 
		else if (currEnergy >= maxEnergy)
		{
			currEnergy = maxEnergy;
			isOnCooldown = false;
		}
				
		//if the weapon is cooling down...
		if(isOnCooldown)
		{
			if (playerScript.chassisQuick) 
			{
				currEnergy += rechargeRate * Time.deltaTime * playerScript.cooldownBoost;
			} 
			else 
			{
				//increase energy up to max, then no longer on cooldown
				currEnergy += rechargeRate * Time.deltaTime;
			}
			if(currEnergy >= maxEnergy)
			{
				currEnergy = maxEnergy;
				isOnCooldown = false;
			}
		}
    }

    public void Blink()
    {
        blinking = true;
        foreach (MeshRenderer m in meshList)
        {
            if (m)
            {
                m.material.SetColor("_Color", new Color(0.3f, 0.3f, 1f, 1f));
            }
        }

        Invoke("Reveal", .1f);
    }

    void Reveal()
    {
        foreach (MeshRenderer m in meshList)
        {
            if (m)
            {
                m.material.SetColor("_Color", Color.white);
            }
        }

        blinking = false;

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
