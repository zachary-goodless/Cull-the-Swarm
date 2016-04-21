
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
		gameObject.SetActive(false);
		Application.Quit();
	}

//--------------------------------------------------------------------------------------------

	public void handleNoButtonClicked()
	{
		gameObject.SetActive(false);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}
}

