
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class RetryDialog : MonoBehaviour
{
	//PUBLIC
	//

	//PRIVATE
	//

//--------------------------------------------------------------------------------------------

	void Start ()
	{
		//force pause
		Time.timeScale = 0f;
		LevelCompleteHandler.isLevelComplete = true;
	}

//--------------------------------------------------------------------------------------------

	public void handleYesButtonClicked()
	{
		//unpause and load the level again
		Time.timeScale = 1f;
		LevelCompleteHandler.isLevelComplete = false;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

//--------------------------------------------------------------------------------------------

	public void handleNoButtonClicked()
	{
		//unpause and load worldmap menu
		Time.timeScale = 1f;
		LevelCompleteHandler.isLevelComplete = false;

		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
	}
}
