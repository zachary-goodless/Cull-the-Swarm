
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
			{"Exterminator", 		"a", "a"},
			{"Final", 				"b", "b"},
			{"Booster", 			"c", "c"},
			{"Shrink", 				"d", "d"},
			{"Quick", 				"e", "e"},

			{"Bug Repellants", 		"f", "f"},
			{"No-Miss Swatter", 	"g", "g"},
			{"Precision Pesticide", "h", "h"},
			{"Citronella Flame", 	"i", "i"},
			{"Volt Lantern", 		"j", "j"},

			{"EMP Counter", 		"k", "k"},
			{"Phasing System", 		"l", "l"},
			{"Holo-Duplicate", 		"m", "m"},
			{"Mosquito Tesla Coil", "n", "n"},
			{"Freeze Ray", 			"o", "o"},

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

		//CHASSIS
		if(ci != Loadout.LoadoutChasis.NULL && isChoiceUnlocked)
		{
			index = (int)ci;
		}

		//PRIMARY
		else if(pi != Loadout.LoadoutPrimary.NULL && isChoiceUnlocked)
		{
			index = (int)pi + 5;
		}

		//SECONDARY
		else if(si != Loadout.LoadoutSecondary.NULL && isChoiceUnlocked)
		{
			index = (int)si + 10;
		}

		//NULL
		else
		{
			index = 15;
		}

		texts[0].text = elementStrings[index, 0];	//name
		texts[1].text = elementStrings[index, 1];	//description
		texts[2].text = elementStrings[index, 2];	//unlock

		//TODO -- set image
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

		//set sprite stage images
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
