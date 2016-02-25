
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

using UnityEditor;

public class WorldMapEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;
	public Button mContinueButton;

	public Image mStagePanelTitle;			//quick refs to dynamic image elements
	public Image mDataPanelImg;

	public GameObject mLevelPanel;
	public GameObject mStagePanel;
	public GameObject mDataPanel;

	//PRIVATE
	private Sprite[] levelTitleSprites;		//arrays to store sprites to dynamically update gui elements
	private Sprite[] levelButtonSprites;
	private Sprite[] levelImgSprites;
	private Sprite[] stageButtonSprites;

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

		//init the sprite arrays
		levelTitleSprites = Resources.LoadAll<Sprite>("GUI_Assets/LevelTitles");
		levelButtonSprites = Resources.LoadAll<Sprite>("GUI_Assets/LevelButtonIcons");
		levelImgSprites = Resources.LoadAll<Sprite>("GUI_Assets/WorldIcons");
		stageButtonSprites = Resources.LoadAll<Sprite>("GUI_Assets/StageButtonIcons");

		//set the interactability of the main level buttons
		lockLevelButtons();
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

	public void handleTutorialButtonClicked(){ 		initStagePanel(0); }
	public void handleLevelOneButtonClicked(){ 		initStagePanel(3); }
	public void handleLevelTwoButtonClicked(){ 		initStagePanel(6); }
	public void handleLevelThreeButtonClicked(){ 	initStagePanel(9); }
	public void handleLevelFourButtonClicked(){ 	initStagePanel(12); }

//--------------------------------------------------------------------------------------------

	private void initStagePanel(int firstStageIndex)
	{
		//for the first three buttons (stages 1, 2, and 3)...
		LevelButtonEventHandler[] behs = mStagePanel.GetComponentsInChildren<LevelButtonEventHandler>();
		for(int i = 0; i < 3; ++i)
		{
			//set isUnlocked and sceneIndex for the current button
			behs[i].isUnlocked = mSavedGameManager.getCurrentGame().unlockedLevels[firstStageIndex + i];
			behs[i].sceneIndex = (SceneIndex)(firstStageIndex + i + 3);
		}

		//set the stage panel's title image based on the firstStageIndex
		mStagePanelTitle.sprite = levelTitleSprites[firstStageIndex / 3];

		//force the unlock for the back button (button 4)
		behs[3].isUnlocked = true;

		//set the buttons enable
		setStageButtonsActive();

		initDataPanel();

		//activate the stage and data panels, toggle level buttons
		mStagePanel.SetActive(true);
		mDataPanel.SetActive(true);

		toggleLevelButtonsActive();
	}

//--------------------------------------------------------------------------------------------

	public void handleStageButtonClicked(SceneIndex si)
	{
		if(si != SceneIndex.NULL)
		{
			//save the incoming scene index
			mSelectedLevel = si;
			Debug.Log("NEW SELECTED LEVEL: " + mSelectedLevel);

			//reassert button enabled -- button just clicked is disabled in its own handler after
			setStageButtonsActive();

			initDataPanel();	//TODO -- move this to stage button mouse-over

			//automatically start the loadouts menu
			//handleContinueButtonClicked();	//TODO -- temp commented out
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
		switch(mSelectedLevel)
		{
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

			//set data panel image
			int i = ((int)mSelectedLevel - 3) / 3;
			mDataPanelImg.sprite = levelImgSprites[i];

			//set data panel high scores
			i = (int)mSelectedLevel - 3;
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

			//set the default level image (TODO -- CLASSIFIED IMAGE)
			mDataPanelImg.sprite = levelImgSprites[levelImgSprites.Length - 1];

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
		}
	}

//--------------------------------------------------------------------------------------------

	void setStageButtonsActive()
	{
		//for each button on the stage panel...
		int spriteIndex = 0;
		foreach(Button b in mStagePanel.GetComponentsInChildren<Button>())
		{
			LevelButtonEventHandler beh = b.gameObject.GetComponent<LevelButtonEventHandler>();

			//if the current button's level is not null (not a back button)...
			if(beh.sceneIndex != SceneIndex.NULL)
			{
				//if the current button's level is unlocked...
				if(beh.isUnlocked)
				{
					//set its sprite images based on what level it is (special case for final boss level)
					switch(beh.sceneIndex)
					{
					case SceneIndex.GAMEPLAY_4_3:
						setStageButtonSprites(b, 16);
						break;

					default:
						setStageButtonSprites(b, spriteIndex);
						break;
					}

					//button is interactable if its level is not the current level
					b.interactable = mSelectedLevel != beh.sceneIndex;
				}

				//else the current button's level is locked...
				if(!beh.isUnlocked)
				{
					//set its sprite image to CLASSIFIED and disable it
					b.gameObject.GetComponent<Image>().sprite = stageButtonSprites[stageButtonSprites.Length - 1];
					b.transition = Button.Transition.None;
					b.interactable = false;
				}
			}

			spriteIndex += 4;	//each button group is offset by 4
		}
	}

//--------------------------------------------------------------------------------------------

	void setStageButtonSprites(Button b, int spriteIndex)
	{
		SpriteState ss = new SpriteState();

		//set the default state
		b.gameObject.GetComponent<Image>().sprite = stageButtonSprites[spriteIndex];

		//set the different sprite states
		ss.pressedSprite = stageButtonSprites[spriteIndex + 1];
		ss.disabledSprite = stageButtonSprites[spriteIndex + 2];
		ss.highlightedSprite = stageButtonSprites[spriteIndex + 3];

		//apply the change to the button
		b.transition = Button.Transition.SpriteSwap;
		b.spriteState = ss;
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
			if(unlocks[i])
			{
				b.interactable = !b.interactable;
			}

			i += 3;
		}
	}

//--------------------------------------------------------------------------------------------

	void lockLevelButtons()
	{
		bool[] unlocks = mSavedGameManager.getCurrentGame().unlockedLevels;
		int i = 0;

		//for each button in the main level panel...
		foreach(Button b in mLevelPanel.GetComponentsInChildren<Button>())
		{
			//if the first stage for that level is locked...
			if(!unlocks[i])
			{
				//set the button's sprite to CLASSIFIED
				b.gameObject.GetComponent<Image>().sprite = levelButtonSprites[levelButtonSprites.Length - 1];
				b.transition = Button.Transition.None;
			}

			b.interactable = false;

			i += 3;
		}
	}

//--------------------------------------------------------------------------------------------

	//TODO -- mouse over events for level buttons (updates large panel with picture / story info)
	//TODO -- mouse over events for stage buttons (updates data panel high score stuff)
}
