
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	//PUBLIC
	public ConfirmationDialog confirmationDialog;

	//PRIVATE
	bool isPaused = false;

//--------------------------------------------------------------------------------------------

	public void pauseGame()
	{
		Time.timeScale = 0f;
		gameObject.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void unpauseGame()
	{
		Time.timeScale = 1f;
		gameObject.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleResumeButtonClicked()
	{
		unpauseGame();
	}

//--------------------------------------------------------------------------------------------

	public void handleMainMenuButtonClicked()
	{
		toggleButtonsEnable();
		confirmationDialog.displayDialog(0);
	}
	public void handleMainMenuButtonClickedHelper(bool confirmation)
	{
		if(confirmation)
		{
			unpauseGame();
			SceneManager.LoadScene((int)SceneIndex.MAIN_MENU);
		}

		toggleButtonsEnable();
	}

//--------------------------------------------------------------------------------------------

	public void handleLevelSelectButtonClicked()
	{
		toggleButtonsEnable();
		confirmationDialog.displayDialog(1);
	}
	public void handleLevelSelectButtonClickedHelper(bool confirmation)
	{
		if(confirmation)
		{
			unpauseGame();
			SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
		}

		toggleButtonsEnable();
	}

//--------------------------------------------------------------------------------------------

	public void handleQuitButtonClicked()
	{
		toggleButtonsEnable();
		confirmationDialog.displayDialog(2);
	}
	public void handleQuitButtonClickedHelper(bool confirmation)
	{
		if(confirmation)
		{
			unpauseGame();
			Application.Quit();	//TODO -- does this work?
		}

		toggleButtonsEnable();
	}

//--------------------------------------------------------------------------------------------

	//TODO -- settings button?

//--------------------------------------------------------------------------------------------

	private void toggleButtonsEnable()
	{
		//for each button in the pause menu...
		foreach(Button b in GetComponentsInChildren<Button>())
		{
			//toggle its interactable property
			b.interactable = !b.interactable;
		}
	}
}
