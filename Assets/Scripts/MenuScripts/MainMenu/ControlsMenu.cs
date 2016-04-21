
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlsMenu : MonoBehaviour
{
	//PUBLIC
	public Text keyboardControls;		//TODO -- switch these to image?
	public Text controllerControls;

	public Button backButton;
	public Button keyboardButton;
	public Button controllerButton;

	//PRIVATE
	private MainMenuEventHandler mMainMenu;

//--------------------------------------------------------------------------------------------

	void OnEnable()
	{
		mMainMenu = GetComponentInParent<MainMenuEventHandler>();
		backButton.Select();
	}

//--------------------------------------------------------------------------------------------

	public void handleKeyboardButtonClicked()
	{
		//toggle active on controls and set buttons enable
		controllerControls.gameObject.SetActive(false);
		keyboardControls.gameObject.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleControllerButtonClicked()
	{
		//toggle active on controls and set buttons enable
		keyboardControls.gameObject.SetActive(false);
		controllerControls.gameObject.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		gameObject.SetActive(false);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}
}
