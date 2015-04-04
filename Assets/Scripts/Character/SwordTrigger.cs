using UnityEngine;
using System.Collections;

public class SwordTrigger : MonoBehaviour {

	public PlayerAttacking playerAttackScript;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			playerAttackScript.MeleeTrigger(other.gameObject);
		}
	}
}
