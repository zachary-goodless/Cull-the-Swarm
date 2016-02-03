
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

	public static int numLoadouts = 5;

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

	//TODO -- array for completed levels (or inferred from other arrays?)
	//TODO -- array for personal high scores

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
		initLoadoutArrays(Loadout.numLoadouts);

		//TODO -- other stuff
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
