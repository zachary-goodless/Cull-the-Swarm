
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LoadoutsEventHandler : MonoBehaviour
{
	//PUBLIC
	public Button mBackButton;
	public Button mStartButton;

	public GameObject mMainPanel;
	public GameObject mChoicePanel;
	public GameObject mDataPanel;

	public ScreenFade mScreenFader;
	public GameObject gmPrefab;

	//PRIVATE
	//TODO -- sprite arrays (chassis buttons, primary buttons, secondary buttons, choice images)
	//TODO -- string arrays (loadout choice names, loadout choice descriptions, loadout choice unlocks)

	private SavedGameManager mSavedGameManager;

	private Loadout mCurrentLoadout;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = SavedGameManager.createOrLoadSavedGameManager(gmPrefab).GetComponent<SavedGameManager>();

		//if the current game ptr is somehow bad, return to the main menu
		if(mSavedGameManager.getCurrentGame() == null)
		{
			Debug.Log("ERROR: CURRENT GAME PTR NULL -- LOADING MAIN MENU");
			SceneManager.LoadScene((int)SceneIndex.MAIN_MENU);
			return;
		}

		//TODO -- init arrays

		//sanity check -- null any current loadout data on the current game ptr
		mSavedGameManager.getCurrentGame().setCurrentLoadout(null);
		mCurrentLoadout = new Loadout();

		//initialize the default loadout
		mCurrentLoadout.setChasis(Loadout.LoadoutChasis.EXTERMINATOR);
		mCurrentLoadout.setPrimary(Loadout.LoadoutPrimary.REPEL);
		mCurrentLoadout.setSecondary(Loadout.LoadoutSecondary.EMP);

		StartCoroutine(mScreenFader.FadeFromBlack());
	}

//--------------------------------------------------------------------------------------------

	void Update()
	{
		//continue button is disabled when there is no currently selected level
		mStartButton.interactable = mCurrentLoadout.isComplete();
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked(){ StartCoroutine(handleBackButtonClickedHelper()); }
	private IEnumerator handleBackButtonClickedHelper()
	{
		//load the worldmap scene
		Debug.Log("LOADING WORLD MAP");

		yield return mScreenFader.FadeToBlack();
		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);

		yield return null;
	}

//--------------------------------------------------------------------------------------------

	public void handleStartButtonClicked(){ StartCoroutine(handleStartButtonClickedHelper()); }
	private IEnumerator handleStartButtonClickedHelper()
	{
		//set the current game ptr's loadout object
		SavedGame currentGame = mSavedGameManager.getCurrentGame();
		currentGame.setCurrentLoadout(mCurrentLoadout);

		//load the gameplay scene
		Debug.Log("CURRENT LOADOUT: " + mCurrentLoadout.toString());
		Debug.Log("LOADING GAMEPLAY SCENE: " + currentGame.getSelectedLevel());

		yield return mScreenFader.FadeToBlack();
		SceneManager.LoadScene((int)currentGame.getSelectedLevel());

		yield return null;
	}

