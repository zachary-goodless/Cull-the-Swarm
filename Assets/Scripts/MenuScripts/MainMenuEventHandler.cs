
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
	public Button mNewGameButton;		//new game
	public Button mLoadGameButton;		//load game
	public Button mDeleteGameButton;	//delete game

	public Button mStartButton;			//start game

	//PRIVATE
	private SavedGameManager mSavedGameManager;

	//

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();

		//TODO -- temp
		if(mSavedGameManager.getCurrentGame() == null)
		{
			mSavedGameManager.createNewGame("test");
		}
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//start button is disabled when game manager's current game ptr is null
		mStartButton.interactable = mSavedGameManager.getCurrentGame() != null;
	}

//--------------------------------------------------------------------------------------------

	public void handleNewGameButtonClicked()
	{
		//TODO -- create new game menu, disable this menu, etc
	}

//--------------------------------------------------------------------------------------------

	public void handleLoadGameButtonClicked()
	{
		//TODO -- create load game menu, disable this menu, etc
	}

//--------------------------------------------------------------------------------------------

	public void handleDeleteGameButtonClicked()
	{
		//TODO -- create delete game menu, disable this menu, etc
	}

//--------------------------------------------------------------------------------------------

	public void handleStartButtonClicked()
	{
		//load the worldmap scene
		Debug.Log("LOADING WORLD MAP");
		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
	}
}
