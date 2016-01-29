
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class WorldMapEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;			//back to main menu
	public Button mContinueButton;		//continue to loadouts

	//TODO -- array of level buttons

	//PRIVATE
	private SceneIndex mSelectedLevel;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//init the selected level to null
		//mSelectedLevel = SceneIndex.NULL;
		//TODO -- above line temporarily commented out

		//if the current game ptr is somehow bad, return to the main menu
		if(SavedGameManager.getCurrentGame() == null)
		{
			Debug.Log("CURRENT GAME PTR NULL: RETURNING TO MAIN MENU");

			//TODO -- spawn error message, return to main menu
		}

		//TODO -- init high scores using the game manager and current game ptr
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//TODO -- cycle thru level buttons, highlight whichever is selected level?

		//continue button is disabled when there is no currently selected level
		mContinueButton.interactable = mSelectedLevel != SceneIndex.NULL;
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		//sanity check -- reset the current game ptr's selected level
		SavedGame currentGame = SavedGameManager.getCurrentGame();
		if(currentGame != null)
		{
			currentGame.setSelectedLevel(SceneIndex.NULL);
		}

		//load the main menu scene
		Debug.Log("LOADING MAIN MENU");
		SceneManager.LoadScene((int)SceneIndex.MAIN_MENU);
	}

//--------------------------------------------------------------------------------------------

	public void handleContinueButtonClicked()
	{
		//set the current game's selected level
		SavedGame currentGame = SavedGameManager.getCurrentGame();
		if(currentGame != null)
		{
			currentGame.setSelectedLevel(mSelectedLevel);
		}

		//load the loadouts scene
		Debug.Log("LOADING LOADOUTS MENU");
		SceneManager.LoadScene((int)SceneIndex.LOADOUTS);
	}

//--------------------------------------------------------------------------------------------

	//TODO -- handlers for level buttons (sets this object's selected level)
	//				level buttons as toggles
}