//--------------------------------------------------------------------------------------------

	public void handleChassisButtonClicked()
	{
		mChoicePanel.GetComponentInChildren<Text>().text = "Chassis";

		//for the first 5 buttons in the choices panel...
		Button[] buttons = mChoicePanel.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length - 1; ++i)
		{
			//get the button event handler
			LoadoutElementButtonEventHandler beh = buttons[i].GetComponent<LoadoutElementButtonEventHandler>();

			//set its loadout element indicies -- only chassis not null
			beh.setLoadoutIndices(i, -1, -1);

			//set the button's unlock and image
			beh.isUnlocked = mSavedGameManager.getCurrentGame().unlockedChasis[i];
			buttons[i].interactable = beh.chasisIndex != mCurrentLoadout.getChasis() && beh.isUnlocked;

			if(beh.isUnlocked)
			{
				//TODO -- change picture of button
				buttons[i].GetComponentInChildren<Text>().text = beh.chasisIndex.ToString();
			}
			else
			{
				//TODO -- classified image
				buttons[i].GetComponentInChildren<Text>().text = beh.chasisIndex.ToString();
			}
		}

		toggleMainPanelButtonsActive();

		mChoicePanel.SetActive(true);
		mDataPanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handlePrimaryButtonClicked()
	{
		mChoicePanel.GetComponentInChildren<Text>().text = "Primary";

		//for the first 5 buttons in the choices panel...
		Button[] buttons = mChoicePanel.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length - 1; ++i)
		{
			//get the button event handler
			LoadoutElementButtonEventHandler beh = buttons[i].GetComponent<LoadoutElementButtonEventHandler>();

			//set its loadout element indicies -- only primary not null
			beh.setLoadoutIndices(-1, i, -1);

			//set the button's unlock and image
			beh.isUnlocked = mSavedGameManager.getCurrentGame().unlockedPrimary[i];
			buttons[i].interactable = beh.primaryIndex != mCurrentLoadout.getPrimary() && beh.isUnlocked;

			if(beh.isUnlocked)
			{
				//TODO -- change picture of button
				buttons[i].GetComponentInChildren<Text>().text = beh.primaryIndex.ToString();
			}
			else
			{
				//TODO -- classified image
				buttons[i].GetComponentInChildren<Text>().text = beh.primaryIndex.ToString();
			}
		}

		toggleMainPanelButtonsActive();

		mChoicePanel.SetActive(true);
		mDataPanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleSecondaryButtonClicked()
	{
		mChoicePanel.GetComponentInChildren<Text>().text = "Secondary";

		//for the first 5 buttons in the choices panel...
		Button[] buttons = mChoicePanel.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length - 1; ++i)
		{
			//get the button event handler
			LoadoutElementButtonEventHandler beh = buttons[i].GetComponent<LoadoutElementButtonEventHandler>();

			//set its loadout element indicies -- only secondary not null
			beh.setLoadoutIndices(-1, -1, i);

			//set the button's unlock and image
			beh.isUnlocked = mSavedGameManager.getCurrentGame().unlockedSecondary[i];
			buttons[i].interactable = beh.secondaryIndex != mCurrentLoadout.getSecondary() && beh.isUnlocked;

			if(beh.isUnlocked)
			{
				//TODO -- change picture of button
				buttons[i].GetComponentInChildren<Text>().text = beh.secondaryIndex.ToString();
			}
			else
			{
				//TODO -- classified image
				buttons[i].GetComponentInChildren<Text>().text = beh.primaryIndex.ToString();
			}
		}

		toggleMainPanelButtonsActive();

		mChoicePanel.SetActive(true);
		mDataPanel.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleChoiceButtonClicked(Loadout.LoadoutChasis ci, Loadout.LoadoutPrimary pi, Loadout.LoadoutSecondary si)
	{
		//CHASSIS
		if(ci != Loadout.LoadoutChasis.NULL)
		{
			mCurrentLoadout.setChasis(ci);
			Debug.Log("NEW CHASSIS SELECTED: " + ci);

			setChoiceButtonsActive();
		}

		//PRIMARY
		else if(pi != Loadout.LoadoutPrimary.NULL)
		{
			mCurrentLoadout.setPrimary(pi);
			Debug.Log("NEW PRIMARY SELECTED: " + pi);

			setChoiceButtonsActive();
		}

		//SECONDARY
		else if(si != Loadout.LoadoutSecondary.NULL)
		{
			mCurrentLoadout.setSecondary(si);
			Debug.Log("NEW SECONDARY SELECTED: " + si);

			setChoiceButtonsActive();
		}

		//BACK BUTTON
		else
		{
			toggleMainPanelButtonsActive();

			mChoicePanel.SetActive(false);
			mDataPanel.SetActive(false);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleChoiceButtonMouseOver(Loadout.LoadoutChasis ci, Loadout.LoadoutPrimary pi, Loadout.LoadoutSecondary si)
	{
		initDataPanel(ci, pi, si);
	}

//--------------------------------------------------------------------------------------------

	public void handleChoiceButtonMouseExit()
	{
		initDataPanel(Loadout.LoadoutChasis.NULL, Loadout.LoadoutPrimary.NULL, Loadout.LoadoutSecondary.NULL);
	}

//--------------------------------------------------------------------------------------------

	private void initDataPanel(Loadout.LoadoutChasis ci, Loadout.LoadoutPrimary pi, Loadout.LoadoutSecondary si)
	{
		Text[] texts = mDataPanel.GetComponentsInChildren<Text>();

		//CHASSIS
		if(ci != Loadout.LoadoutChasis.NULL)
		{
			texts[0].text = ci.ToString();	//name
			texts[1].text = ci.ToString();	//description
			texts[2].text = ci.ToString();	//unlock

			//TODO -- set image
			//TODO -- set texts
		}

		//PRIMARY
		else if(pi != Loadout.LoadoutPrimary.NULL)
		{
			texts[0].text = pi.ToString();
			texts[1].text = pi.ToString();
			texts[2].text = pi.ToString();

			//TODO -- set image
			//TODO -- set texts
		}

		//SECONDARY
		else if(si != Loadout.LoadoutSecondary.NULL)
		{
			texts[0].text = si.ToString();
			texts[1].text = si.ToString();
			texts[2].text = si.ToString();

			//TODO -- set image
			//TODO -- set texts
		}

		//NULL
		else
		{
			//set each textbox's text to empty
			foreach(Text t in mDataPanel.GetComponentsInChildren<Text>())
			{
				t.text = "-";
			}

			//TODO -- set image to empty
		}
	}

//--------------------------------------------------------------------------------------------

	private void setChoiceButtonsActive()
	{
		//for each button on the choice panel...
		foreach(Button b in mChoicePanel.GetComponentsInChildren<Button>())
		{
			//it is interactable if its choice is unlocked
			LoadoutElementButtonEventHandler beh = b.GetComponent<LoadoutElementButtonEventHandler>();
			b.interactable = beh.isUnlocked;
		}
	}

//--------------------------------------------------------------------------------------------

	private void setChoiceButtonsSprites(Sprite[] spriteArray)
	{
		//TODO
	}

//--------------------------------------------------------------------------------------------

	public void toggleMainPanelButtonsActive()
	{
		foreach(Button b in mMainPanel.GetComponentsInChildren<Button>())
		{
			b.interactable = !b.interactable;
		}
	}
}
