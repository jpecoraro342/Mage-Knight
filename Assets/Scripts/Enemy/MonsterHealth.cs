using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
	public int maxHealth = 50;
	int currentHealth;
	GameObject player;
	Animator animator;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
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

	public void TakeDamage(int damage){
		this.currentHealth -= damage;
	}
}
