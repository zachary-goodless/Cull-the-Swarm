
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{	
	//PUBLIC
	public float maxEnergy = 100f;
	public float rechargeRate = 20f;
	public float consumptionRate = 50f;

	public float minChargeTime = 1f;
	public float cooldownDelay = 2f;

	//PRIVATE
	private Player playerScript; 

	private float currEnergy;

	public bool isActive;
	private bool isOnInitialActivate;
	private bool isOnCooldown;
	private bool isOnCooldownDelay;
    bool blinking = false;
    MeshRenderer[] meshList;

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

        meshList =  playerScript.GetComponentsInChildren<MeshRenderer>();

        currEnergy = maxEnergy;

		isActive = false;
		isOnCooldown = false;
		isOnCooldownDelay = false;
		isOnInitialActivate = false;

		//get handle on audio source for secondary ready
		readyAudio = GetComponents<AudioSource>()[1];
	}

//--------------------------------------------------------------------------------------------

	void Update ()
	{
		if(Time.timeScale != 1f) return;

		handleEnergyBar();

		//if button pressed, has energy, and not already active...
		if((Input.GetButtonDown("Secondary") || Input.GetButtonDown("XBOX_B") || Input.GetButtonDown("XBOX_Y")) && currEnergy > 0f && !isActive && !isOnCooldown)
		{
			//handle the initial charge
			isActive = isOnInitialActivate = true;
			StartCoroutine(handleInitialCharge());
		}

		//if button not pressed and not on initial activate...
		if(!(Input.GetButton("Secondary") || Input.GetButton("XBOX_B") || Input.GetButton("XBOX_Y")) && !isOnInitialActivate)
		{
			isActive = false;
		}

		//if active...
		if(isActive)
		{
			Blink();

			//reduce energy down to min
			currEnergy -= consumptionRate * Time.deltaTime;
			if(currEnergy < 0f && !isOnCooldownDelay)
			{
				currEnergy = 0f;

				isActive = false;
				isOnCooldownDelay = true;
				StartCoroutine(handleCoolDownDelay());
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
			yield return new WaitForSeconds(0.7f);
			energyImg.gameObject.SetActive(false);

			yield return new WaitForSeconds(0.15f);
			energyImg.gameObject.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

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

    IEnumerator handleCoolDownDelay()
	{
		yield return new WaitForSeconds(cooldownDelay);

		isOnCooldownDelay = false;
		isOnCooldown = true;

		yield break;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleInitialCharge()
	{
		yield return new WaitForSeconds(minChargeTime);
		isOnInitialActivate = false;

		yield break;
	}
}
