
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class LevelCompleteHandler : MonoBehaviour
{
	//PUBLIC
	public float delayBetweenTicks = 0.01f;

	public int highScoreMsgMaxFontSize = 45;

	public Text personalHighScoreMsg;
	public Text globalHighScoreMsg;

	public Text scoreDisplay;

	public static bool isLevelComplete = false;

	//PRIVATE
	SavedGameManager gameManager;

	SceneIndex completedLevel;

	bool wasFinalChassisUsed = false;

	int playerScore = 0;
	int oldPersonalHighScore = 0;
	int oldGlobalHighScore = 0;

//--------------------------------------------------------------------------------------------

	public void handleLevelCompleted(SceneIndex level, int score, bool finalChassis)
	{
		isLevelComplete = true;

		//get the saved game manager
		gameManager = GameObject.Find("SavedGameManager").GetComponent<SavedGameManager>();
		if(gameManager == null)
		{
			return;
		}

		//save completed level and if the final chassis was used
		completedLevel = level;

		wasFinalChassisUsed = finalChassis;

		//save score and get the old high scores
		playerScore = score;
		oldPersonalHighScore = gameManager.getCurrentGame().highScores[(int)completedLevel - 3];
		oldGlobalHighScore = gameManager.globalHighScores[(int)completedLevel - 3];

		//save game
		gameManager.handleLevelCompleted(completedLevel, playerScore);

		//now we can activate the panel and run its animations
		gameObject.SetActive(true);
		StartCoroutine(handlePanelAnimations());
	}

//--------------------------------------------------------------------------------------------

	public void handleLevelSelectButtonClicked()
	{
		isLevelComplete = false;
		SceneManager.LoadScene((int)SceneIndex.WORLD_MAP);
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handlePanelAnimations()
	{
		//init display score
		int displayScoreTick = playerScore / 100;
		if(displayScoreTick == 0)
		{
			displayScoreTick = 100;
		}

		int currentDisplayScore = 0;
		scoreDisplay.text = currentDisplayScore.ToString();

		//while the display score is below the player's score...
		while(currentDisplayScore < playerScore)
		{
			yield return new WaitForSeconds(delayBetweenTicks);

			//increment the display score up to the actual score
			currentDisplayScore += displayScoreTick;
			if(currentDisplayScore > playerScore)
			{
				currentDisplayScore = playerScore;
			}

			scoreDisplay.text = currentDisplayScore.ToString();
		}

		yield return new WaitForSeconds(1f);

		//activate the high score messages
		bool isNewPersonal = false;
		bool isNewGlobal = false;

		if(oldPersonalHighScore < playerScore)
		{
			personalHighScoreMsg.gameObject.SetActive(true);
			isNewPersonal = true;
		}
		if(oldGlobalHighScore < playerScore)
		{
			globalHighScoreMsg.gameObject.SetActive(true);
			isNewGlobal = true;
		}

		//init font size
		int currentFontSize = personalHighScoreMsg.fontSize;

		//while the current message font size is less than the max font size...
		while(currentFontSize < highScoreMsgMaxFontSize)
		{
			yield return new WaitForSeconds(delayBetweenTicks);

			//increment the font sizes up to the max
			currentFontSize += 2;
			if(currentFontSize > highScoreMsgMaxFontSize)
			{
				currentFontSize = highScoreMsgMaxFontSize;
			}

			//grow the relevant messages
			if(isNewPersonal)
			{
				personalHighScoreMsg.fontSize = currentFontSize;
			}
			if(isNewGlobal)
			{
				globalHighScoreMsg.fontSize = currentFontSize;
			}
		}

		yield break;
	}
}
