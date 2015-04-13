using UnityEngine;
using System.Collections;

public class SwordTrigger : MonoBehaviour {

	static string EnemyTag = "Enemy";
	static string BossTag = "Boss";

	public PlayerAttacking playerAttackScript;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == EnemyTag || other.gameObject.tag == BossTag) {
			playerAttackScript.MeleeTrigger(other.gameObject);
		}
	}
}
