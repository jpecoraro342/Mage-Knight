using UnityEngine;
using System.Collections;

public class PlayerMovement2 : MonoBehaviour {

	public GUIText stats; 
	public float turnSmoothing = 5f;
	public float speed = 10f;
	public float jumpForce = 50f;
	
	Rigidbody playerRigidbody;
	Animator animator;
	Vector3 movement;
	bool jumping;

	static string AnimatorSpeed = "Speed";
	static string AnimatorTurn = "IsTurning";

	void start(){
		stats.text = "Stats: ";
		movement = new Vector3 (0f, 0f, 0f);
		jumping = false;
	}
	
	void Awake() {
		playerRigidbody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		
		animator.SetFloat(AnimatorSpeed, 0);
	}
	
	void FixedUpdate ()
	{
		//Change h to get axis/raw in order to handle/not joystick sensitivity
		//float h = Input.GetAxisRaw("Horizontal");
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		//Rotate (h);
		if (!jumping) {
			if(Input.GetButtonDown ("Jump")){
				jumping = true;
				playerRigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
				//playerRigidbody.velocity += jumpForce * Vector3.up;
			}
		}
		Move(h, v);

		stats.text = "Stats: \n\th = " + h + "\n\tv = " + v + "\n\tAnimatorSpeed = " + (new Vector3(h, 0, v)).normalized.magnitude * speed;
	}
	
	void Move (float horizontal, float vertical){

		movement.Set(horizontal, 0, vertical);
		
		if(horizontal != 0f || vertical != 0f)
		{
			// ... set the players rotation and set the speed parameter to 5.5f.
			Rotate(horizontal, vertical);

			//playerRigidbody.velocity = transform.forward * movement.normalized.magnitude * speed;
			playerRigidbody.position = playerRigidbody.position + (movement * movement.normalized.magnitude * speed * Time.deltaTime);
			animator.SetFloat(AnimatorSpeed, movement.magnitude * speed);
		}
		else
			// Otherwise set the speed parameter to 0.
			animator.SetFloat(AnimatorSpeed, 0);

	}

	void OnCollisionStay(){
		jumping = false;
	}

	void Rotate (float horizontal, float vertical)
	{
		// Create a new vector of the horizontal and vertical inputs.
		Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
		
		// Create a rotation based on this new vector assuming that up is the global y axis.
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
		
		// Create a rotation that is an increment closer to the target rotation from the player's rotation.
		Quaternion newRotation = Quaternion.Lerp(playerRigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		
		// Change the players rotation to this new rotation.
		playerRigidbody.MoveRotation(newRotation);
	}
}
