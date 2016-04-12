
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
	public int noHitsMsgMaxFontSize = 20;

	public Text personalHighScoreMsg;
	public Text globalHighScoreMsg;
	public Text noHitsMsg;

	public Text finalChassisMsg;
	public Image finalChassisStar;

	public Text scoreDisplay;
	public Score score;

	public static bool isLevelComplete = false;

	//PRIVATE
	SavedGameManager gameManager;

	bool didUseFinalChassis = false;

	int finalScore = 0;
	int oldPersonalHighScore = 0;
	int oldGlobalHighScore = 0;

//--------------------------------------------------------------------------------------------

	public void handleLevelCompleted(SceneIndex level)
	{
		isLevelComplete = true;

		//get the saved game manager
		gameManager = GameObject.Find("SavedGameManager").GetComponent<SavedGameManager>();
		if(gameManager == null)
		{
			return;
		}

		//save whether or not the final chassis was used
		didUseFinalChassis = gameManager.getCurrentGame().getCurrentLoadout().getChasis() == Loadout.LoadoutChasis.FINAL;

		//save the score, and if there were no hits (if player not hit, bonus added to final score)
		score = GameObject.Find("Score").GetComponent<Score>();
		finalScore = score.wasPlayerHit ? 
			score.trueScore : 
			score.trueScore + (int)PointVals.NO_HITS;

		//save score and get the old high scores
		oldPersonalHighScore = gameManager.getCurrentGame().highScores[(int)level - 3];
		oldGlobalHighScore = gameManager.globalHighScores[(int)level - 3];

		//save game
		gameManager.handleLevelCompleted(level, finalScore, didUseFinalChassis);

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
		int displayScoreTick = finalScore / 100;
		if(displayScoreTick == 0)
		{
			displayScoreTick = 100;
		}

		//init the score display text
		int currentDisplayScore = 0;
		scoreDisplay.text = currentDisplayScore.ToString();

		//while the display score is below the final score...
		while(currentDisplayScore < finalScore)
		{
			yield return new WaitForSeconds(delayBetweenTicks);

			//increment the display score up to the actual score
			currentDisplayScore += displayScoreTick;
			if(currentDisplayScore > finalScore)
			{
				currentDisplayScore = finalScore;
			}

			scoreDisplay.text = currentDisplayScore.ToString();
		}

		yield return new WaitForSeconds(1f);

		//activate the high score and no hit messages
		bool isNewPersonal = false;
		bool isNewGlobal = false;
		bool isNoHit = false;

		if(oldPersonalHighScore < finalScore)
		{
			personalHighScoreMsg.gameObject.SetActive(true);
			isNewPersonal = true;
		}
		if(oldGlobalHighScore < finalScore)
		{
			globalHighScoreMsg.gameObject.SetActive(true);
			isNewGlobal = true;
		}
		if(!score.wasPlayerHit)
		{
			noHitsMsg.gameObject.SetActive(true);
			isNoHit = true;
		}
		if(didUseFinalChassis)
		{
			finalChassisMsg.gameObject.SetActive(true);
			finalChassisStar.gameObject.SetActive(true);
		}

		//init font size
		int currentHighScoreFontSize = personalHighScoreMsg.fontSize;
		int currentNoHitFontSize = noHitsMsg.fontSize;

		//while the current message font size is less than the max font size...
		while(currentHighScoreFontSize < highScoreMsgMaxFontSize || currentNoHitFontSize < noHitsMsgMaxFontSize)
		{
			yield return new WaitForSeconds(delayBetweenTicks);

			//increment the font sizes up to the max
			currentNoHitFontSize = currentHighScoreFontSize += 2;
			if(currentHighScoreFontSize > highScoreMsgMaxFontSize)
			{
				currentHighScoreFontSize = highScoreMsgMaxFontSize;
			}
			if(currentNoHitFontSize > noHitsMsgMaxFontSize)
			{
				currentNoHitFontSize = noHitsMsgMaxFontSize;
			}

			//grow the relevant messages
			if(isNewPersonal)
			{
				personalHighScoreMsg.fontSize = currentHighScoreFontSize;
			}
			if(isNewGlobal)
			{
				globalHighScoreMsg.fontSize = currentHighScoreFontSize;
			}
			if(isNoHit)
			{
				noHitsMsg.fontSize = currentNoHitFontSize;
			}
			if(didUseFinalChassis)
			{
				finalChassisMsg.fontSize = currentNoHitFontSize;
			}
		}

		yield break;
	}
}
