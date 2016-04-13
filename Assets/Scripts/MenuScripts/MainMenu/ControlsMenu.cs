
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

	// Use this for initialization
	void Start ()
	{
		mMainMenu = GetComponentInParent<MainMenuEventHandler>();

		setButtonsInteractable();
	}

//--------------------------------------------------------------------------------------------

	public void handleKeyboardButtonClicked()
	{
		//toggle active on controls and set buttons enable
		controllerControls.gameObject.SetActive(false);
		keyboardControls.gameObject.SetActive(true);

		setButtonsInteractable();
	}

//--------------------------------------------------------------------------------------------

	public void handleControllerButtonClicked()
	{
		//toggle active on controls and set buttons enable
		keyboardControls.gameObject.SetActive(false);
		controllerControls.gameObject.SetActive(true);

		setButtonsInteractable();
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		gameObject.SetActive(false);
		mMainMenu.toggleButtons();
	}

//--------------------------------------------------------------------------------------------

	void setButtonsInteractable()
	{
		//choice buttons active is opposite of their respective controller panel
		keyboardButton.interactable = !keyboardControls.gameObject.activeSelf;
		controllerButton.interactable = !controllerControls.gameObject.activeSelf;
	}
}
