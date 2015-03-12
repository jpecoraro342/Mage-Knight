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

	static string Attack1 = "Attack1";
	static string Attack2 = "Attack2";
	static string Attack3 = "Attack3";
	static string Attack4 = "Attack4";

	static string AlternateAttac = "AltAttack";

	bool isAttacking = false;
	
	void Awake () {
		animator = GetComponent<Animator>();
	}

	void Update () {
		if (Input.GetButton(Attack1) && !isAttacking) {
			Debug.Log("Attack!");
			isAttacking = true;
			animator.SetTrigger(Attack1);
			StartCoroutine(StartAttacking(.6f));
		}
	}

	IEnumerator StartAttacking(float attackTime) {
		yield return null;
		yield return new WaitForSeconds(attackTime);
		isAttacking = false;
	}
}
