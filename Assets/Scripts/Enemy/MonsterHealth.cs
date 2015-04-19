using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
	GameObject player;
	Animator animator;
	NavMeshAgent agent;
	MonsterStats stats;
	public Transform healthOrb;
	private float deathTime;

	private Rigidbody rigid;

	// Use this for initialization
	void Start () {
		stats = GetComponent<MonsterStats> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (stats.currentHealth <= 0) {
			agent.enabled = false;
			if (!stats.isDead){
				animator.SetTrigger("dead");
				stats.isDead = true;
				deathTime = Time.time;
				//if (Random.value < 0.75){
				Instantiate(healthOrb, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
				//}
			}
			if (stats.isDead && Time.time - deathTime > 5) {
				this.transform.root.Translate (new Vector3(0,-0.1f,0) * Time.deltaTime);
			}
			
			if (stats.isDead && Time.time - deathTime > 13) {
				DestroyObject (this.gameObject);
			}
		}
	}

	public void TakeDamage(float damage) {
		stats.currentHealth -= damage;
	}
}
