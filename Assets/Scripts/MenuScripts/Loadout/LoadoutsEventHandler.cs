
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
	public Sprite[] buttonSprites;
	//TODO -- sprite arrays (choice icons)

	public string[,] elementStrings;

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

		//init resource arrays
		buttonSprites = Resources.LoadAll<Sprite>("GUI_Assets/Menu_Loadouts");
		//TODO -- init choice icon array

		//TODO -- complete the element strings arrays
		elementStrings = new string[16, 3]		//name, description, unlock
		{
			{"Exterminator", 		"a", "-"},
			{"Final", 				"b", "Complete Tutorial 3 to unlock!"},
			{"Booster", 			"c", "Complete Level 1-1 to unlock!"},
			{"Shrink", 				"d", "Complete Level 2-2 to unlock!"},
			{"Quick", 				"e", "Complete Level 3-3 to unlock!"},

			{"Bug Repellants", 		"f", "-"},
			{"No-Miss Swatter", 	"g", "Complete Tutorial 2 to unlock!"},
			{"Precision Pesticide", "h", "Complete Level 1-2 to unlock!"},
			{"Citronella Flame", 	"i", "Complete Level 2-1 to unlock!"},
			{"Volt Lantern", 		"j", "Complete Level 3-2 to unlock!"},

			{"EMP Counter", 		"k", "-"},
			{"Phasing System", 		"l", "Complete Tutorial 1 to unlock!"},
			{"Holo-Duplicate", 		"m", "Complete Level 1-3 to unlock!"},
			{"Mosquito Tesla Coil", "n", "Complete Level 2-3 to unlock!"},
			{"Freeze Ray", 			"o", "Complete Level 3-1 to unlock!"},

			{"-", "-", "-"}
		};

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
				setChoiceButtonsSprites(buttons[i], i * 4 + 16);
			}
			else
			{
				buttons[i].gameObject.GetComponent<Image>().sprite = buttonSprites[buttonSprites.Length - 1];
				buttons[i].transition = Button.Transition.None;
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
				setChoiceButtonsSprites(buttons[i], i * 4 + 36);
			}
			else
			{
				buttons[i].gameObject.GetComponent<Image>().sprite = buttonSprites[buttonSprites.Length - 1];
				buttons[i].transition = Button.Transition.None;
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
				setChoiceButtonsSprites(buttons[i], i * 4 + 56);
			}
			else
			{
				buttons[i].gameObject.GetComponent<Image>().sprite = buttonSprites[buttonSprites.Length - 1];
				buttons[i].transition = Button.Transition.None;
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

	public void handleChoiceButtonMouseOver(
		Loadout.LoadoutChasis ci, 
		Loadout.LoadoutPrimary pi, 
		Loadout.LoadoutSecondary si,
		bool isChoiceUnlocked)
	{
		initDataPanel(ci, pi, si, isChoiceUnlocked);
	}

//--------------------------------------------------------------------------------------------

	public void handleChoiceButtonMouseExit()
	{
		initDataPanel(Loadout.LoadoutChasis.NULL, Loadout.LoadoutPrimary.NULL, Loadout.LoadoutSecondary.NULL);
	}

//--------------------------------------------------------------------------------------------

	private void initDataPanel(
		Loadout.LoadoutChasis ci, 
		Loadout.LoadoutPrimary pi, 
		Loadout.LoadoutSecondary si,
		bool isChoiceUnlocked = false)
	{
		Text[] texts = mDataPanel.GetComponentsInChildren<Text>();
		int index = 0;

		if(ci != Loadout.LoadoutChasis.NULL)
		{
			index = (int)ci;
		}
		else if(pi != Loadout.LoadoutPrimary.NULL)
		{
			index = (int)pi + 5;
		}
		else if(si != Loadout.LoadoutSecondary.NULL)
		{
			index = (int)si + 10;
		}
		else
		{
			index = 15;
		}

		//if the element is unlocked...
		if(isChoiceUnlocked)
		{
			//TODO -- set element icon

			//set name, description, blank unlock criteria
			texts[0].text = elementStrings[index, 0];
			texts[1].text = elementStrings[index, 1];
			texts[2].text = "-";
		}

		//otherwise, if the element is locked...
		else
		{
			//TODO -- set locked icon

			//set blank name, blank description, unlock criteria
			texts[0].text = "-";
			texts[1].text = "-";
			texts[2].text = elementStrings[index, 2];
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

	private void setChoiceButtonsSprites(Button b, int spriteIndex)
	{
		SpriteState ss = new SpriteState();

		//set the default state
		b.gameObject.GetComponent<Image>().sprite = buttonSprites[spriteIndex];

		//set sprite state images
		ss.pressedSprite = buttonSprites[spriteIndex + 1];
		ss.disabledSprite = buttonSprites[spriteIndex + 2];
		ss.highlightedSprite = buttonSprites[spriteIndex + 3];

		//apply change to the button
		b.transition = Button.Transition.SpriteSwap;
		b.spriteState = ss;
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
