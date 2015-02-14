using UnityEngine;
using System.Collections;

public class SphereJumpScript : MonoBehaviour {

	public float jumpForce;

	Rigidbody sphereRigidBody;
	bool jumping;

	// Use this for initialization
	void Start () {
		sphereRigidBody = GetComponent<Rigidbody> ();
		jumping = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!jumping){
			if (Input.GetButtonDown ("Jump")){
				sphereRigidBody.AddForce (Vector3.up * jumpForce);
			}
		}
	}
	void OnCollisionStay(){
		jumping = false;
	}
}
