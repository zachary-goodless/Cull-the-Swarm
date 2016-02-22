
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.IO;

public class SavedGameManager : MonoBehaviour
{
	public bool GOD_MODE;						//TODO -- temp god mode for full-unlock (configurable from inspector)
	public static bool isGodMode;

	public static int NUM_GAMEPLAY_LEVELS = 15;	//TODO -- hardcoded to 15 right now (5 levels of 3 stages)

	//PUBLIC
	public int[] globalHighScores;

	//PRIVATE
	private static SavedGameManager mInstance;

	private string mSavedGameFile;

	private Dictionary<string, SavedGame> mGamesMap = new Dictionary<string, SavedGame>();
	private SavedGame mCurrentGame;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		//on startup, set the static object if we haven't already
		if(mInstance == null)
		{
			mInstance = this;
			isGodMode = GOD_MODE;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		//set saved game file location
		switch((int)Environment.OSVersion.Platform)
		{
		case 0:
		case 1:
		case 2:
		case 3:
			Debug.Log("PLATFORM DETECTED: WINDOWS");

			mSavedGameFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Cull_the_Swarm";
			Directory.CreateDirectory(mSavedGameFile);
			mSavedGameFile += "\\saved_games.ini";

			break;

		case 4:
		case 6:
			Debug.Log("PLATFORM DETECTED: UNIX / MAC");

			mSavedGameFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Library/Cull_the_Swarm";
			Directory.CreateDirectory(mSavedGameFile);
			mSavedGameFile += "/saved_games.ini";

			break;

		default:
			Debug.Log("UNHANDLED PLATFORM: " + Environment.OSVersion.Platform);
			mSavedGameFile = "";
			break;
		}


		Debug.Log("SAVED GAME FILE: " + mSavedGameFile);

		//read from the saved game file on startup
		if(!readSavedGameFile())
		{
			Debug.Log("ERROR READING FROM SAVED GAME FILE!");
		}
	}

//--------------------------------------------------------------------------------------------

	public bool createNewGame(string name)
	{
		//if our games map contains a game w/ the given name...
		if(mGamesMap.ContainsKey(name) || name == "")
		{
			//return false -- no new game created
			return false;
		}

		Debug.Log("CREATING NEW SAVED GAME: " + name);

		//otherwise, create a new game object
		mCurrentGame = new SavedGame(name);
		mGamesMap.Add(name, mCurrentGame);

		//major change -- write out to file
		writeSavedGameFile();

		return true;
	}

//--------------------------------------------------------------------------------------------

	public bool loadSavedGame(string name)
	{
		//if our games map doesn't contain a game w/ the given name...
		if(!mGamesMap.ContainsKey(name) || name == "")
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
		if(!mGamesMap.ContainsKey(name) || name == "")
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

		//major change -- write out to file
		writeSavedGameFile();

		return true;
	}

//--------------------------------------------------------------------------------------------

	private bool readSavedGameFile()
	{
		//init global high scores to default values
		globalHighScores = new int[NUM_GAMEPLAY_LEVELS];
		for(int i = 0; i < globalHighScores.Length; ++i)
		{
			globalHighScores[i] = 0;
		}
			
		//try to create a file reader
		StreamReader reader;
		try
		{
			reader = new StreamReader(mSavedGameFile);
		}
		catch
		{
			Debug.Log("ERROR: UNABLE TO CREATE STREAM READER");
			return false;
		}

		//if the file is not empty...
		if(reader.Peek() != -1)
		{
			//tokenize the first line, return if there's an error
			int[] array = new int[NUM_GAMEPLAY_LEVELS];
			if(!tokenizeInts(reader.ReadLine(), array))
			{
				Debug.Log("ERROR: ERROR READING GLOBAL HIGH SCORES");
				reader.Close();
				return false;
			}

			globalHighScores = array;
		}
		else
		{
			Debug.Log("ERROR: ERROR READING GLOBAL HIGH SCORES");
			reader.Close();
			return false;
		}
					
		//read saved game data until the end of the file...
		while(!reader.EndOfStream)
		{
			string tokens = "";

			//read the next line (game name)
			tokens = reader.ReadLine();
			if(tokens == null || tokens == "" || tokens[0] != '$')	//skip if there was any problem
				continue;

			//create a new saved game object
			tokens = tokens.Remove(0, 1);
			SavedGame game = new SavedGame(tokens);

			//read high scores for current game
			int[] highscores = new int[NUM_GAMEPLAY_LEVELS];
			if(!tokenizeInts(reader.ReadLine(), highscores))
				continue;

			//read level unlocks for current game
			bool[] levelUnlocks = new bool[NUM_GAMEPLAY_LEVELS];
			if(!tokenizeBools(reader.ReadLine(), levelUnlocks))
				continue;

			//read chasis unlocks for current game
			bool[] chasis = new bool[Loadout.NUM_LOADOUTS];
			if(!tokenizeBools(reader.ReadLine(), chasis))
				continue;

			//read primary unlocks for current game
			bool[] primary = new bool[Loadout.NUM_LOADOUTS];
			if(!tokenizeBools(reader.ReadLine(), primary))
				continue;

			//read secondary unlocks for current game
			bool[] secondary = new bool[Loadout.NUM_LOADOUTS];
			if(!tokenizeBools(reader.ReadLine(), secondary))
				continue;

			//set the values for the current game
			game.highScores = highscores;
			game.unlockedLevels = levelUnlocks;

			game.unlockedChasis = chasis;
			game.unlockedPrimary = primary;
			game.unlockedSecondary = secondary;

			//add the game to the list of saved games
			mGamesMap.Add(game.getName(), game);
		}

		reader.Close();
		return true;
	}

//--------------------------------------------------------------------------------------------

