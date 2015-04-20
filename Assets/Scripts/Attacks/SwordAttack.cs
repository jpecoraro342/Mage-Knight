using UnityEngine;
using System.Collections;

public class SwordAttack : MonoBehaviour {

	public float damage = 80;
	public float damageMultiplier = 1;

	public GameObject damageObject;

	static string EnemyTag = "Enemy";
	static string BossTag = "Boss";

	void OnTriggerEnter(Collider other) {
		GameObject otherObject = other.gameObject;
		if (isEnemy(otherObject)) {
			Vector3 damageSystemPosition = Vector3.zero;
			damageSystemPosition.y += .5f;
			
			GameObject damageSystem = (GameObject)Instantiate (damageObject);
			damageSystem.transform.parent = otherObject.transform;
			
			damageSystem.transform.localPosition = damageSystemPosition;
			damageSystem.transform.localScale = new Vector3(1, 1, 1);

			MonsterHealth health = otherObject.GetComponent<MonsterHealth>();
			if (health != null) {
				health.TakeDamage(damage*damageMultiplier);
			}
		}
	}

	bool isEnemy(GameObject gameObject) {
		return gameObject.tag == EnemyTag || gameObject.tag == BossTag;
	}
}
