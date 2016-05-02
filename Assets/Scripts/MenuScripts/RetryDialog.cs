
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class RetryDialog : MonoBehaviour
{
	//PUBLIC
	public Button yesButton;

	//PRIVATE
	//

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//force pause
		Time.timeScale = 0f;
		LevelCompleteHandler.isLevelComplete = true;

		yesButton.Select();
	}

//--------------------------------------------------------------------------------------------

	void yesButtonHelper()
	{
		LevelCompleteHandler.isLevelComplete = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void handleYesButtonClicked()
	{
		//unpause and load the level again
		Time.timeScale = 1f;
		Invoke("yesButtonHelper", 0.2f);
	}

//--------------------------------------------------------------------------------------------

	void noButtonHelper()
	{
		LevelCompleteHandler.isLevelComplete = false;
		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
	}
	public void handleNoButtonClicked()
	{
		//unpause and load worldmap menu
		Time.timeScale = 1f;
		Invoke("noButtonHelper", 0.2f);
	}
}
