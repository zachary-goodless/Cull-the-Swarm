
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

	public Image chassisChoiceIcon;
	public Image primaryChoiceIcon;
	public Image secondaryChoiceIcon;

	public Button chassisButton;
	public Button primaryButton;
	public Button secondaryButton;
	public Button lastButtonClicked;

	//PRIVATE
	public Sprite[] buttonSprites;
	public Sprite[] iconSprites;

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
		iconSprites = Resources.LoadAll<Sprite>("GUI_Assets/NewLoadoutIcons");

		elementStrings = new string[16, 3]		//name, description, unlock
		{
			{"Exterminator", 		"Standard issue chassis. Takes 3 hits.", 									"-"},
			{"Final", 				"Breaks after a single hit, but radiates a mysterious energy...", 			"Complete Tutorial 3 to unlock!"},
			{"Booster", 			"Slower than the Exterminator, but Precision gives you a speed boost.", 	"Complete Level 1-1 to unlock!"},
			{"Shrink", 				"Slower than the Exterminator, but your hitbox in Precision is SMALL.", 	"Complete Level 2-2 to unlock!"},
			{"Quick", 				"Quicker but weaker than the Exterminator. Secondaries recharge faster.", 	"Complete Level 3-3 to unlock!"},

			{"Bug Repellants", 		"Standard issue anti-bug bullets.", 										"-"},
			{"No-Miss Swatter", 	"Fire and forget! Automatically attacks the nearest enemy.", 				"Complete Tutorial 2 to unlock!"},
			{"Precision Pesticide", "Laser that sweeps from side to side as you move.", 						"Complete Level 1-2 to unlock!"},
			{"Citronella Flame", 	"Shoots a short, but powerful cone of fire.", 								"Complete Level 2-1 to unlock!"},
			{"Volt Lantern", 		"Powerful forward and rear facing lasers.", 								"Complete Level 3-2 to unlock!"},

			{"EMP Counter", 		"Disrupts enemy firing systems and clears the area of bullets.", 			"-"},
			{"Phasing System", 		"Renders your ship invulnerable for a short time.", 						"Complete Tutorial 1 to unlock!"},
			{"Holo-Duplicate", 		"Deploys a hologram of your ship that enemies attack instead.", 			"Complete Level 1-3 to unlock!"},
			{"Mosquito Tesla Coil", "Hold to absorb bullets. Release to send damage back. Does not recharge!", 	"Complete Level 2-3 to unlock!"},
			{"Freeze Ray", 			"Starts a chain reaction that freezes nearby enemy bullets.", 				"Complete Level 3-1 to unlock!"},

			{"-", "-", "-"}
		};

		//sanity check -- null any current loadout data on the current game ptr
		mSavedGameManager.getCurrentGame().setCurrentLoadout(null);
		mCurrentLoadout = new Loadout();

		//initialize the default loadout
		mCurrentLoadout.setChasis(Loadout.LoadoutChasis.EXTERMINATOR);
		mCurrentLoadout.setPrimary(Loadout.LoadoutPrimary.REPEL);
		mCurrentLoadout.setSecondary(Loadout.LoadoutSecondary.EMP);

		chassisChoiceIcon.sprite = iconSprites[0];
		primaryChoiceIcon.sprite = iconSprites[5];
		secondaryChoiceIcon.sprite = iconSprites[10];

		//init menu as though chassis button has been clicked
		chassisButton.Select();
		handleChassisButtonClicked();

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
		lastButtonClicked = chassisButton;
		mChoicePanel.GetComponentInChildren<Text>().text = "Chassis";

		//for the first 5 buttons in the choices panel...
		Button[] buttons = mChoicePanel.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length; ++i)
		{
			//get the button event handler
			LoadoutElementButtonEventHandler beh = buttons[i].GetComponent<LoadoutElementButtonEventHandler>();

			//set its loadout element indicies -- only chassis not null
			beh.setLoadoutIndices(i, -1, -1);

			//set the button's unlock and image
			beh.isUnlocked = mSavedGameManager.getCurrentGame().unlockedChasis[i];
			buttons[i].interactable = beh.isUnlocked;

			if(beh.isUnlocked)
			{
				setChoiceButtonsSprites(buttons[i], i * 4 + 16);
			}
			else
			{
				buttons[i].gameObject.GetComponent<Image>().sprite = buttonSprites[buttonSprites.Length - 1];
				buttons[i].transition = Button.Transition.None;
			}

			//set navigation data for buttons
			Navigation nav = buttons[i].navigation;
			nav.selectOnLeft = lastButtonClicked;
			buttons[i].navigation = nav;
		}

		initDataPanel(Loadout.LoadoutChasis.NULL, Loadout.LoadoutPrimary.NULL, Loadout.LoadoutSecondary.NULL);
	}

