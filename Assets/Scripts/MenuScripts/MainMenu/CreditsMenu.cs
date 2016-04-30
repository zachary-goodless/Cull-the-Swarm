
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

	void deactivate(){ gameObject.SetActive(false); }
	public void handleBackButtonClicked()
	{
		Invoke("deactivate", 0.2f);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}
}
