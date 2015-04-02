using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class will handle all of the attacking for the player
 * The players chosen attacks will be saved here, and are mapped to the different buttons. Each weapon has attacks 1-4
 * Appropriate animations will be available for each of the attacks
 * 
 * Once an attack button is pressed, This script will check the AltAttack button, and then perform the attack with the appropriate weapon
 * 
 * If an enemy is struck (some form of trigger or something will be used) the player will take away health from that enemy
 * 
 */
public class PlayerAttacking : MonoBehaviour {

	Animator animator;

	public CapsuleCollider swordCollider;
	public GameObject mageAttack1Clone;

	LinkedList<GameObject> enemyTargetList;

	//Button Mappings
	static string Attack1 = "Attack1";
	static string Attack2 = "Attack2";
	static string Attack3 = "Attack3";
	static string Attack4 = "Attack4";

	static string AlternateAttack = "AltAttack";

	//Attack Bools
	bool isAttacking = false;
	bool canAttack = true;

	bool altAttackPressed = false;

	bool meleeAttacksActive = true;
	bool mageAttacksActive = false;

	//Attack Times Note: These really shouldnt be static..
	float mageAttack1Distance = 20f;
	
	float Attack1Time = .8f;
	float Attack1AnimationTime = 1.05f;
	
	float MageAttack1Time = .8f;
	float MageAttack1AnimationTime = 1f;

	int BaseLayerIndex = 0;
	int MeleeAttackLayerIndex = 1;
	int MageAttackLayerIndex = 2;



	void Awake () {
		animator = GetComponent<Animator>();
		enemyTargetList = new LinkedList<GameObject>();
	}

	void Update () {
		if (Input.GetButton(AlternateAttack) && !altAttackPressed) {
			altAttackPressed = true;
			meleeAttacksActive = !meleeAttacksActive;
			mageAttacksActive = !mageAttacksActive;
		}
		else if (!Input.GetButton(AlternateAttack)) {
			altAttackPressed = false;
		}


		if (Input.GetButton(Attack1) && !isAttacking && canAttack) {
			isAttacking = true;
			animator.SetTrigger(Attack1);

			if (meleeAttacksActive) {
				Debug.Log("Melee Attack!");
				StartCoroutine(StartMeleeAttack(Attack1Time, Attack1AnimationTime));
			}
			else {
				Debug.Log("Mage Attack!");
				StartCoroutine(StartMageAttack1());
			}
		}
		else if (!Input.GetButton(Attack1)) {
			canAttack = true;
		}
	}

	public void MeleeTrigger(GameObject enemy) {
		Debug.Log("Deal Damage");
	}

	public void OnTriggerEnter(Collider other) {
		GameObject triggerObject = other.gameObject;
		if (triggerObject.tag == "Enemy") {
			Debug.Log("Enter");
			enemyTargetList.AddLast(other.gameObject);
		}
	}

	public void OnTriggerExit(Collider other) {
		GameObject triggerObject = other.gameObject;
		if (triggerObject.tag == "Enemy") {
			Debug.Log("Exit");
			enemyTargetList.Remove(other.gameObject);
		}
	}

	Vector3 getTargetTransformDirection() {
		Vector3 closest = transform.forward;
		float closestDistance = float.MaxValue;

		foreach (GameObject enemy in enemyTargetList) {
			Vector3 enemyPosition = enemy.transform.position;
			float distance = Vector3.Distance(transform.position, enemyPosition);
			if (distance < closestDistance) {
				closest = enemyPosition;
				closestDistance = distance;
			}
		}

		//We did not change, just use forward position
		if (closestDistance != float.MaxValue) {
			float oldY = closest.y;
			closest = closest - transform.position;
			closest.y = oldY + .5f;
		}

		return closest;
	}

	//Attack Enumerators

	IEnumerator StartMeleeAttack(float attackTime, float animationTime) {
		yield return null;
		animator.SetLayerWeight(MeleeAttackLayerIndex, 1);
		swordCollider.enabled = true;
		yield return new WaitForSeconds(attackTime);
		swordCollider.enabled = false;

		yield return new WaitForSeconds(animationTime - attackTime);
		animator.SetLayerWeight(MeleeAttackLayerIndex, 0);

		isAttacking = false;
	}

	IEnumerator StartMageAttack1() {
		yield return null;

		RaycastHit hit;
		Vector3 attackTransform = transform.position;
		bool targetFound = false;


//		int xoffset = -2;
//		for (int i = 0; i < 5; i++) {
//			Vector3 raycastVect = transform.position;
//			raycastVect.x += xoffset;
//			if (Physics.Raycast(raycastVect, transform.forward, out hit, mageAttack1Distance)) {
//				Debug.DrawRay(transform.position, hit.point, Color.green, 2);
//				attackTransform = hit.point;
//				break;
//			}
//		}

		attackTransform.y = transform.position.y + .5f;

		GameObject attack = (GameObject)Instantiate (mageAttack1Clone, attackTransform, Quaternion.identity);
		attack.transform.parent = gameObject.transform.parent;

		Vector3 Direction = getTargetTransformDirection();
		Debug.Log(transform.forward +  ": " + Direction);
		attack.transform.forward = Direction;


		yield return new WaitForSeconds(MageAttack1Time);

		//This is handled in the objects class itself
		//Destroy(attack.gameObject);

		isAttacking = false;
	}
}
