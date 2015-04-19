using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
	public float speed = 10f;
	//public float maxHP = 50f;
	public float currentHP;
	public float jumpForce;

	bool grounded = true;
	float jumpTime;

	Animator animator;
	Rigidbody rigidbody;
	NavMeshAgent navmeshAgent;
	GameObject player;
	//GUIText statsText;

	float atkTime = 0f;

	OffMeshLinkData jumpLink;

	bool traversingLink = false;
	MonsterStats stats;


	void Awake(){
		stats = GetComponent<MonsterStats> ();
		animator = GetComponent<Animator> ();
		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("speed", 0f);

		navmeshAgent = GetComponent<NavMeshAgent> ();
		rigidbody = GetComponent<Rigidbody> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		//statsText = GameObject.FindGameObjectWithTag ("MonsterStats").GetComponent<GUIText>();

	}
	void Start () {
		navmeshAgent.enabled = true;
		//navmeshAgent.destination = player.transform.position;
		navmeshAgent.autoTraverseOffMeshLink = false;
		//this.rigidbody.detectCollisions = false;
		this.rigidbody.isKinematic = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (navmeshAgent.enabled && canSeePlayer ())
			pursuePlayer ();   //for performance, set it to player's position every x seconds instead of every frame?
		animator.SetFloat ("speed", this.navmeshAgent.velocity.magnitude);
		/*statsText.text = "\nnavMeshSpeed: " + this.navmeshAgent.velocity.magnitude + 
			"\n\tnavmeshAgent.isOnOffMeshLink: " + navmeshAgent.isOnOffMeshLink + 
			"\n\ttraversingLink: " + this.traversingLink + 
				"\n\tRigidbody.iskinematic: " + this.rigidbody.isKinematic + 
				"\n\tnavmeshAgent.active: " + this.navmeshAgent.enabled;*/


		//checkAttack ();

	}

	void pursuePlayer(){
		navmeshAgent.destination = player.transform.position;
		checkJump ();
	}

	bool canSeePlayer(){
		Vector3 heightAdjust = new Vector3 (0, 1, 0);
		Vector3 raySource = this.transform.position + heightAdjust;
		bool result = false;
		Vector3 targetDir = player.transform.position - this.transform.position;
		Vector3 forward = this.transform.forward;
		float angle = Vector3.Angle(targetDir, forward);
		angle = Mathf.Abs (angle);
		if (angle < stats.visionCone && playerDistance () < stats.visionRadius) {
			RaycastHit hit;
			Physics.Raycast (raySource, targetDir, out hit);
			if (hit.collider.transform.root == player.transform.root){
				if(stats.seenPlayer == false){
					stats.visionCone = 360;
					stats.visionRadius = 50;
				}
				stats.seenPlayer = true;

				result = true;
			}
		}
		return result;
	}

	float playerDistance(){
		return (player.transform.position - this.transform.position).magnitude;
	}



	/*void checkAttack(){
		//animator.SetBool ("grabweapon", true);
		if ((player.transform.position - this.transform.position).magnitude < 3 && !stats.isDead) {
			if ((Time.time - atkTime) > 1){
				animator.SetFloat ("random", Random.Range(0, 4));
				atkTime = Time.time;
				animator.SetTrigger ("attacking");
				//if weapon hits player: player.takedamage
			}
			//http://answers.unity3d.com/questions/750785/mecanim-trigger-event-at-end-of-animation-state.html
		}
	}
*/
	void checkJump(){
		if (!this.navmeshAgent.enabled) return;
		if (navmeshAgent.isOnOffMeshLink){
			if (!this.traversingLink && (Time.time - jumpTime) > 0.5){
				//jumpLink = navmeshAgent.currentOffMeshLinkData;
				traversingLink = true;
				navmeshAgent.enabled = false;
				this.rigidbody.detectCollisions = true;
				this.rigidbody.WakeUp();
				this.rigidbody.isKinematic = false;
				this.rigidbody.useGravity = true;
				this.rigidbody.AddRelativeForce(new Vector3(0, jumpForce, jumpForce), ForceMode.Impulse);
				jumpTime = Time.time;
				grounded = false;
				this.animator.SetBool("jump", true);
			}
		}
	}
	/*
	void OnCollisionExit(Collision collisionInfo){
		if (collisionInfo.gameObject.tag == "Ground") {
			this.animator.SetBool("jump", false);
		}
	}
	/*
	void OnCollisionEnter(Collision collisionInfo){
		if (collisionInfo.gameObject.tag == "Ground") {
			if (this.traversingLink) {
				this.traversingLink = false;
				this.rigidbody.isKinematic = true;
				this.rigidbody.detectCollisions = false;
				this.navmeshAgent.enabled = true;
				this.navmeshAgent.CompleteOffMeshLink();
			}
		}
	}
	*/

	void OnCollisionStay(){ 
		if ((Time.time - jumpTime) > 0.1f) {
			if (this.traversingLink) {
				this.animator.SetBool("jump", false);
				this.traversingLink = false;
				this.rigidbody.isKinematic = true;
				this.navmeshAgent.enabled = true;
			}
		}
	}

	void calculateJumpForce(OffMeshLinkData data){
		Vector3 disp = data.endPos - data.startPos;
		float xzDisp = Mathf.Sqrt ((disp.x * disp.x) + (disp.z * disp.z));
		float yDisp = disp.y;
		float velocity = (float)(xzDisp * (1 + (0.5 * (-9.8) * xzDisp))) / yDisp;

		velocity = Mathf.Abs (velocity);

		float forceY = Mathf.Sqrt (velocity);
		float forceX = Mathf.Sqrt ((xzDisp * xzDisp) - (disp.z * disp.z)) * velocity;
		float forceZ = Mathf.Sqrt ((xzDisp * xzDisp) - (disp.x * disp.x)) * velocity;

		Vector3 dispN = disp.normalized;

		Debug.Log ("Jump Info: \n\tdisplacement: " + disp.ToString() + 
		           "\n\txzDisp :" + xzDisp + 
		           "\n\tyDisp : " + yDisp + 
		           "\n\tCalculated Velocity: " + velocity + 
		           "\n\tForceX: " + forceX + 
		           "\n\tForceY: " + forceY + 
		           "\n\tForceZ: " + forceZ + 
		           "\n\tdispN: " + dispN);

	}

	//equation for projectile motion: 
		//xDisp = xIn + vIn(t)
		//yDisp = yIn + vIn(t) - 0.5Gt^2
		// v = xDisp (1 + 0.5(9.8) * xDisp) / yDisp
		// F = m(v^2) / 2
		//disp.x = sqrt(xzDisp^2 - disp.z^2)
}
