
using UnityEngine;
using System.Collections;

public class Loadout
{
	//TODO -- enum for chasis
	//TODO -- enum for primary weapon
	//TODO -- enum for secondary weapon
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
		
		//TODO -- current loadout

//--------------------------------------------------------------------------------------------

	public SavedGame(string name)
	{
		mName = name;
	}

//--------------------------------------------------------------------------------------------

	//getter for name
	public string getName(){ return mName; }
}
