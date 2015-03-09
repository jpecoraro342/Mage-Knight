using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GUIText stats; 
	public float turnSmoothing = 5f;
	public float speed = 10f;
	public float jumpSpeed = 10f;

	float gravity = 19.8f;

	//CharacterController playerCharacterController;
	Rigidbody playerRigidBody;
	Animator animator;
	Vector3 moveDirection;
	Vector3 movePosition;
	Vector3 targetDirection;

	Vector3 previousPosition;
	Vector3 currentPosition;
	Vector3 targetVelocity;
	Vector3 currentVelocity;

	static string AnimatorSpeed = "Speed";
	static string AnimatorTurn = "IsTurning";
	static string AnimatorJump = "Jump";
	
	static string JumpButton = "Jump";

	bool jumpPressed = false;
	bool canJump = true;
	bool grounded = true;

	void Awake() {
		//playerCharacterController = GetComponent<CharacterController>();
		playerRigidBody = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator>();

		animator.SetFloat(AnimatorSpeed, 0);
		stats.text = "Stats: ";
	}
	
	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		moveDirection = Vector3.zero;
		movePosition = Vector3.zero;
		previousPosition = currentPosition;
		currentPosition = transform.position;

		targetDirection.Set (h, 0f, v);
		targetVelocity = targetDirection * speed;

		if (h != 0 || v != 0) {
			ApplyRotation ();
		}

		ApplyMovement();
	

		//ApplyJumping ();

		//PerformMovement();

		ApplyAnimations();

		UpdateStats(h, v);
	}

	void ApplyRotation ()
	{

		Quaternion currentRotation = playerRigidBody.rotation;
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp (playerRigidBody.rotation, targetRotation, 10f * Time.fixedDeltaTime);
		playerRigidBody.MoveRotation (newRotation);

		//Vector3 targetDirection = horizontal * Vector3.right + vertical * Vector3.forward;	
		//moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
	}

	/* Makes Adjustments to the Move Vector in the X and Y Direction */
	void ApplyMovement (){
		if (grounded) {
			Vector3 velocity = playerRigidBody.velocity;
			Vector3 velocityChange = targetVelocity - velocity;
			
			velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, speed);
			velocityChange.y = 0;
			
			playerRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
			//playerRigidBody.velocity = velocityChange;

			if (canJump && Input.GetButtonDown("Jump")) {
				playerRigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				grounded = false;
				animator.SetBool("Jump", true);
				canJump = false;
			}
		}

		else{
				Vector3 velocity = playerRigidBody.velocity;
				Vector3 velocityChange = targetVelocity - velocity;
				
				velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
				velocityChange.z = Mathf.Clamp(velocityChange.z, -speed, speed);
				velocityChange.y = 0;

				velocityChange *= 0.1f;

				playerRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
				
		}
		playerRigidBody.AddForce(new Vector3 (0, -gravity * playerRigidBody.mass, 0));

		//if (Input.GetButtonUp ("Jump")) canJump = true;
		grounded = false;



		//playerRigidBody.AddForce(targetDirection.normalized * speed);
		/*if (new Vector3 (playerRigidBody.velocity.x, 0f, playerRigidBody.velocity.z).magnitude > 10f) {
			playerRigidBody.velocity = playerRigidBody.velocity.normalized * speed;
		}*/
	}

	void OnCollisionStay(){
		grounded = true;
		animator.SetBool ("Jump", false);
		if (Input.GetButtonUp ("Jump")) canJump = true;
	}

	/*void ApplyJumping () 
	{
		if (Input.GetButtonDown ("Jump")){

			playerRigidBody.velocity.Set(playerRigidBody.velocity.x, jumpSpeed, playerRigidBody.velocity.z);
			canJump = false;
		}
	}*/

	/*void PerformMovement()
	{
		moveDirection = moveDirection.normalized;
		if (moveDirection != Vector3.zero) {
			Quaternion newRotation = Quaternion.LookRotation(moveDirection);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turnSmoothing);
		}
	
		playerRigidBody.MovePosition(moveDirection * Time.deltaTime * speed * Mathf.Min(movePosition.magnitude, 1));

		currentVelocity = playerRigidBody.velocity;
		//currentVelocity = (transform.position - currentPosition) / Time.deltaTime; //dividing by Time.detlaTime introduces underflow-rounding-error!
	}*/

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpSpeed * gravity);
	}

	void ApplyAnimations() 
	{
		animator.SetFloat(AnimatorSpeed, playerRigidBody.velocity.magnitude);

	}   


	/* Debugging */

	void UpdateStats(float horizontal, float vertical) 
	{
		stats.text = "Stats: \n\th = " + horizontal + "\n\tv = " + vertical + "\n\tSpeed = " + currentVelocity.magnitude + "\n\tRigidBody.velocity: " + playerRigidBody.velocity +
			"\n\tGrounded: " + grounded + "\n\tCanJump: " + canJump;
	}
}
