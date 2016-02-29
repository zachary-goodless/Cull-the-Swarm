
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
	private GameObject mParentPanel;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//get the worldmap canvas event handler
		mParentEventHandler = GetComponentInParent<LoadoutsEventHandler>();
		mParentPanel = transform.parent.gameObject;

		//TODO -- temp force button text
		if(chasisIndex != Loadout.LoadoutChasis.NULL)
		{
			gameObject.GetComponentInChildren<Text>().text = chasisIndex.ToString();
		}
		else if(primaryIndex != Loadout.LoadoutPrimary.NULL)
		{
			gameObject.GetComponentInChildren<Text>().text = primaryIndex.ToString();
		}
		else if(secondaryIndex != Loadout.LoadoutSecondary.NULL)
		{
			gameObject.GetComponentInChildren<Text>().text = secondaryIndex.ToString();
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonClicked()
	{
		if(mParentEventHandler != null)
		{
			//call to the canvas -- sets the selected loadout to this button's indicies
			mParentEventHandler.handleLoadoutElementButtonClicked(
				chasisIndex, 
				primaryIndex, 
				secondaryIndex);

			//if all indicies are NULL, this is a back button, disable the panel
			if(chasisIndex == Loadout.LoadoutChasis.NULL &&
				primaryIndex == Loadout.LoadoutPrimary.NULL &&
				secondaryIndex == Loadout.LoadoutSecondary.NULL)
			{
				mParentPanel.SetActive(false);
			}

			//otherwise, diable the button itself
			else
			{
				GetComponent<Button>().interactable = false;
			}
		}
	}
}
