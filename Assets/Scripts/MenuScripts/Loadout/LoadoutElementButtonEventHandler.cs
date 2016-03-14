
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadoutElementButtonEventHandler : MonoBehaviour
{
	//TODO -- handle in similar fassion to the worldmap menu

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

			//if any of the indicies are not null (not a back button)...
			if(chasisIndex != Loadout.LoadoutChasis.NULL ||
				primaryIndex != Loadout.LoadoutPrimary.NULL ||
				secondaryIndex != Loadout.LoadoutSecondary.NULL)
			{
				//set the button to non-interactable
				gameObject.GetComponent<Button>().interactable = false;
			}
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonMouseOver()
	{
		if(mParentEventHandler != null)
		{
			mParentEventHandler.handleChoiceButtonMouseOver(chasisIndex, primaryIndex, secondaryIndex);
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
