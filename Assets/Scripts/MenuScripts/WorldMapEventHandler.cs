
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class WorldMapEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;			//back to main menu
	public Button mContinueButton;		//continue to loadouts

	//PRIVATE
	private SavedGameManager mSavedGameManager;

	private Dictionary<LevelButtonEventHandler, Button> mLevelButtonMap = new Dictionary<LevelButtonEventHandler, Button>();

	private SceneIndex mSelectedLevel;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();

		//if the current game ptr is somehow bad, return to the main menu
		if(mSavedGameManager.getCurrentGame() == null)
		{
			Debug.Log("CURRENT GAME PTR NULL: RETURNING TO MAIN MENU");

			//TODO -- spawn error message, return to main menu
		}

		//populate map of level buttons
		LevelButtonEventHandler[] levelButtons = GetComponentsInChildren<LevelButtonEventHandler>();
		foreach(LevelButtonEventHandler handler in levelButtons)
		{
			Button button = handler.gameObject.GetComponent<Button>();
			if(button != null)
			{
				mLevelButtonMap.Add(handler, button);
			}
		}

		//TODO -- init high scores using the game manager and current game ptr
		//TODO -- enable / disable buttons based on current game ptr level completion

		//sanity check -- null any selected level data on the current game ptr
		mSavedGameManager.getCurrentGame().setSelectedLevel(SceneIndex.NULL);
		mSelectedLevel = SceneIndex.NULL;
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//for each level button...
		foreach(KeyValuePair<LevelButtonEventHandler, Button> pair in mLevelButtonMap)
		{
			//if the button is active...
			Button button = pair.Value;
			if(button.interactable)
			{
				//highlight the button if its scene index matches the selected level index
				LevelButtonEventHandler handler = pair.Key;
				if(handler.sceneIndex == mSelectedLevel)
				{
					//TODO -- highlight button
				}

				//otherwise, maintain some default color
				else
				{
					//TODO -- restore button to its normal color
				}
			}
		}

		//continue button is disabled when there is no currently selected level
		mContinueButton.interactable = mSelectedLevel != SceneIndex.NULL;
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		//load the main menu scene
		Debug.Log("LOADING MAIN MENU");
		SceneManager.LoadScene((int)SceneIndex.MAIN_MENU);
	}

//--------------------------------------------------------------------------------------------

	public void handleContinueButtonClicked()
	{
		//set the current game's selected level
		mSavedGameManager.getCurrentGame().setSelectedLevel(mSelectedLevel);

		//load the loadouts scene
		Debug.Log("LOADING LOADOUTS MENU");
		SceneManager.LoadScene((int)SceneIndex.LOADOUTS);
	}

//--------------------------------------------------------------------------------------------

	public void handleMapButtonClicked(SceneIndex si)
	{
		//save the incoming scene index
		mSelectedLevel = si;
		Debug.Log("NEW SELECTED LEVEL: " + mSelectedLevel);
	}
}
