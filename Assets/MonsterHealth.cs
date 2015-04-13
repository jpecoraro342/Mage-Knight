using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
	public int maxHealth = 50;
	int currentHealth = maxHealth;
	GameObject player;
	Animator animator;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			agent.enabled = false;
			animator.SetTrigger("dead");
		}
	}

	void TakeDamage(int damage){
		this.currentHealth -= damage;
	}
}
