
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LevelButtonEventHandler : MonoBehaviour, ISelectHandler
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

	public void OnSelect(BaseEventData eventData)
	{
		handleButtonMouseOver();
	}

	public void handleButtonMouseOver()
	{
		if(mParentEventHandler != null)
		{
			//call to the canvas' event handler
			mParentEventHandler.lastButtonClicked = GetComponent<Button>();
			mParentEventHandler.handleLevelButtonMouseOver(firstStageIndex);
		}
	}
}
