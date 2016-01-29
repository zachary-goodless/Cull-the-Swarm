
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

//ASSIGNS GLOBAL INDEXING FOR SCENES
public enum SceneIndex
{
	NULL = -1,			//null val

	MAIN_MENU = 0,		//menu scenes
	WORLD_MAP = 1,
	LOADOUTS = 2,

	//
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

		//TODO -- temp change this to point to the gameplay level for prototype?
	}
}
