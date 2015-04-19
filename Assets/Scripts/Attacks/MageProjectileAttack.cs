using UnityEngine;
using System.Collections;

public class MageProjectileAttack : MonoBehaviour {

	public float speed = 2f;
	public float projectileLife = 1f;

	public float damage = 10;
	public float damageMultiplier = 1;

	public GameObject damageObject;

	void Start () {
		StartCoroutine(DelayDestroy(projectileLife));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other) {
		GameObject otherobj = other.gameObject;
		if (otherobj.tag == "Enemy") {
			Debug.Log("Deal Damage");

			otherobj.GetComponent<MonsterHealth>().TakeDamage(20);

			Vector3 damageSystemPosition = Vector3.zero;
			damageSystemPosition.y += .5f;

			GameObject damageSystem = (GameObject)Instantiate (damageObject);
			damageSystem.transform.parent = otherobj.transform;

			damageSystem.transform.localPosition = damageSystemPosition;
			damageSystem.transform.localScale = new Vector3(1, 1, 1);

			MonsterHealth health = otherobj.GetComponent<MonsterHealth>();
			if (health != null) {
				health.TakeDamage(damage*damageMultiplier);
			}
		}
	}

	IEnumerator DelayDestroy(float time) {
		yield return null;
		yield return new WaitForSeconds(time);
		
		Destroy(gameObject);
	}
}
