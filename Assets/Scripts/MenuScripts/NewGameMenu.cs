
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

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
			//create a new game object, load worldmap if successful
			if(mSavedGameManager.createNewGame(mName))
			{
				Debug.Log("LOADING WORLD MAP");
				SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
				return;
			}
		}

		handleBackButtonClicked();
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		//clear the text edit, disable the new game panel
		mNameField.text = "";
		gameObject.SetActive(false);

		mMainMenu.toggleButtons();
	}
}
