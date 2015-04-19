using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * 15, Space.World);
	}
}
