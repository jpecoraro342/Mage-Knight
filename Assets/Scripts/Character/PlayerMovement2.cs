using UnityEngine;
using System.Collections;

public class PlayerMovement2 : MonoBehaviour {
	
	public float turnSpeed = 20f;
	public float turnSmoothing = 5f;

	public float speedDampTime = 0.1f;
	
	public float speed = 10f;
	public float JumpForce = 50f;
	
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
		//float h = Input.GetAxisRaw("Horizontal");
		float h = Input.GetAxisRaw("Horizontal");
		
		float v = Input.GetAxis("Vertical");
		
		//Rotate (h);
		Move(h, v);
		if (Input.GetButton ("Jump"))
						playerRigidbody.AddForce (new Vector3 (0, JumpForce, 0));
	}
	
	void Move (float horizontal, float vertical)
	{
		Vector3 movement = new Vector3(horizontal, 0, vertical);
		
		if(horizontal != 0f || vertical != 0f)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotate(horizontal, vertical);
			//playerRigidbody.MovePosition(transform.position);
			//playerRigidbody.AddForce(transform.forward * movement.magnitude * speed);
			//playerRigidbody.AddForce(transform.forward  * speed);
			playerRigidbody.velocity = transform.forward * movement.magnitude * speed;
			animator.SetFloat(AnimatorSpeed, movement.magnitude, speedDampTime, Time.deltaTime);
		}
		else
			// Otherwise set the speed parameter to 0.
			animator.SetFloat(AnimatorSpeed, 0);
	}
	
	void Rotate (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		rigidbody.MoveRotation(newRotation);
	}
}
