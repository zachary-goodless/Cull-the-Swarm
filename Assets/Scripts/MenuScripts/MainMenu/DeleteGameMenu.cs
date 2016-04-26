
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeleteGameMenu : MonoBehaviour
{
	//PUBLIC
	public Button mDeleteButton;
	public Button mBackButton;

	public GameObject elemPrefab;	//game list element

	public GameObject scrollPanel;

	//PRIVATE
	private string mName;

	private MainMenuEventHandler mMainMenu;
	private SavedGameManager mSavedGameManager;
//--------------------------------------------------------------------------------------------

	void OnEnable()
	{
		mName = "";

		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();

		StartCoroutine(buildList());
	}

//--------------------------------------------------------------------------------------------

	public IEnumerator buildList()
	{
		//clear games already in the list
		clearList();

		mDeleteButton.interactable = false;

		//get a list of saved games
		List<string> savedGames = mSavedGameManager.getSavedGameNames();

		//get rect transforms
		RectTransform elemRectTransform = elemPrefab.GetComponent<RectTransform>();
		RectTransform scrollRectTransform = scrollPanel.GetComponent<RectTransform>();

		//calc the width and height of each child item
		float width = scrollRectTransform.rect.width;
		float height = elemRectTransform.rect.height * (width / elemRectTransform.rect.width);

		//height of the scrollable panel
		float scrollHeight = height * savedGames.Count;
		scrollRectTransform.offsetMin = new Vector2(scrollRectTransform.offsetMin.x, -scrollHeight / 2);
		scrollRectTransform.offsetMax = new Vector2(scrollRectTransform.offsetMax.x, scrollHeight / 2);

		//for each saved game listed...
		int i = 0;
		foreach(string name in savedGames)
		{
			//create new list element
			GameObject newElem = Instantiate(elemPrefab) as GameObject;
			newElem.name = "ListElement_" + name;
			newElem.transform.SetParent(scrollPanel.transform, false);

			//select first element
			if(i == 0)
			{
				newElem.GetComponentInChildren<Button>().Select();
			}

			//set names
			newElem.GetComponent<GameListElementHandler>().setName(name);

			//place the new list element
			RectTransform rectTransform = newElem.GetComponent<RectTransform>();

			float x = -scrollRectTransform.rect.width / 2;
			float y = scrollRectTransform.rect.height / 2 - height * ++i;
			rectTransform.offsetMin = new Vector2(x, y);

			x = rectTransform.offsetMin.x + width;
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2(x, y);
		}

		//force scroll to top of list at start
		yield return null;
		GetComponentInChildren<Scrollbar>().value = 1f;

		mBackButton.Select();

		yield break;
	}

//--------------------------------------------------------------------------------------------

	private void clearList()
	{
		//remove each element in the list (children of the scrollPanel)
		GameListElementHandler[] listElems = scrollPanel.GetComponentsInChildren<GameListElementHandler>();
		foreach(GameListElementHandler elem in listElems)
		{
			Destroy(elem.gameObject);
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleElementButtonClicked(string name)
	{
		//save the name, enable the delete button
		mName = name;
		mDeleteButton.interactable = true;

		//enable all element buttons, clicked disables self
		Button[] buttons = scrollPanel.GetComponentsInChildren<Button>();
		foreach(Button button in buttons)
		{
			button.interactable = true;
		}

		mDeleteButton.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleDeleteButtonClicked()
	{
		//if the name is nonempty
		if(mName != "")
		{
			//delete a saved game object
			mSavedGameManager.deleteSavedGame(mName);
		}

		StartCoroutine(buildList());
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		//disable the new game panel
		mDeleteButton.interactable = false;
		gameObject.SetActive(false);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}
}
