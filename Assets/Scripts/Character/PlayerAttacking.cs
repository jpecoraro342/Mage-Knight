using UnityEngine;
using System.Collections;

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

	float Attack1Time = .6f;

	static string Attack1 = "Attack1";
	static string Attack2 = "Attack2";
	static string Attack3 = "Attack3";
	static string Attack4 = "Attack4";

	static string AlternateAttack = "AltAttack";
	
	bool isAttacking = false;
	bool canAttack = true;

	bool altAttackPressed = false;

	void Awake () {
		animator = GetComponent<Animator>();
	}

	void Update () {
		if (Input.GetButton(AlternateAttack) && !altAttackPressed) {

		}
		else if (!Input.GetButton(AlternateAttack)) {

		}


		if (Input.GetButton(Attack1) && !isAttacking && canAttack) {
			Debug.Log("Attack!");
			isAttacking = true;
			animator.SetTrigger(Attack1);
			StartCoroutine(StartAttacking(Attack1Time));
		}
		else if (!Input.GetButton(Attack1)) {
			canAttack = true;
		}
	}

	public void MeleeTrigger(GameObject enemy) {
		Debug.Log("Deal Damage");
	}

	IEnumerator StartAttacking(float attackTime) {
		yield return null;
		swordCollider.enabled = true;
		yield return new WaitForSeconds(attackTime);
		swordCollider.enabled = false;
		isAttacking = false;
	}
}
