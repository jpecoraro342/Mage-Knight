using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MageExplosiveAttack : MonoBehaviour {

	static string EnemyTag = "Enemy";
	static string BossTag = "Boss";

	public float startTime = 0f; //For the particle system  
	public float projectileLife = 1f;

	public float damage = 13;
	public float damageMultiplier = 1;
	
	public GameObject damageObject;
	public ParticleSystem attackSystem;

	LinkedList<GameObject> enemyTargetList;

	bool hasDealtDamage = false;
	
	void Start () {
		enemyTargetList = new LinkedList<GameObject>();

		attackSystem.Simulate(startTime, true, true);
		attackSystem.Play();

		StartCoroutine(DelayDestroy(projectileLife));
	}
	
	// Update is called once per frame
	void Update () {
		if (attackSystem.time > 1.8 && !hasDealtDamage) {
			DealDamage();
			hasDealtDamage = true;
		}
	}

	void DealDamage() {
		foreach (GameObject enemy in enemyTargetList) {
			//deal damge
			Debug.Log("Deal Damage");
			Vector3 damageSystemPosition = Vector3.zero;
			damageSystemPosition.y += .5f;
			
			GameObject damageSystem = (GameObject)Instantiate (damageObject);
			damageSystem.transform.parent = enemy.transform;
			
			damageSystem.transform.localPosition = damageSystemPosition;
			damageSystem.transform.localScale = new Vector3(1, 1, 1);

			//TODO: Deal Damage
		}
	}
	
	public void OnTriggerEnter(Collider other) {
		GameObject triggerObject = other.gameObject;
		if (triggerObject.tag == EnemyTag || triggerObject.tag == BossTag) {
			Debug.Log("Enter");
			enemyTargetList.AddLast(other.gameObject);
		}
	}
	
	public void OnTriggerExit(Collider other) {
		GameObject triggerObject = other.gameObject;
		if (triggerObject.tag == EnemyTag || triggerObject.tag == BossTag) {
			Debug.Log("Exit");
			enemyTargetList.Remove(other.gameObject);
		}
	}
	
	IEnumerator DelayDestroy(float time) {
		yield return null;
		yield return new WaitForSeconds(time);
		
		Destroy(gameObject);
	}
}
