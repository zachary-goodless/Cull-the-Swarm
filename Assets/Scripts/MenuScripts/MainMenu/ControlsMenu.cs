
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlsMenu : MonoBehaviour
{
	//PUBLIC
	public Text titleText;

	public Text keyboardControls_1;
	public Text keyboardControls_2;

	public Text controllerControls_1;
	public Text controllerControls_2;

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
		titleText.text = "Keyboard Controls";

		//toggle active on controls and set buttons enable
		controllerControls_1.gameObject.SetActive(false);
		controllerControls_2.gameObject.SetActive(false);

		keyboardControls_1.gameObject.SetActive(true);
		keyboardControls_2.gameObject.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleControllerButtonClicked()
	{
		titleText.text = "Xbox 360 Controls";

		//toggle active on controls and set buttons enable
		keyboardControls_1.gameObject.SetActive(false);
		keyboardControls_2.gameObject.SetActive(false);

		controllerControls_1.gameObject.SetActive(true);
		controllerControls_2.gameObject.SetActive(true);
	}

//--------------------------------------------------------------------------------------------

	public void handleBackButtonClicked()
	{
		gameObject.SetActive(false);

		mMainMenu.toggleButtons();
		mMainMenu.lastButtonClicked.Select();
	}
}
