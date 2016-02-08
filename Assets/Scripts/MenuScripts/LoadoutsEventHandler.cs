
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LoadoutsEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;			//back to world map
	public Button mStartButton;			//start game

	public GameObject mMainPanel;		//ui element panels
	public GameObject mChasisPanel;
	public GameObject mPrimaryPanel;
	public GameObject mSecondaryPanel;

	//PRIVATE
	private SavedGameManager mSavedGameManager;

	private Loadout mCurrentLoadout;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();

		//if the current game ptr is somehow bad, return to the main menu
		if(mSavedGameManager.getCurrentGame() == null)
		{
			Debug.Log("CURRENT GAME PTR NULL: RETURNING TO MAIN MENU");
		}

		//set all loadout element buttons' isUnlocked using the saved game data
		//	back buttons' interactable must be set via the inspector view 
		setButtonsUnlock(
			mChasisPanel.GetComponentsInChildren<LoadoutElementButtonEventHandler>(), 
			mSavedGameManager.getCurrentGame().unlockedChasis);
		setButtonsUnlock(
			mPrimaryPanel.GetComponentsInChildren<LoadoutElementButtonEventHandler>(), 
			mSavedGameManager.getCurrentGame().unlockedPrimary);
		setButtonsUnlock(
			mSecondaryPanel.GetComponentsInChildren<LoadoutElementButtonEventHandler>(), 
			mSavedGameManager.getCurrentGame().unlockedSecondary);

		//assert buttons' enabled now that their unlock status is set
		setButtonsEnable(mChasisPanel.GetComponentsInChildren<Button>());
		setButtonsEnable(mPrimaryPanel.GetComponentsInChildren<Button>());
		setButtonsEnable(mSecondaryPanel.GetComponentsInChildren<Button>());

		//sanity check -- null any current loadout data on the current game ptr
		mSavedGameManager.getCurrentGame().setCurrentLoadout(null);
		mCurrentLoadout = new Loadout();

		initDefaultLoadout();	//initialze the default loadout
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//continue button is disabled when there is no currently selected level
		mStartButton.interactable = mCurrentLoadout.isComplete();
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
		Debug.Log("CURRENT LOADOUT: " + mCurrentLoadout.toString());
		Debug.Log("LOADING GAMEPLAY SCENE: " + currentGame.getSelectedLevel());
		SceneManager.LoadScene((int)currentGame.getSelectedLevel());
	}
		
//--------------------------------------------------------------------------------------------

	public void handleChasisButtonClicked()
	{
		mChasisPanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handlePrimaryButtonClicked()
	{
		mPrimaryPanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleSecondaryButtonClicked()
	{
		mSecondaryPanel.SetActive(true);
		mMainPanel.SetActive(false);
	}

//--------------------------------------------------------------------------------------------

	public void handleLoadoutElementButtonClicked(
		Loadout.LoadoutChasis lc,
		Loadout.LoadoutPrimary lp,
		Loadout.LoadoutSecondary ls)
	{
		//update current chasis if that's what was passed in
		if(lc != Loadout.LoadoutChasis.NULL)
		{
			mCurrentLoadout.setChasis(lc);
			setButtonsEnable(mChasisPanel.GetComponentsInChildren<Button>());

			Debug.Log("NEW SELECTED CHASIS: " + lc);
		}

		//update current primary if that's what was passed in
		else if(lp != Loadout.LoadoutPrimary.NULL)
		{
			mCurrentLoadout.setPrimary(lp);
			setButtonsEnable(mPrimaryPanel.GetComponentsInChildren<Button>());

			Debug.Log("NEW SELECTED PRIMARY: " + lp);
		}

		//update current secondary if that's what was passed in
		else if(ls != Loadout.LoadoutSecondary.NULL)
		{
			mCurrentLoadout.setSecondary(ls);
			setButtonsEnable(mSecondaryPanel.GetComponentsInChildren<Button>());

			Debug.Log("NEW SELECTED SECONDARY: " + ls);
		}

		//else back button, reenable the main panel
		else
		{
			mMainPanel.SetActive(true);
		}
	}

//--------------------------------------------------------------------------------------------

	void setButtonsUnlock(LoadoutElementButtonEventHandler[] behs, bool[] unlocks)
	{
		//for each element in the unlocks array...
		for(int i = 0; i < unlocks.Length; ++i)
		{
			behs[i].isUnlocked = unlocks[i];
		}
	}

//--------------------------------------------------------------------------------------------

	void setButtonsEnable(Button[] buttons)
	{
		//asserts button enabled based on whether or not it has been unlocked on the current game
		foreach(Button b in buttons)
		{
			b.interactable = b.gameObject.GetComponent<LoadoutElementButtonEventHandler>().isUnlocked;
		}
	}

//--------------------------------------------------------------------------------------------

	void initDefaultLoadout()
	{
		//set the current loadout to the default values
		mCurrentLoadout.setChasis(Loadout.LoadoutChasis.DEFAULT);
		mCurrentLoadout.setPrimary(Loadout.LoadoutPrimary.DEFAULT);
		mCurrentLoadout.setSecondary(Loadout.LoadoutSecondary.DEFAULT);

		//force the default buttons to disabled
		Button[] buttons = mChasisPanel.GetComponentsInChildren<Button>();
		buttons[0].interactable = false;

		buttons = mPrimaryPanel.GetComponentsInChildren<Button>();
		buttons[0].interactable = false;

		buttons = mSecondaryPanel.GetComponentsInChildren<Button>();
		buttons[0].interactable = false;
	}
}
