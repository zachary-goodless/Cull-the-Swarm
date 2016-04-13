
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
	GAMEPLAY_4_2 = 16,
	GAMEPLAY_4_3 = 17
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

	public ScreenFade mScreenFader;
	public GameObject gmPrefab;

	//PRIVATE
	private SavedGameManager mSavedGameManager;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = SavedGameManager.createOrLoadSavedGameManager(gmPrefab).GetComponent<SavedGameManager>();
		StartCoroutine(mScreenFader.FadeFromBlack());
	}

//--------------------------------------------------------------------------------------------

	public void handleNewGameButtonClicked()
	{
		toggleButtons();
		mNewGamePanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleLoadGameButtonClicked()
	{
		toggleButtons();
		mLoadGamePanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleDeleteGameButtonClicked()
	{
		toggleButtons();
		mDeleteGamePanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleControlsButtonClicked()
	{
		toggleButtons();
		mControlsPanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleCreditsButtonClicked()
	{
		toggleButtons();
		mCreditsPanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleQuitButtonClicked()
	{
		//TODO
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
