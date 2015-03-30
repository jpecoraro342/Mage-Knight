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

	float Attack1Time = .8f;
	float Attack1AnimationTime = 1.05f;

	float MageAttack1Time = .8f;
	float MageAttack1AnimationTime = 1f;

	static string Attack1 = "Attack1";
	static string Attack2 = "Attack2";
	static string Attack3 = "Attack3";
	static string Attack4 = "Attack4";

	static string AlternateAttack = "AltAttack";
	
	bool isAttacking = false;
	bool canAttack = true;

	bool altAttackPressed = false;

	bool meleeAttacksActive = true;
	bool mageAttacksActive = false;

	int BaseLayerIndex = 0;
	int MeleeAttackLayerIndex = 1;
	int MageAttackLayerIndex = 2;

	void Awake () {
		animator = GetComponent<Animator>();
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

		yield return new WaitForSeconds(MageAttack1Time);

		isAttacking = false;
	}
}
