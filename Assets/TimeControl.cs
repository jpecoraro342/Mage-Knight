using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q)){
			Time.timeScale -= .25f;
		}
		if(Input.GetKeyDown(KeyCode.E)){
			Time.timeScale += .25f;
		}
	}
}
