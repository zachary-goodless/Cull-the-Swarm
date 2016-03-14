
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LevelButtonEventHandler : MonoBehaviour
{
	//PUBLIC
	public int firstStageIndex;

	//PRIVATE
	private WorldMapEventHandler mParentEventHandler;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//get the worldmap canvas event handler
		mParentEventHandler = GetComponentInParent<WorldMapEventHandler>();
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonClick()
	{
		if(mParentEventHandler != null)
		{
			//call to the canvas' event handler
			mParentEventHandler.handleLevelButtonClicked(firstStageIndex);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonMouseOver()
	{
		if(gameObject.GetComponent<Button>().interactable)
		{
			if(mParentEventHandler != null)
			{
				//call to the canvas' event handler
				mParentEventHandler.handleLevelButtonMouseOver(firstStageIndex);
			}
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonMouseExit()
	{
		if(gameObject.GetComponent<Button>().interactable)
		{
			if(mParentEventHandler != null)
			{
				//call to the canvas' event handler
				mParentEventHandler.handleLevelButtonMouseExit();
			}
		}
	}
}
