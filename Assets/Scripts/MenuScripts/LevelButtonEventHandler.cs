
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LevelButtonEventHandler : MonoBehaviour
{
	//PUBLIC
	public SceneIndex sceneIndex;

	//PRIVATE
	private WorldMapEventHandler mParentEventHandler;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//get the worldmap canvas event handler
		mParentEventHandler = GetComponentInParent<WorldMapEventHandler>();
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonClicked()
	{
		if(mParentEventHandler != null)
		{
			//call to the canvas -- sets the selected level to this button's index
			mParentEventHandler.handleMapButtonClicked(sceneIndex);
		}
	}
}
