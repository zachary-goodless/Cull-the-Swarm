﻿
using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public enum PointVals
{
	DRONE = 		10,
	BAGWORM = 		30,
	OTHER = 		50,

	BOSS_NEW_STAGE = 	500,
	BOSS_DEFEATED = 	1000,

	NO_HITS = 		1000,
}

public class Score : MonoBehaviour
{
	//PUBLIC
	public const float GRAZE_MULTIPLIER_TICK = 	0.01f;		//incoming score multiplier vals
	public const int GRAZES_PER_TICK = 			5;


	public float delayBetweenTicks = 0.01f;

	public bool isLevelOver = false;


	public int displayScore = 0;
	public int trueScore = 0;

	public bool wasPlayerHit = false;

	public float multiplier = 1f;
	public int numGrazes = 0;

	//PRIVATE
	Text scoreText;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//grab text component
		scoreText = GetComponent<Text>();

		//start score countup routine
		StartCoroutine(handleScoreCountUp());
	}

//--------------------------------------------------------------------------------------------

	public void handleEnemyDefeated(PointVals points)
	{
		trueScore += (int)((int)points * multiplier);	//TODO -- add calls on enemy script
	}

//--------------------------------------------------------------------------------------------

	public void handlePlayerHit()
	{
		wasPlayerHit = true;
	}

//--------------------------------------------------------------------------------------------

	public void handleGraze()
	{
		numGrazes++;
		multiplier = 1 + (numGrazes / GRAZES_PER_TICK) * GRAZE_MULTIPLIER_TICK;
	}

//--------------------------------------------------------------------------------------------

	IEnumerator handleScoreCountUp()
	{
		while(!isLevelOver)
		{
			while(displayScore < trueScore)
			{
				//increment display score up to true score
				displayScore += 10;
				if(displayScore > trueScore)
				{
					displayScore = trueScore;
				}

				//display the new display score
				scoreText.text = displayScore.ToString();

				yield return new WaitForSeconds(delayBetweenTicks);
			}

			yield return new WaitForSeconds(delayBetweenTicks);
		}

		yield break;
	}
}