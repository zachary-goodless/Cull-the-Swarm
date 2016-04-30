
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using System;
using System.Collections;

public class MainMenuQuitConfirm : MonoBehaviour
{
	//PUBLIC
	public Button noButton;
	public Button yesButton;

	//PRIVATE
	private MainMenuEventHandler mMainMenu;

//--------------------------------------------------------------------------------------------

	void OnEnable()
	{
		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
		noButton.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleYesButtonClicked()
	{
		Invoke("deactivate", 0.2f);
		Application.Quit();
	}

//--------------------------------------------------------------------------------------------

	public void handleNoButtonClicked()
	{
		Invoke("deactivate", 0.2f);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}

//--------------------------------------------------------------------------------------------

	void deactivate(){ gameObject.SetActive(false); }
}

