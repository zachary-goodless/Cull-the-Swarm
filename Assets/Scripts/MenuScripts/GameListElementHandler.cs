
using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameListElementHandler : MonoBehaviour
{
	//PUBLIC
	//

	//PRIVATE
	private string mName;

	private LoadGameMenu mLoadMenuEventHandler;
	private DeleteGameMenu mDeleteMenuEventHandler;

//--------------------------------------------------------------------------------------------

	public void Start()
	{
		//set mLoadMenuEventHandler and mDeleteMenuEventHandler
		mLoadMenuEventHandler = transform.parent.parent.GetComponentInParent<LoadGameMenu>();
		mDeleteMenuEventHandler = transform.parent.parent.GetComponentInParent<DeleteGameMenu>();
	}

//--------------------------------------------------------------------------------------------

	public void handleButtonClicked()
	{
		//this button belongs to the load menu
		if(mLoadMenuEventHandler != null)
		{
			mLoadMenuEventHandler.handleElementButtonClicked(mName);
		}

		//else this belongs to the delete menu
		else if(mDeleteMenuEventHandler != null)
		{
			mDeleteMenuEventHandler.handleElementButtonClicked(mName);
		}

		//disable the button
		GetComponentInChildren<Button>().interactable = false;
	}

//--------------------------------------------------------------------------------------------

	//getter-setter for the name
	public string getName(){ return mName; }
	public void setName(string n)
	{
		mName = n;

		//for each text component in this list element...
		Text[] texts = gameObject.GetComponentsInChildren<Text>();
		foreach(Text t in texts)
		{
			//if it is the label text...
			if(t.gameObject.name == "ElementText")
			{
				//set its text
				t.text = mName;
				break;
			}
		}
	}
}
