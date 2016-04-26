
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadoutElementButtonEventHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
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
			handleButtonMouseOver();
		}
	}

//--------------------------------------------------------------------------------------------

	public void OnSelect(BaseEventData eventData)
	{
		handleButtonMouseOver();
	}

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

	public void OnDeselect(BaseEventData eventData)
	{
		handleButtonMouseExit();
	}

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
