
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadGameMenu : MonoBehaviour
{
	//PUBLIC
	//

	//PRIVATE
	private SavedGameManager mSavedGameManager;

//--------------------------------------------------------------------------------------------

	void Start()
	{
		mSavedGameManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavedGameManager>();

		//TODO
	}

//--------------------------------------------------------------------------------------------
}
