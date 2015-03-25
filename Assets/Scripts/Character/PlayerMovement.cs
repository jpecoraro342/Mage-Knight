using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public GUIText stats; 
	public float turnSmoothing = 10f;
	public float speed = 10f;
	public float targetJumpHeight = 3f;

	CharacterController playerCharacterController;
	Animator animator;
	Vector3 horizontalMoveDirection;
	Vector3 horizontalMovePosition;

	Vector3 previousPosition;
	Vector3 currentPosition;

	Vector3 horizontalVelocity;
	float verticalVelocity;

	float gravity = 30f;

	static string AnimatorSpeed = "Speed";
	static string AnimatorTurn = "IsTurning";
	static string AnimatorJump = "Jump";
	
	static string JumpButton = "Jump";

	bool canJump = true;

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

		horizontalMoveDirection = Vector3.zero;
		horizontalMovePosition = Vector3.zero;
		previousPosition = currentPosition;
		currentPosition = transform.position;
		
		ApplyRotation (h, v);
		ApplyMovement(h, v);
		ApplyJumping ();
		ApplyGravity();
		PerformMovement();

		ValuesAndStats();
		ApplyAnimations();

		UpdateStats(h, v);
	}

	void ApplyRotation (float horizontal, float vertical)
	{
		Vector3 targetDirection = horizontal * Vector3.right + vertical * Vector3.forward;	

		horizontalMoveDirection = Vector3.RotateTowards(horizontalMoveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
	}

	/* Makes Adjustments to the Move Vector in the X and Y Direction */
	void ApplyMovement (float horizontal, float vertical){
		horizontalMovePosition = new Vector3(horizontal, 0 , vertical);
	}

	void ApplyJumping () 
	{
		if (Input.GetButton(JumpButton) && canJump) {
			verticalVelocity = Mathf.Sqrt(2 * targetJumpHeight * gravity);
			animator.SetTrigger(AnimatorJump);
			canJump = false;
		}
		else {

		}
	}

	void ApplyGravity ()
	{
		verticalVelocity -= gravity * Time.deltaTime;
	}

	void PerformMovement()
	{
		horizontalMoveDirection = horizontalMoveDirection.normalized;
		if (horizontalMoveDirection != Vector3.zero) {
			Quaternion newRotation = Quaternion.LookRotation(horizontalMoveDirection);
			transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * turnSmoothing);
		}

		horizontalMoveDirection = horizontalMoveDirection * Time.deltaTime * speed * Mathf.Min(horizontalMovePosition.magnitude, 1);
		horizontalMoveDirection.y = verticalVelocity * Time.deltaTime;
		playerCharacterController.Move(horizontalMoveDirection);

	}

	void ValuesAndStats()
	{
		Vector3 positionDifference =  transform.position - currentPosition;

		float verticalDifference = positionDifference.y;

		if (verticalDifference == 0) {
			canJump = true;
			verticalVelocity = 0;
		}

		horizontalVelocity = (positionDifference / Time.deltaTime);
		horizontalVelocity.y = 0;
	}

	void ApplyAnimations() 
	{
		animator.SetFloat(AnimatorSpeed, horizontalVelocity.magnitude);
	}

	/* Debugging */

	void UpdateStats(float horizontal, float vertical) 
	{
		stats.text = "Stats: \n\th = " + horizontal + "\n\tv = " + vertical + "\n\tMovement = " + horizontalMoveDirection + "\n\tHorizontal Speed = " + horizontalVelocity.magnitude + "\n\tVertical Speed = " + verticalVelocity;
	}
}