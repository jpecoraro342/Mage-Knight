using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
	GameObject player;
	Animator animator;
	NavMeshAgent agent;
	MonsterStats stats;

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
			}
		}
	}

	public void TakeDamage(int damage){
		stats.currentHealth -= damage;
	}
}
