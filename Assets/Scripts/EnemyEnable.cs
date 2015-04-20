using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyEnable : MonoBehaviour {

	public List<GameObject> objectsToEnable = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			enableObjects();
		}
	}

	void enableObjects() {
		foreach (GameObject objectToEnable in objectsToEnable) {
			objectToEnable.SetActive(true);
		}
	}
}
