
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadoutElementButtonEventHandler : MonoBehaviour
{
	//PUBLIC
	public Loadout.LoadoutChasis chasisIndex;
	public Loadout.LoadoutPrimary primaryIndex;
	public Loadout.LoadoutSecondary secondaryIndex;

	public bool isUnlocked;

	//PRIVATE
	private LoadoutsEventHandler mParentEventHandler;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mParentEventHandler = GetComponentInParent<LoadoutsEventHandler>();
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonClicked()
	{
		if(mParentEventHandler != null)
		{
			mParentEventHandler.handleChoiceButtonClicked(chasisIndex, primaryIndex, secondaryIndex);

			//if it's a secondary button...
			if(secondaryIndex != Loadout.LoadoutSecondary.NULL)
			{
				//set the button to non-interactable
				gameObject.GetComponent<Button>().interactable = false;
			}

			//else, call on the next panel type
			else if(chasisIndex != Loadout.LoadoutChasis.NULL)
			{
				mParentEventHandler.handlePrimaryButtonClicked();
			}
			else if(primaryIndex != Loadout.LoadoutPrimary.NULL)
			{
				mParentEventHandler.handleSecondaryButtonClicked();
			}
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonMouseOver()
	{
		if(mParentEventHandler != null)
		{
			mParentEventHandler.handleChoiceButtonMouseOver(
				chasisIndex, 
				primaryIndex, 
				secondaryIndex, 
				isUnlocked);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonMouseExit()
	{
		if(mParentEventHandler != null)
		{
			mParentEventHandler.handleChoiceButtonMouseExit();
		}
	}

//--------------------------------------------------------------------------------------------

	public void setLoadoutIndices(int ci, int pi, int si)
	{
		chasisIndex = (Loadout.LoadoutChasis)ci;
		primaryIndex = (Loadout.LoadoutPrimary)pi;
		secondaryIndex = (Loadout.LoadoutSecondary)si;
	}
}
