
using UnityEngine;
using System.Collections;

public class Loadout
{
	//TODO -- rename enum elements

	//PUBLIC 
	public enum LoadoutChasis		//chasis enums
	{
		NULL = -1,

		EXTERMINATOR = 	0,
		FINAL = 		1,
		BOOSTER = 		2,
		SHRINK = 		3,
		QUICK = 		4
	}

	public enum LoadoutPrimary		//primary weapon enums
	{
		NULL = -1,

		REPEL = 	0,
		SWATTER = 	1,
		BUGSPRAY = 	2,
		FLAME = 	3,
		VOLT = 		4
	}

	public enum LoadoutSecondary	//secondary weapon enums
	{
		NULL = -1,

		EMP = 		0,
		PHASING = 	1,
		HOLOGRAM = 	2,
		TESLA = 	3,
		FREEZE = 	4
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
	public int[] highScores;			//level highscore array
	public bool[] unlockedLevels;		//level completion array

	public bool[] unlockedChasis;		//loadout element arrays
	public bool[] unlockedPrimary;
	public bool[] unlockedSecondary;

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
			//defaults to true when in godmode, otherwise false
			array[i] = SavedGameManager.isGodMode;
		}

		return array;
	}

//--------------------------------------------------------------------------------------------

	private int[] initIntArray(int size)
	{
		int[] array = new int[size];
		for(int i = 0; i < size; ++i)
		{
			array[i] = 0;
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
		switch(index)
		{
		//final tutorial stage
		case 2:

			unlockedLevels[3] = true;	//unlock the first stages of levels 1, 2, and 3
			unlockedLevels[6] = true;
			unlockedLevels[9] = true;
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

	public void handleIncomingLoadoutUnlock(SceneIndex index)
	{
		switch(index)
		{
		case SceneIndex.GAMEPLAY_TUTORIAL_1:
			unlockedSecondary[(int)Loadout.LoadoutSecondary.PHASING] = true;
			break;
		case SceneIndex.GAMEPLAY_TUTORIAL_2:
			unlockedPrimary[(int)Loadout.LoadoutPrimary.SWATTER] = true;
			break;
		case SceneIndex.GAMEPLAY_TUTORIAL_3:
			unlockedChasis[(int)Loadout.LoadoutChasis.FINAL] = true;
			break;

		case SceneIndex.GAMEPLAY_1_1:
			unlockedChasis[(int)Loadout.LoadoutChasis.BOOSTER] = true;
			break;
		case SceneIndex.GAMEPLAY_1_2:
			unlockedPrimary[(int)Loadout.LoadoutPrimary.BUGSPRAY] = true;
			break;
		case SceneIndex.GAMEPLAY_1_3:
			unlockedSecondary[(int)Loadout.LoadoutSecondary.HOLOGRAM] = true;
			break;

		case SceneIndex.GAMEPLAY_2_1:
			unlockedPrimary[(int)Loadout.LoadoutPrimary.FLAME] = true;
			break;
		case SceneIndex.GAMEPLAY_2_2:
			unlockedChasis[(int)Loadout.LoadoutChasis.SHRINK] = true;
			break;
		case SceneIndex.GAMEPLAY_2_3:
			unlockedSecondary[(int)Loadout.LoadoutSecondary.TESLA] = true;
			break;

		case SceneIndex.GAMEPLAY_3_1:
			unlockedSecondary[(int)Loadout.LoadoutSecondary.FREEZE] = true;
			break;
		case SceneIndex.GAMEPLAY_3_2:
			unlockedPrimary[(int)Loadout.LoadoutPrimary.VOLT] = true;
			break;
		case SceneIndex.GAMEPLAY_3_3:
			unlockedChasis[(int)Loadout.LoadoutChasis.QUICK] = true;
			break;

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
