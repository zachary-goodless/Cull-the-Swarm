
using UnityEngine;
using System.Collections;

public class Loadout
{
	//PUBLIC 
	public enum LoadoutChasis		//chasis enums
	{
		NULL = -1,

		TEST = 0,
		//
	}

	public enum LoadoutPrimary		//primary weapon enums
	{
		NULL = -1,

		TEST = 0,
		//
	}

	public enum LoadoutSecondary	//secondary weapon enums
	{
		NULL = -1,

		TEST = 0,
		//
	}

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
	//

	//PRIVATE
	private string mName;

	//TODO -- array for completed levels (or inferred from other arrays?)
	//TODO -- array for personal high scores
	//TODO -- array for unlocked loadouts

	private SceneIndex mSelectedLevel;
	private Loadout mCurrentLoadout;

//--------------------------------------------------------------------------------------------

	public SavedGame(string name)
	{
		mName = name;

		mSelectedLevel = SceneIndex.NULL;
		mCurrentLoadout = null;
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
