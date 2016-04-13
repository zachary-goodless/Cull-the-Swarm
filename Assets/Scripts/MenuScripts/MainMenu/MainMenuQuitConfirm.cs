
using UnityEngine;

using System;
using System.Collections;

public class MainMenuQuitConfirm : MonoBehaviour
{
	//PUBLIC
	//

	//PRIVATE
	private MainMenuEventHandler mMainMenu;

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
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
	}
}

