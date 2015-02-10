using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float turnSpeed = 20f;
	public float turnSmoothing = 5f;

	public float speed = 10f;

	Rigidbody playerRigidbody;
	
	void Awake() {
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		//Change h to get axis/raw in order to handle/not joystick sensitivity
		float h = Input.GetAxisRaw("Horizontal");

		float v = Input.GetAxis("Vertical");

		Rotate (h);
		Move(v);
	}

	void Move (float vertical)
	{
		Vector3 movement = new Vector3(0, 0, vertical);
		
		//Need to normalize the vector so that the player always moves at the same rate
		movement = movement.normalized * speed * Time.deltaTime;

		
		//moves the player to the new position
		playerRigidbody.MovePosition(transform.position);

		playerRigidbody.velocity = transform.forward * vertical * speed;
	}
	
	void Rotate (float horizontal)
	{
		if (horizontal == 0) {
			return;
		}

		//Current Rotation
		Quaternion currentRotation = playerRigidbody.rotation;

		//Axis Rotation
		Quaternion axisRotation = Quaternion.Euler(0, horizontal*turnSpeed, 0);

		//New Rotation
		Quaternion newRotation = (currentRotation * axisRotation);

		newRotation = Quaternion.Lerp(currentRotation, newRotation, turnSmoothing * Time.deltaTime);

		// Change the players rotation to this new rotation.
		playerRigidbody.MoveRotation(newRotation);
	}
}
