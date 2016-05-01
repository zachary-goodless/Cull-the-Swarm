
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

//ASSIGNS GLOBAL INDEXING FOR SCENES
public enum SceneIndex
{
	NULL = -1,					//null val

	MAIN_MENU = 0,				//menu scenes
	WORLD_MAP = 1,
	LOADOUTS = 2,

	GAMEPLAY_TUTORIAL_1 = 3,	//tutorial scenes
	GAMEPLAY_TUTORIAL_2 = 4,
	GAMEPLAY_TUTORIAL_3 = 5,

	GAMEPLAY_1_1 = 6,			//level 1 stages
	GAMEPLAY_1_2 = 7,
	GAMEPLAY_1_3 = 8,

	GAMEPLAY_2_1 = 9,			//level 2 stages
	GAMEPLAY_2_2 = 10,
	GAMEPLAY_2_3 = 11,

	GAMEPLAY_3_1 = 12,			//level 3 stages
	GAMEPLAY_3_2 = 13,
	GAMEPLAY_3_3 = 14,

	GAMEPLAY_4_1 = 15,			//level 4 stages
	GAMEPLAY_4_2 = 16
}

//============================================================================================

public class MainMenuEventHandler : MonoBehaviour
{
	//PUBLIC
	public GameObject mMainPanel;		//menu panels

	public GameObject mNewGamePanel;
	public GameObject mLoadGamePanel;
	public GameObject mDeleteGamePanel;

	public GameObject mControlsPanel;
	public GameObject mCreditsPanel;

	public GameObject quitConfirmation;

	public ScreenFade mScreenFader;
	public GameObject gmPrefab;

	public Button newButton;
	public Button loadButton;
	public Button deleteButton;
	public Button controlsButton;
	public Button creditsButton;
	public Button quitButton;

	public Button lastButtonClicked;

	//PRIVATE
	private SavedGameManager mSavedGameManager;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = SavedGameManager.createOrLoadSavedGameManager(gmPrefab).GetComponent<SavedGameManager>();
		StartCoroutine(mScreenFader.FadeFromBlack());

		lastButtonClicked = null;
	}

//--------------------------------------------------------------------------------------------

	public void handleNewGameButtonClicked()
	{
		lastButtonClicked = newButton;

		if(!mNewGamePanel.activeSelf)
		{
			toggleButtons();
			mNewGamePanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleLoadGameButtonClicked()
	{
		lastButtonClicked = loadButton;

		if(!mLoadGamePanel.activeSelf)
		{
			toggleButtons();
			mLoadGamePanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleDeleteGameButtonClicked()
	{
		lastButtonClicked = deleteButton;

		if(!mDeleteGamePanel.activeSelf)
		{
			toggleButtons();
			mDeleteGamePanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleControlsButtonClicked()
	{
		lastButtonClicked = controlsButton;

		if(!mControlsPanel.activeSelf)
		{
			toggleButtons();
			mControlsPanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleCreditsButtonClicked()
	{
		lastButtonClicked = creditsButton;

		if(!mCreditsPanel.activeSelf)
		{
			toggleButtons();
			mCreditsPanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleQuitButtonClicked()
	{
		lastButtonClicked = quitButton;

		if(!quitConfirmation.activeSelf)
		{
			toggleButtons();
			quitConfirmation.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	public void toggleButtons()
	{
		//for each button on the main panel...
		foreach(Button b in mMainPanel.GetComponentsInChildren<Button>())
		{
			//toggle its enabled state
			b.interactable = !b.interactable;
		}
	}
}
