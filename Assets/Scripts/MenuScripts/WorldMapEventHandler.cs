
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

	public GameObject mMainPanel;
	public GameObject mTutorialPanel;
	public GameObject mLevelOnePanel;
	public GameObject mLevelTwoPanel;
	public GameObject mLevelThreePanel;
	public GameObject mLevelFourPanel;

	//PRIVATE
	private SavedGameManager mSavedGameManager;
	private List<GameObject> mPanelsList;

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

		//populate list of level select panels panels (cleaner code)
		mPanelsList = new List<GameObject>();
		mPanelsList.Add(mTutorialPanel);
		mPanelsList.Add(mLevelOnePanel);
		mPanelsList.Add(mLevelTwoPanel);
		mPanelsList.Add(mLevelThreePanel);
		mPanelsList.Add(mLevelFourPanel);

		//some extra stuff for the coming foreach loop...
		bool[] unlocks = mSavedGameManager.getCurrentGame().levelCompletion;
		Button[] buttons = mMainPanel.GetComponentsInChildren<Button>();
		int i = 0;

		//for each level select panel...
		foreach(GameObject panel in mPanelsList)
		{
			//set the buttons' unlocks and the corresponding main panel's button unlock
			setButtonsUnlock(i, i + 3, panel.GetComponentsInChildren<LevelButtonEventHandler>(), unlocks);
			buttons[i / 3].interactable = unlocks[i];

			//set the buttons' enabled status now that their unlock status is set
			setButtonsEnable(panel.GetComponentsInChildren<Button>());

			//set the global and personal high scores
			setHighScores(i, i + 3, panel);

			i += 3;
		}

		//sanity check -- null any selected level data on the current game ptr
		mSavedGameManager.getCurrentGame().setSelectedLevel(SceneIndex.NULL);
		mSelectedLevel = SceneIndex.NULL;
	}

//--------------------------------------------------------------------------------------------

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

	public void handleTutorialButtonClicked()
	{
		mTutorialPanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleLevelOneButtonClicked()
	{
		mLevelOnePanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleLevelTwoButtonClicked()
	{
		mLevelTwoPanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleLevelThreeButtonClicked()
	{
		mLevelThreePanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleLevelFourButtonClicked()
	{
		mLevelFourPanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleMapButtonClicked(SceneIndex si)
	{
		if(si != SceneIndex.NULL)
		{
			//save the incoming scene index
			mSelectedLevel = si;
			Debug.Log("NEW SELECTED LEVEL: " + mSelectedLevel);

			//for each level select panel...
			foreach(GameObject panel in mPanelsList)
			{
				//reassert button enabled -- button just clicked is disabled in its own handler after
				setButtonsEnable(panel.GetComponentsInChildren<Button>());
			}
		}
		else
		{
			mMainPanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	private void setButtonsUnlock(int start, int end, LevelButtonEventHandler[] behs, bool[] unlocks)
	{
		//for each group in the unlocks array...
		for(int i = start; i < end; ++i)
		{
			//set the isUnlocked on the corresponding button
			behs[i - start].isUnlocked = unlocks[i];
		}
	}

//--------------------------------------------------------------------------------------------

	void setButtonsEnable(Button[] buttons)
	{
		//asserts button enabled based on whether or not it has been unlocked on the current game
		foreach(Button b in buttons)
		{
			b.interactable = b.gameObject.GetComponent<LevelButtonEventHandler>().isUnlocked;
		}
	}

//--------------------------------------------------------------------------------------------

	private void setHighScores(int start, int end, GameObject panel)
	{
		//lists of the labels that will display our high scores
		List<Text> personalLabels = new List<Text>();
		List<Text> globalLabels = new List<Text>();

		//for each text component on this panel...
		Text[] texts = panel.GetComponentsInChildren<Text>();
		foreach(Text t in texts)
		{
			//add it to one of the lists if it has an appropriate tag
			if(t.gameObject.tag == "PersonalHighScoreLabel") personalLabels.Add(t);
			else if(t.gameObject.tag == "GlobalHighScoreLabel") globalLabels.Add(t);
		}

		//for each high score stored in this block...
		for(int i = start; i < end; ++i)
		{
			//set the high score labels' text
			personalLabels[i - start].text = mSavedGameManager.getCurrentGame().highScores[i].ToString();
			globalLabels[i - start].text = mSavedGameManager.globalHighScores[i].ToString();
		}
	}
}
