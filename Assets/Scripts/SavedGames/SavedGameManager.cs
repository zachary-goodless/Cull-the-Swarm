
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SavedGameManager : MonoBehaviour
{
	//PUBLIC
	//TODO -- saved game file location (set in inspector, relative to user appdata?)

	//PRIVATE
	private SavedGameManager mInstance;

	private Dictionary<string, SavedGame> mGamesMap = new Dictionary<string, SavedGame>();
	private SavedGame mCurrentGame;

	//TODO -- 2d array for global high scores

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//on startup, set the static object if we haven't already
		if(mInstance == null)
		{
			mInstance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		//read from the saved game file on startup
		if(!readSavedGameFile())
		{
			Debug.Log("ERROR READING FROM SAVED GAME FILE!");
		}
	}

//--------------------------------------------------------------------------------------------

	public bool createNewGame(string name)
	{
		mCurrentGame = null;	//sanity check, zero out current game ptr

		//if our games map contains a game w/ the given name...
		if(mGamesMap.ContainsKey(name))
		{
			//return false -- no new game created
			return false;
		}

		//otherwise, create a new game object
		mCurrentGame = new SavedGame(name);
		mGamesMap.Add(name, mCurrentGame);

		return true;
	}

//--------------------------------------------------------------------------------------------

	public bool loadSavedGame(string name)
	{
		mCurrentGame = null;	//sanity check, zero out current game ptr

		//if our games map doesn't contain a game w/ the given name...
		if(!mGamesMap.ContainsKey(name))
		{
			//return false -- no game loaded
			return false;
		}

		//otherwise, set the current game object
		mCurrentGame = mGamesMap[name];

		return true;
	}

//--------------------------------------------------------------------------------------------

	public bool deleteSavedGame(string name)
	{
		mCurrentGame = null;	//sanity check, zero out current game ptr

		//if the games map doesn't contain a game w/ the given name...
		if(!mGamesMap.ContainsKey(name))
		{
			//return false -- no game deleted
			return false;
		}

		//otherwise, delete the saved game object
		mGamesMap.Remove(name);

		return true;
	}

//--------------------------------------------------------------------------------------------

	private bool readSavedGameFile()
	{
		bool success = true;

		//TODO -- read from saved game file

		return success;
	}

//--------------------------------------------------------------------------------------------

	public bool writeSavedGameFile()
	{
		bool success = true;

		//TODO -- write to saved game file

		return success;
	}

//--------------------------------------------------------------------------------------------

	public List<string> getSavedGameNames()
	{
		//add each key in the saved games map to a list, return
		//	caller takes ownership of list
		List<string> namesList = new List<string>();
		foreach(KeyValuePair<string, SavedGame> pair in mGamesMap)
		{
			namesList.Add(pair.Key);
		}

		return namesList;
	}

//--------------------------------------------------------------------------------------------

	//getter for current game ptr
	public SavedGame getCurrentGame(){ return mCurrentGame; }
}
