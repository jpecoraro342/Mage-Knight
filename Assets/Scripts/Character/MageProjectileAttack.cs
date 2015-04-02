using UnityEngine;
using System.Collections;

public class MageProjectileAttack : MonoBehaviour {

	public float speed = 2f;
	public float projectileLife = 4f;

	void Start () {
		StartCoroutine(DelayDestroy(projectileLife));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward.normalized * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			Debug.Log("Deal Damage");
			//Start a fire particle system on the enemy to show damage dealt to them. 
			//Deal the damage

			//Destroy or keep going?
			//StartCoroutine(DelayDestroy(.2f));
		}
	}

	IEnumerator DelayDestroy(float time) {
		yield return null;
		yield return new WaitForSeconds(time);
		
		Destroy(gameObject);
	}
}
