
using UnityEngine;
using System.Collections;

public class Loadout
{
	//PUBLIC 
	public enum LoadoutChasis		//chasis enums
	{
		NULL = -1,

		DEFAULT = 0,
		CHASIS_1 = 1,
		CHASIS_2 = 2,
		CHASIS_3 = 3,
		CHASIS_4 = 4
	}

	public enum LoadoutPrimary		//primary weapon enums
	{
		NULL = -1,

		DEFAULT = 0,
		PRIMARY_1 = 1,
		PRIMARY_2 = 2,
		PRIMARY_3 = 3,
		PRIMARY_4 = 4
	}

	public enum LoadoutSecondary	//secondary weapon enums
	{
		NULL = -1,

		DEFAULT = 0,
		SECONDARY_1 = 1,
		SECONDARY_2 = 2,
		SECONDARY_3 = 3,
		SECONDARY_4 = 4
	}

	public static int NUM_LOADOUTS = 5;

	//PRIVATE
	private LoadoutChasis mChasis;
	private LoadoutPrimary mPrimary;
	private LoadoutSecondary mSecondary;

//--------------------------------------------------------------------------------------------

	public Loadout()
	{
		mChasis = LoadoutChasis.NULL;
		mPrimary = LoadoutPrimary.NULL;
		mSecondary = LoadoutSecondary.NULL;
	}

//--------------------------------------------------------------------------------------------

	public bool isComplete()
	{
		//if any chasis, primary, or secondary are null, return false
		if(mChasis == LoadoutChasis.NULL)
			return false; 

		if(mPrimary == LoadoutPrimary.NULL)
			return false;
		
		if(mSecondary == LoadoutSecondary.NULL)
			return false;

		//otherwise, return true
		return true;
	}

//--------------------------------------------------------------------------------------------

	public string toString()
	{
		return "CHASIS: " + mChasis + " | PRIMARY: " + mPrimary + " | SECONDARY: " + mSecondary;
	}

//--------------------------------------------------------------------------------------------

	//getter-setters for chasis
	public LoadoutChasis getChasis(){ return mChasis; }
	public void setChasis(LoadoutChasis lc){ mChasis = lc; }

//--------------------------------------------------------------------------------------------

	//getter-setters for primary weapon
	public LoadoutPrimary getPrimary(){ return mPrimary; }
	public void setPrimary(LoadoutPrimary lp){ mPrimary = lp; }

//--------------------------------------------------------------------------------------------

	//getter-setters for secondary weapon
	public LoadoutSecondary getSecondary(){ return mSecondary; }
	public void setSecondary(LoadoutSecondary ls){ mSecondary = ls; }
}


//============================================================================================


public class SavedGame
{
	//PUBLIC
	public bool[] unlockedChasis;		//loadout element arrays
	public bool[] unlockedPrimary;
	public bool[] unlockedSecondary;

	public bool[] unlockedLevels;		//level completion array
	public int[] highScores;			//level highscore array

	//PRIVATE
	private string mName;

	private SceneIndex mSelectedLevel;
	private Loadout mCurrentLoadout;

//--------------------------------------------------------------------------------------------

	public SavedGame(string name)
	{
		mName = name;

		mSelectedLevel = SceneIndex.NULL;
		mCurrentLoadout = null;

		//init loadout arrays
		initLoadoutArrays(Loadout.NUM_LOADOUTS);
		initLevelCompletionArray(SavedGameManager.NUM_GAMEPLAY_LEVELS);
		initHighScoreArray(SavedGameManager.NUM_GAMEPLAY_LEVELS);
	}

//--------------------------------------------------------------------------------------------

	private void initLoadoutArrays(int size)
	{
		unlockedChasis = initBoolArray(size);
		unlockedPrimary = initBoolArray(size);
		unlockedSecondary = initBoolArray(size);

		//unlocked by default
		unlockedChasis[0] = true;
		unlockedPrimary[0] = true;
		unlockedSecondary[0] = true;
	}

//--------------------------------------------------------------------------------------------

	private void initLevelCompletionArray(int size)
	{
		unlockedLevels = initBoolArray(size);

		//unlocked by default
		unlockedLevels[0] = true;
	}

//--------------------------------------------------------------------------------------------

	private void initHighScoreArray(int size)
	{
		highScores = initIntArray(size);
	}

//--------------------------------------------------------------------------------------------

	private bool[] initBoolArray(int size)
	{
		bool[] array = new bool[size];
		for(int i = 0; i < size; ++i)
		{
			array[i] = true;	//TODO -- temp force this to true
		}

		return array;
	}

//--------------------------------------------------------------------------------------------

	private int[] initIntArray(int size)
	{
		int[] array = new int[size];
		for(int i = 0; i < size; ++i)
		{
			array[i] = (int)(Random.value * 10);	//TODO -- temp
		}

		return array;
	}

//--------------------------------------------------------------------------------------------

	public void handleIncomingScore(int index, int score)
	{
		if(highScores[index] < score)
		{
			highScores[index] = score;
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleIncomingLevelUnlock(int index)
	{
		//TODO -- keep an eye on this for proper indexing
		switch(index)
		{
		//final tutorial stage
		case 2:

			unlockedLevels[3] = true;	//unlock the first stages of levels 1, 2, and 3
			unlockedLevels[5] = true;
			unlockedLevels[6] = true;
			break;

		//final non-tutorial stages, no level unlocks
		case 5:
		case 8:
		case 11:
		case 14:
			break;

		//all other stages unlock the next stage
		default:

			unlockedLevels[index + 1] = true;
			break;
		}
	}

//--------------------------------------------------------------------------------------------

	public void handleIncomingLoadoutUnlock(int index)
	{
		//index for the array
		int j = (index / 3) + 1;

		//TODO -- keep an eye on this for proper indexing
		//TODO -- work on unlock order
		switch(index)
		{
		//first stage -- unlock chasis
		case 0:
		case 3:
		case 6:
		case 9:
			unlockedChasis[j] = true;
			break;

		//second stage -- unlock primary weapon
		case 1:
		case 4:
		case 7:
		case 10:
			unlockedPrimary[j] = true;
			break;

		//final stage -- unlock secondary weapon
		case 2:
		case 5:
		case 8:
		case 11:
			unlockedSecondary[j] = true;
			break;

		//otherwise, do nothing
		default:
			break;
		}
	}

//--------------------------------------------------------------------------------------------

	//getter for name
	public string getName(){ return mName; }

//--------------------------------------------------------------------------------------------

	//getter-setter for selected level
	public SceneIndex getSelectedLevel(){ return mSelectedLevel; }
	public void setSelectedLevel(SceneIndex sl){ mSelectedLevel = sl; }

//--------------------------------------------------------------------------------------------

	//getter-setter for current loadout
	public Loadout getCurrentLoadout(){ return mCurrentLoadout; }
	public void setCurrentLoadout(Loadout cl){ mCurrentLoadout = cl; }
}
