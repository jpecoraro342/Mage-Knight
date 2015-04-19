using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject pauseMenu;

	static string pauseButton = "Pause";

	bool isPausePressed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(pauseButton) && !isPausePressed) {
			//Pause
			Debug.Log("Pause Pressed");
			isPausePressed = true;
			Time.timeScale = 0f;
			pauseMenu.SetActive(true);
		}
		else if (Input.GetButtonDown(pauseButton) && isPausePressed) {
			//Resume
			Debug.Log("Resume Now");
			unPause();
		}
	}

	public void restartLevel() {
		Application.LoadLevel(Application.loadedLevel);
		Time.timeScale = 1f;
	}

	public void exitGame() {

	}

	public void unPause() {
		isPausePressed = false;
		Time.timeScale = 1f;
		pauseMenu.SetActive(false);
	}
}
