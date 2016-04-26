
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	//PUBLIC
	public ConfirmationDialog confirmationDialog;

	public Button resumeButton;
	public Button mainMenuButton;
	public Button levelSelectButton;
	public Button quitButton;

	public Button lastButtonClicked;

	//PRIVATE
	//

//--------------------------------------------------------------------------------------------

	public void togglePaused()
	{
		//if time scale is 0 (paused), unpause
		if(Time.timeScale == 0f)
		{
			unpauseGame();
		}

		//else game is unpaused, pause
		else
		{
			pauseGame();
		}
	}

//--------------------------------------------------------------------------------------------

	void pauseGame()
	{
		if(!LevelCompleteHandler.isLevelComplete)
		{
			Time.timeScale = 0f;
			gameObject.SetActive(true);

			resumeButton.Select();
		}
	}

//--------------------------------------------------------------------------------------------

	void unpauseGame()
	{
		Time.timeScale = 1f;
		gameObject.SetActive(false);

		mainMenuButton.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleResumeButtonClicked()
	{
		unpauseGame();
		lastButtonClicked = resumeButton;
	}

//--------------------------------------------------------------------------------------------

	public void handleMainMenuButtonClicked()
	{
		lastButtonClicked = mainMenuButton;

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
		lastButtonClicked = levelSelectButton;

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
		lastButtonClicked = quitButton;

		toggleButtonsEnable();
		confirmationDialog.displayDialog(2);
	}
	public void handleQuitButtonClickedHelper(bool confirmation)
	{
		if(confirmation)
		{
			unpauseGame();
			Application.Quit();
		}

		toggleButtonsEnable();
	}

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