//--------------------------------------------------------------------------------------------

	public void handlePrimaryButtonClicked()
	{
		lastButtonClicked = primaryButton;
		mChoicePanel.GetComponentInChildren<Text>().text = "Primary";

		//for the first 5 buttons in the choices panel...
		Button[] buttons = mChoicePanel.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length; ++i)
		{
			//get the button event handler
			LoadoutElementButtonEventHandler beh = buttons[i].GetComponent<LoadoutElementButtonEventHandler>();

			//set its loadout element indicies -- only primary not null
			beh.setLoadoutIndices(-1, i, -1);

			//set the button's unlock and image
			beh.isUnlocked = mSavedGameManager.getCurrentGame().unlockedPrimary[i];
			buttons[i].interactable = beh.isUnlocked;

			if(beh.isUnlocked)
			{
				setChoiceButtonsSprites(buttons[i], i * 4 + 36);
			}
			else
			{
				buttons[i].gameObject.GetComponent<Image>().sprite = buttonSprites[buttonSprites.Length - 1];
				buttons[i].transition = Button.Transition.None;
			}

			//set navigation data for buttons
			Navigation nav = buttons[i].navigation;
			nav.selectOnLeft = lastButtonClicked;
			buttons[i].navigation = nav;
		}

		initDataPanel(Loadout.LoadoutChasis.NULL, Loadout.LoadoutPrimary.NULL, Loadout.LoadoutSecondary.NULL);
	}

//--------------------------------------------------------------------------------------------

	public void handleSecondaryButtonClicked()
	{
		lastButtonClicked = secondaryButton;
		mChoicePanel.GetComponentInChildren<Text>().text = "Secondary";

		//for the first 5 buttons in the choices panel...
		Button[] buttons = mChoicePanel.GetComponentsInChildren<Button>();
		for(int i = 0; i < buttons.Length; ++i)
		{
			//get the button event handler
			LoadoutElementButtonEventHandler beh = buttons[i].GetComponent<LoadoutElementButtonEventHandler>();

			//set its loadout element indicies -- only secondary not null
			beh.setLoadoutIndices(-1, -1, i);

			//set the button's unlock and image
			beh.isUnlocked = mSavedGameManager.getCurrentGame().unlockedSecondary[i];
			buttons[i].interactable = beh.isUnlocked;

			if(beh.isUnlocked)
			{
				setChoiceButtonsSprites(buttons[i], i * 4 + 56);
			}
			else
			{
				buttons[i].gameObject.GetComponent<Image>().sprite = buttonSprites[buttonSprites.Length - 1];
				buttons[i].transition = Button.Transition.None;
			}

			//set navigation data for buttons
			Navigation nav = buttons[i].navigation;
			nav.selectOnLeft = lastButtonClicked;
			buttons[i].navigation = nav;
		}

		initDataPanel(Loadout.LoadoutChasis.NULL, Loadout.LoadoutPrimary.NULL, Loadout.LoadoutSecondary.NULL);
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
			chassisChoiceIcon.sprite = iconSprites[(int)ci];

			handlePrimaryButtonClicked();
		}

		//PRIMARY
		else if(pi != Loadout.LoadoutPrimary.NULL)
		{
			mCurrentLoadout.setPrimary(pi);
			Debug.Log("NEW PRIMARY SELECTED: " + pi);

			setChoiceButtonsActive();
			primaryChoiceIcon.sprite = iconSprites[(int)pi + 5];

			handleSecondaryButtonClicked();
		}

		//SECONDARY
		else if(si != Loadout.LoadoutSecondary.NULL)
		{
			mCurrentLoadout.setSecondary(si);
			Debug.Log("NEW SECONDARY SELECTED: " + si);

			setChoiceButtonsActive();
			secondaryChoiceIcon.sprite = iconSprites[(int)si + 10];
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
		GameObject temp = mDataPanel.transform.GetChild(3).gameObject;
		Image icon = temp.GetComponent<Image>();

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
			//set name, description, blank unlock criteria
			texts[0].text = elementStrings[index, 0];
			texts[1].text = elementStrings[index, 1];
			texts[2].text = "-";

			//set element icon
			icon.sprite = iconSprites[index];
		}

		//otherwise, if the element is locked...
		else
		{
			//set blank name, blank description, unlock criteria
			texts[0].text = "-";
			texts[1].text = "-";
			texts[2].text = elementStrings[index, 2];

			//set classified icon
			icon.sprite = iconSprites[15];
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
}
