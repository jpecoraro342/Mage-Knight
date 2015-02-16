using UnityEngine;
using System.Collections;

public class SphereJumpScript : MonoBehaviour {

	public float jumpForce;
	public float additionalJumpForce;
	public float moveSpeed;
	public GUIText stats;

	Rigidbody sphereRigidBody;
	bool jumping;
	Vector3 movement;
	bool canHoldJump;
	Vector3 additionalForce;

	// Use this for initialization
	void Start () {
		sphereRigidBody = GetComponent<Rigidbody> ();
		jumping = false;
		canHoldJump = true;
		movement = new Vector3 ();
		additionalForce = Vector3.up * additionalJumpForce;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(jumping == false){
			if (Input.GetButtonDown ("Jump")){
				jumping = true;
				sphereRigidBody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		if(Input.GetButton ("Jump") && canHoldJump){
			//Vector3 additionalForce = Vector3.up * additionalJumpForce;
			sphereRigidBody.AddForce (additionalForce);
			//additionalForce *= 0.9f;
		}
		stats.text = jumping.ToString()+ "\n" + canHoldJump.ToString();

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		move (h, v);	

	}
	void Update(){
		if(Input.GetButtonUp ("Jump")) canHoldJump = false;
	}

	void move(float h, float v){
		movement.Set (h, 0f, v);

		sphereRigidBody.position += movement.normalized * moveSpeed * Time.deltaTime;
	}


	void OnCollisionEnter(Collision col){
		if (col.transform.tag == "Ground") {
			jumping = false;
			canHoldJump = true;
			additionalForce = Vector3.up * additionalJumpForce;
		}
	}
}
