
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SavedGameManager : MonoBehaviour
{
	public static int NUM_GAMEPLAY_LEVELS = 15;	//TODO -- hardcoded to 15 right now (5 levels of 3 stages)

	//PUBLIC
	//TODO -- saved game file location (set in inspector, relative to user appdata?)

	public int[] globalHighScores;

	//PRIVATE
	private static SavedGameManager mInstance;

	private Dictionary<string, SavedGame> mGamesMap = new Dictionary<string, SavedGame>();
	private SavedGame mCurrentGame;

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

		//TODO -- temp force high scores
		globalHighScores = new int[NUM_GAMEPLAY_LEVELS];
		for(int i = 0; i < globalHighScores.Length; ++i)
		{
			globalHighScores[i] = (int)(Random.value * 10);
		}
		//TODO
	}

//--------------------------------------------------------------------------------------------

	public bool createNewGame(string name)
	{
		//if our games map contains a game w/ the given name...
		if(mGamesMap.ContainsKey(name))
		{
			//return false -- no new game created
			return false;
		}

		Debug.Log("CREATING NEW SAVED GAME: " + name);

		//otherwise, create a new game object
		mCurrentGame = new SavedGame(name);
		mGamesMap.Add(name, mCurrentGame);

		return true;
	}

//--------------------------------------------------------------------------------------------

	public bool loadSavedGame(string name)
	{
		//if our games map doesn't contain a game w/ the given name...
		if(!mGamesMap.ContainsKey(name))
		{
			//return false -- no game loaded
			return false;
		}

		Debug.Log("LOADING SAVED GAME: " + name);

		//otherwise, set the current game object
		mCurrentGame = mGamesMap[name];

		return true;
	}

//--------------------------------------------------------------------------------------------

	public bool deleteSavedGame(string name)
	{
		//if the games map doesn't contain a game w/ the given name...
		if(!mGamesMap.ContainsKey(name))
		{
			//return false -- no game deleted
			return false;
		}

		Debug.Log("DELETING SAVED GAME: " + name);

		//otherwise, delete the saved game object
		mGamesMap.Remove(name);

		//delete the current game if it is being deleted
		if(mCurrentGame != null && mCurrentGame.getName() == name)
		{
			mCurrentGame = null;
		}

		return true;
	}

//--------------------------------------------------------------------------------------------

	private bool readSavedGameFile()
	{
		bool success = true;

		//TODO -- read from saved game file

		//first line is global highscore data
		//foreach line starting w/ '#'
		//		create new game obj
		//		read next 'n' lines, populate game obj
		//		add game obj to list of saved games

		return success;
	}

//--------------------------------------------------------------------------------------------

	public bool writeSavedGameFile()
	{
		bool success = true;

		//TODO -- write to saved game file

		//write global highscores to file
		//foreach game obj in list
		//		write name
		//		write out data

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

	public void handleLevelCompleted(SceneIndex currentLevel, int score)
	{
		//TODO -- keep an eye on this for proper indexing
		//convert from SceneIndex to indexing int
		int i = (int)currentLevel - 3;
		if(i < 0 || i >= NUM_GAMEPLAY_LEVELS) return;

		//handle high scores
		if(globalHighScores[i] < score)			//global
		{
			globalHighScores[i] = score;
		}

		//handle stuff on the current game object
		mCurrentGame.handleIncomingScore(i, score);		//highscore
		mCurrentGame.handleIncomingLevelUnlock(i);		//level unlock
		mCurrentGame.handleIncomingLoadoutUnlock(i);	//loadout unlock
	}

//--------------------------------------------------------------------------------------------

	//getter for current game ptr
	public SavedGame getCurrentGame(){ return mCurrentGame; }
}
