
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

	//PRIVATE
	private SavedGameManager mSavedGameManager;

	//TODO -- map of loadout buttons

	private Loadout mCurrentLoadout;

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

		//TODO -- enable / disable loadout buttons using current game ptr's avaialble loadouts

		//sanity check -- null any current loadout data on the current game ptr
		mSavedGameManager.getCurrentGame().setCurrentLoadout(null);
		mCurrentLoadout = new Loadout();
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//TODO -- cycle thru loadout button map

		//continue button is disabled when there is no currently selected level
		//mStartButton.interactable = mCurrentLoadout.isComplete();	//TODO -- temp comment out
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		//load the worldmap scene
		Debug.Log("LOADING WORLD MAP");
		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
	}

//--------------------------------------------------------------------------------------------

	public void handleStartButtonClicked()
	{
		//set the current game ptr's loadout object
		SavedGame currentGame = mSavedGameManager.getCurrentGame();
		currentGame.setCurrentLoadout(mCurrentLoadout);

		//load the gameplay scene
		Debug.Log("LOADING GAMEPLAY SCENE: " + currentGame.getSelectedLevel());
		SceneManager.LoadScene((int)currentGame.getSelectedLevel());
	}
		
//--------------------------------------------------------------------------------------------

	//TODO -- handler for loadout buttons
}
