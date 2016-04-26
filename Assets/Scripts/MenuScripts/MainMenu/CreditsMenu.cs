
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditsMenu : MonoBehaviour
{
	//PUBLIC
	public Button backButton;

	//PRIVATE
	private MainMenuEventHandler mMainMenu;

//--------------------------------------------------------------------------------------------

	void OnEnable()
	{
		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
		backButton.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		gameObject.SetActive(false);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}
}
