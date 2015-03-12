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

	static string AttackButton1 = "Attack1";
	static string AttackButton2 = "Attack2";
	static string AttackButton3 = "Attack3";
	static string AttackButton4 = "Attack4";

	static string AlternateAttackButton = "AltAttack";
	
	void Awake () {
	
	}

	void Update () {
	
	}
}
