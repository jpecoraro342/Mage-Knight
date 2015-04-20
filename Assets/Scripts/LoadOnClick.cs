using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour 
{
	//public GameObject canvas;
	public void LoadScene(int level)
	{
		Application.LoadLevel (level);
	}

	public void SwitchCanvas(bool main)
	{
		Canvas mainMenu = (Canvas) GameObject.FindGameObjectWithTag("MainMenu").GetComponent ("Canvas");
		Canvas credits = (Canvas) GameObject.FindGameObjectWithTag("Credits").GetComponent ("Canvas");
		if (main) {
			mainMenu.enabled = false;
			credits.enabled = true;
		} else {
			mainMenu.enabled = true;
			credits.enabled = false;
		}
	}
}
