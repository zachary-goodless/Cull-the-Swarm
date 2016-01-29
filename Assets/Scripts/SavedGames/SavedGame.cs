
using UnityEngine;
using System.Collections;

public class Loadout
{
	//PUBLIC 
	//

	//PRIVATE
	//TODO -- enum for chasis
	//TODO -- enum for primary weapon
	//TODO -- enum for secondary weapon

	public Loadout()
	{
		//TODO -- init blank
	}

//--------------------------------------------------------------------------------------------

	public bool isComplete()
	{
		//TODO -- complete when chasis, primary, and secondary are valid
		//return false;

		return true;	//TODO -- temporarily returns true all the time
	}
}

//============================================================================================

public class SavedGame
{
	//PUBLIC
	//

	//PRIVATE
	private string mName;

	//TODO -- 2d array for completed levels (or inferred from other arrays?)
	//TODO -- 2d array for personal high scores
	//TODO -- 2d array for unlocked loadouts

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
	public void setSelectedLevel(SceneIndex i){ mSelectedLevel = i; }

//--------------------------------------------------------------------------------------------

	//getter-setter for current loadout
	public Loadout getCurrentLoadout(){ return mCurrentLoadout; }
	public void setCurrentLoadout(Loadout l){ mCurrentLoadout = l; }
}
