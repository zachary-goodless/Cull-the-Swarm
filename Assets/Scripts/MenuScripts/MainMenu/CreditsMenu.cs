
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

	void Start ()
	{
		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		gameObject.SetActive(false);
		mMainMenu.toggleButtons();
	}
}
