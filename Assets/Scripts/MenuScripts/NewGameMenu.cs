
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewGameMenu : MonoBehaviour
{
	//PUBLIC
	public InputField mNameField;
	public Button mCreateNewGameButton;

	//PRIVATE
	private string mName;

	private MainMenuEventHandler mMainMenu;
	private SavedGameManager mSavedGameManager;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mName = "";

		mNameField.onValueChanged.AddListener( delegate{ handleTextEditValueChanged(); } );

		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();
	}

//--------------------------------------------------------------------------------------------

	public void handleTextEditValueChanged()
	{
		//save the text, remove spaces
		mName = mNameField.text;
		mName = mName.Replace(" ", "");

		//the create new game button is enabled if the text was nonempty
		mCreateNewGameButton.interactable = mName != "";
	}

//--------------------------------------------------------------------------------------------

	public void handleCreateNewGameButtonClicked()
	{
		//if the name is nonempty...
		if(mName != "")
		{
			//create a new game object
			mSavedGameManager.createNewGame(mName);

			//enable the main menu buttons
			mMainMenu.enableButtons();

			//clear the text edit, disable the new game panel
			mNameField.text = "";
			gameObject.SetActive(false);
		}
	}
}
