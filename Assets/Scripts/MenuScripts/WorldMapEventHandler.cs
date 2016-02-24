
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class WorldMapEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;
	public Button mContinueButton;

	public GameObject mLevelPanel;
	public GameObject mStagePanel;
	public GameObject mDataPanel;

	//PRIVATE
	private SavedGameManager mSavedGameManager;

	private SceneIndex mSelectedLevel;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();

		//if the current game ptr is somehow bad, return to the main menu
		if(mSavedGameManager.getCurrentGame() == null)
		{
			Debug.Log("CURRENT GAME PTR NULL: RETURNING TO MAIN MENU");
		}

		toggleLevelButtonsActive();

		//sanity check -- null any selected level data on the current game ptr
		mSavedGameManager.getCurrentGame().setSelectedLevel(SceneIndex.NULL);
		mSelectedLevel = SceneIndex.NULL;
	}

//--------------------------------------------------------------------------------------------

	//TODO -- delete this function when the mouse-over stuff is added and we load the next menu on stage button click
	void Update()
	{
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

	public void handleTutorialButtonClicked(){ 		handleLevelButtonClicked(0); }
	public void handleLevelOneButtonClicked(){ 		handleLevelButtonClicked(3); }
	public void handleLevelTwoButtonClicked(){ 		handleLevelButtonClicked(6); }
	public void handleLevelThreeButtonClicked(){ 	handleLevelButtonClicked(9); }
	public void handleLevelFourButtonClicked(){ 	handleLevelButtonClicked(12); }

//--------------------------------------------------------------------------------------------

	private void handleLevelButtonClicked(int firstStageIndex)
	{
		//activate the stage and data panels, toggle level buttons
		mStagePanel.SetActive(true);
		mDataPanel.SetActive(true);

		toggleLevelButtonsActive();

		//for the first three buttons (stages 1, 2, and 3)...
		LevelButtonEventHandler[] behs = mStagePanel.GetComponentsInChildren<LevelButtonEventHandler>();
		for(int i = 0; i < 3; ++i)
		{
			//set isUnlocked and sceneIndex for the current button
			behs[i].isUnlocked = mSavedGameManager.getCurrentGame().unlockedLevels[firstStageIndex + i];
			behs[i].sceneIndex = (SceneIndex)firstStageIndex + i + 3;
		}

		//force the unlock for the back button (button 4)
		behs[3].isUnlocked = true;

		//set the buttons enable
		setStageButtonsActive();

		initDataPanel();
	}

//--------------------------------------------------------------------------------------------

	public void handleMapButtonClicked(SceneIndex si)
	{
		if(si != SceneIndex.NULL)
		{
			//save the incoming scene index
			mSelectedLevel = si;
			Debug.Log("NEW SELECTED LEVEL: " + mSelectedLevel);

			//reassert button enabled -- button just clicked is disabled in its own handler after
			setStageButtonsActive();

			initDataPanel();	//TODO -- move this to stage button mouse-over
			//TODO -- automatically load into loadout select menu, add once mouse-over is working
		}

		//back button clicked...
		else
		{
			//deactivate the stage and data panels, toggle level buttons
			mStagePanel.SetActive(false);
			mDataPanel.SetActive(false);

			toggleLevelButtonsActive();
		}
	}

//--------------------------------------------------------------------------------------------

	void initDataPanel()
	{
		//using the selected level...
		switch(mSelectedLevel)
		{
		case SceneIndex.NULL:
			
			//TODO -- restore default image

			//restore default high score text
			foreach(Text t in mDataPanel.GetComponentsInChildren<Text>())
			{
				if(t.gameObject.name == "HighScorePersonal")
				{
					t.text = "-";
				}
				else if(t.gameObject.name == "HighScoreGlobal")
				{
					t.text = "-";
				}
			}

			break;

		case SceneIndex.GAMEPLAY_TUTORIAL_1:
		case SceneIndex.GAMEPLAY_TUTORIAL_2:
		case SceneIndex.GAMEPLAY_TUTORIAL_3:
		case SceneIndex.GAMEPLAY_1_1:
		case SceneIndex.GAMEPLAY_1_2:
		case SceneIndex.GAMEPLAY_1_3:
		case SceneIndex.GAMEPLAY_2_1:
		case SceneIndex.GAMEPLAY_2_2:
		case SceneIndex.GAMEPLAY_2_3:
		case SceneIndex.GAMEPLAY_3_1:
		case SceneIndex.GAMEPLAY_3_2:
		case SceneIndex.GAMEPLAY_3_3:
		case SceneIndex.GAMEPLAY_4_1:
		case SceneIndex.GAMEPLAY_4_2:
		case SceneIndex.GAMEPLAY_4_3:

			//TODO -- set data panel image

			//set data panel high scores
			int i = (int)mSelectedLevel - 3;
			foreach(Text t in mDataPanel.GetComponentsInChildren<Text>())
			{
				if(t.gameObject.name == "HighScorePersonal")
				{
					t.text = (mSavedGameManager.getCurrentGame().highScores[i]).ToString();
				}
				else if(t.gameObject.name == "HighScoreGlobal")
				{
					t.text = (mSavedGameManager.globalHighScores[i]).ToString();
				}
			}

			break;

		default:
			break;
		}
	}

//--------------------------------------------------------------------------------------------

	void setStageButtonsActive()
	{
		//for each button on the stage panel...
		foreach(Button b in mStagePanel.GetComponentsInChildren<Button>())
		{
			LevelButtonEventHandler beh = b.gameObject.GetComponent<LevelButtonEventHandler>();

			//if the current button's scene index is not null (not a back button)...
			if(beh.sceneIndex != SceneIndex.NULL)
			{
				//the current button is interactable if it's level is unlocked and it's not already selected
				b.interactable = mSelectedLevel == beh.sceneIndex ? false : beh.isUnlocked;
			}
		}
	}

//--------------------------------------------------------------------------------------------

	void toggleLevelButtonsActive()
	{
		bool[] unlocks = mSavedGameManager.getCurrentGame().unlockedLevels;
		int i = 0;

		//for each button in the main level panel...
		foreach(Button b in mLevelPanel.GetComponentsInChildren<Button>())
		{
			//if the first stage for that level is unlocked... toggle the button
			b.interactable = unlocks[i] ? !b.interactable : false;
			i += 3;
		}
	}

//--------------------------------------------------------------------------------------------

	//TODO -- mouse over events for level buttons (updates large panel with picture / story info)
	//TODO -- mouse over events for stage buttons (updates data panel high score stuff)
}