	public bool writeSavedGameFile()
	{
		Debug.Log("WRITING OUT TO FILE");

		//create a file writer
		StreamWriter writer = new StreamWriter(mSavedGameFile);

		//write the global high scores
		string line = "";
		foreach(int num in globalHighScores)
		{
			line += (num + " ");
		}
		writer.WriteLine(line);
		line = "";

		//foreach name-game pair in the games list...
		foreach(KeyValuePair<string, SavedGame> pair in mGamesMap)
		{
			//write the game's name
			writer.WriteLine("$" + pair.Key);

			//write the game's highscores
			foreach(int num in pair.Value.highScores)
			{
				line += (num + " ");
			}
			writer.WriteLine(line);
			line = "";

			//write the game's level unlocks
			foreach(bool val in pair.Value.unlockedLevels)
			{
				line += ((val ? "1" : "0") + " ");
			}
			writer.WriteLine(line);
			line = "";

			//write the game's chasis unlocks
			foreach(bool val in pair.Value.unlockedChasis)
			{
				line += ((val ? "1" : "0") + " ");
			}
			writer.WriteLine(line);
			line = "";

			//write the game's primary unlocks
			foreach(bool val in pair.Value.unlockedPrimary)
			{
				line += ((val ? "1" : "0") + " ");
			}
			writer.WriteLine(line);
			line = "";

			//write the game's secondary unlocks
			foreach(bool val in pair.Value.unlockedSecondary)
			{
				line += ((val ? "1" : "0") + " ");
			}
			writer.WriteLine(line);
			line = "";
		}

		writer.Close();
		return true;
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

		//major change -- write out to file
		writeSavedGameFile();
	}

//--------------------------------------------------------------------------------------------

	//getter for current game ptr
	public SavedGame getCurrentGame(){ return mCurrentGame; }

//--------------------------------------------------------------------------------------------

	private bool tokenizeInts(string line, int[] target)
	{
		//can't do anything if there's nothing to tokenize...
		if(line == "")
			return false;

		//split the read line into tokens
		char[] delimiters = {' '};
		string[] tokens = line.Split(delimiters);

		//error -- not enough tokens
		if(tokens.Length < target.Length)
			return false;

		//for each element in the target array...
		for(int i = 0; i < target.Length; ++i)
		{
			//parse it and set the target's value
			target[i] = int.Parse(tokens[i]);
		}

		return true;
	}

//--------------------------------------------------------------------------------------------

	private bool tokenizeBools(string line, bool[] target)
	{
		//can't do anything if there's nothing to tokenize...
		if(line == "")
			return false;

		//split the read line into tokens
		char[] delimiters = {' '};
		string[] tokens = line.Split(delimiters);

		//error -- not enough tokens
		if(tokens.Length < target.Length)
			return false;

		//for each element in the target array...
		for(int i = 0; i < target.Length; ++i)
		{
			//parse it and set the target's value (true if value was '1')
			target[i] = int.Parse(tokens[i]) == 1;
		}

		return true;
	}
}
