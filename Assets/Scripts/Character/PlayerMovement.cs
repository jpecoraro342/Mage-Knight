using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float turnSpeed = 20f;
	public float turnSmoothing = 5f;

	public float speed = 10f;

	Rigidbody playerRigidbody;

	Animator animator;

	static string AnimatorSpeed = "Speed";
	static string AnimatorTurn = "IsTurning";
	
	void Awake() {
		playerRigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();

		animator.SetFloat(AnimatorSpeed, 0);
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
		animator.SetFloat(AnimatorSpeed, playerRigidbody.velocity.magnitude);
	}
	
	void Rotate (float horizontal)
	{
		//The turning transition isn't very good right now, that's why it's commented out 

		if (horizontal == 0) {
			//animator.SetBool(AnimatorTurn, false);
			return;
		}

		//animator.SetBool(AnimatorTurn, true);

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
