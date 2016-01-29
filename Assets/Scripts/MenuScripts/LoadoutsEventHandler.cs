﻿
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LoadoutsEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;		//back to world map
	public Button mStartButton;		//start game

	//TODO -- array of loadout buttons, dynamic?

	//PRIVATE
	private Loadout mCurrentLoadout;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//if the current game ptr is somehow bad, return to the main menu
		if(SavedGameManager.getCurrentGame() == null)
		{
			Debug.Log("CURRENT GAME PTR NULL: RETURNING TO MAIN MENU");

			//TODO -- spawn error message, return to main menu
		}

		//create a new loadout object
		mCurrentLoadout = new Loadout();

		//TODO -- fill out array of loadout buttons using current game's avaialble loadouts
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//TODO -- cycle thru level buttons, highlight whichever is selected level?

		//continue button is disabled when there is no currently selected level
		mStartButton.interactable = mCurrentLoadout.isComplete();
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		//sanity check -- reset the selected level and loadout for the current game ptr
		SavedGame currentGame = SavedGameManager.getCurrentGame();
		if(currentGame != null)
		{
			currentGame.setSelectedLevel(SceneIndex.NULL);
			currentGame.setCurrentLoadout(null);
		}

		//load the worldmap scene
		Debug.Log("LOADING WORLD MAP");
		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
	}

//--------------------------------------------------------------------------------------------

	public void handleStartButtonClicked()
	{
		//set the loadout for the current game (if able)
		SavedGame currentGame = SavedGameManager.getCurrentGame();
		if(currentGame != null)
		{
			currentGame.setCurrentLoadout(mCurrentLoadout);

			//load the gameplay scene
			Debug.Log("LOADING GAMEPLAY SCENE: " + currentGame.getSelectedLevel());
			//SceneManager.LoadScene((int)currentGame.getSelectedLevel());
			//TODO -- above line temporarily commented out
		}

		//otherwise, return to the main menu, something is wrong
		else
		{
			Debug.Log("CURRENT GAME PTR NULL: CAN'T ENTER GAMEPLAY, RETURNING TO MAIN MENU");

			//TODO -- spawn error message, return to main menu
		}

		//TODO -- temporarily enter gameplay for prototype
	}
		
//--------------------------------------------------------------------------------------------

	//TODO -- handlers for loadout buttons (sets this object's loadout ptr data)
	//				loadout buttons as toggles
}
