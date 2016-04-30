
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;

public class ConfirmationDialog : MonoBehaviour
{
	//PUBLIC
	public PauseMenu parent;

	public Button noButton;

	//PRIVATE
	private int callbackAction;

//--------------------------------------------------------------------------------------------

	public void displayDialog(int action)
	{
		gameObject.SetActive(true);
		callbackAction = action;

		noButton.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleYesButtonClicked()
	{
		gameObject.SetActive(false);
		switch(callbackAction)
		{
		case 0:			//main menu load callback
			parent.handleMainMenuButtonClickedHelper(true);
			break;
		case 1:			//world map menu load callback
			parent.handleLevelSelectButtonClickedHelper(true);
			break;
		case 2:			//quit game callback
			parent.handleQuitButtonClickedHelper(true);
			break;

		default:
			Debug.Log("UNHANDLED CALLBACK ACTION");
			break;
		}

		parent.lastButtonClicked.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleNoButtonClicked()
	{
		gameObject.SetActive(false);
		switch(callbackAction)
		{
		case 0:			//main menu load callback
			parent.handleMainMenuButtonClickedHelper(false);
			break;
		case 1:			//world map menu load callback
			parent.handleLevelSelectButtonClickedHelper(false);
			break;
		case 2:			//quit game callback
			parent.handleQuitButtonClickedHelper(false);
			break;

		default:
			Debug.Log("UNHANDLED CALLBACK ACTION");
			break;
		}

		parent.lastButtonClicked.Select();
	}
}
