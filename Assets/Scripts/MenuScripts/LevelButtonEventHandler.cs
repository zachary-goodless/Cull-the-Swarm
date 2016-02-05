
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LevelButtonEventHandler : MonoBehaviour
{
	//PUBLIC
	public SceneIndex sceneIndex;

	public bool isUnlocked;

	//PRIVATE
	private WorldMapEventHandler mParentEventHandler;
	private GameObject mParentPanel;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//get the worldmap canvas event handler
		mParentEventHandler = GetComponentInParent<WorldMapEventHandler>();
		mParentPanel = transform.parent.gameObject;
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonClicked()
	{
		if(mParentEventHandler != null)
		{
			//call to the canvas -- sets the selected level to this button's index
			mParentEventHandler.handleMapButtonClicked(sceneIndex);

			//if this is a back button, return to the main panel
			if(sceneIndex == SceneIndex.NULL)
			{
				mParentPanel.SetActive(false);
			}

			//otherwise, this is a stage-select button -- disable it
			else
			{
				GetComponent<Button>().interactable = false;
			}
		}
	}
}
