using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GUIText stats; 
	public float turnSmoothing = 5f;
	public float speed = 10f;
	public float jumpSpeed = 10f;

	CharacterController playerCharacterController;
	Animator animator;
	Vector3 moveDirection;
	Vector3 movePosition;

	Vector3 previousPosition;
	Vector3 currentPosition;

	Vector3 currentVelocity;

	static string AnimatorSpeed = "Speed";
	static string AnimatorTurn = "IsTurning";
	static string AnimatorJump = "Jump";
	
	static string JumpButton = "Jump";

	bool jumpPressed = false;
	bool isJumping = false;

	void Awake() {
		playerCharacterController = GetComponent<CharacterController>();
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
		
		ApplyRotation (h, v);
		ApplyMovement(h, v);
		ApplyJumping ();

		PerformMovement();

		ApplyAnimations();

		UpdateStats(h, v);
	}

	void ApplyRotation (float horizontal, float vertical)
	{
		Vector3 targetDirection = horizontal * Vector3.right + vertical * Vector3.forward;	

		moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
	}

	/* Makes Adjustments to the Move Vector in the X and Y Direction */
	void ApplyMovement (float horizontal, float vertical){
		movePosition = new Vector3(horizontal, 0 , vertical);
	}

	void ApplyJumping () 
	{
		if (Input.GetButtonDown ("Jump")){

			playerCharacterController.velocity.Set(playerCharacterController.velocity.x, jumpSpeed, playerCharacterController.velocity.z);
			isJumping = true;
		}
	}

	void PerformMovement()
	{
		moveDirection = moveDirection.normalized;
		if (moveDirection != Vector3.zero) {
			Quaternion newRotation = Quaternion.LookRotation(moveDirection);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turnSmoothing);
		}
	
		playerCharacterController.Move(moveDirection * Time.deltaTime * speed * Mathf.Min(movePosition.magnitude, 1));

		currentVelocity = playerCharacterController.velocity;
		//currentVelocity = (transform.position - currentPosition) / Time.deltaTime; //dividing by Time.detlaTime introduces underflow-rounding-error!
	}

	void ApplyAnimations() 
	{
		animator.SetFloat(AnimatorSpeed, currentVelocity.magnitude);

	}

	/* Debugging */

	void UpdateStats(float horizontal, float vertical) 
	{
		stats.text = "Stats: \n\th = " + horizontal + "\n\tv = " + vertical + "\n\tSpeed = " + currentVelocity.magnitude;
	}
}
