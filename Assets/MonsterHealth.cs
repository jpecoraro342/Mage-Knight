using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
	public int maxHealth = 50;
	int currentHealth;
	GameObject player;
	Animator animator;
	NavMeshAgent agent;
	bool isDead;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			agent.enabled = false;
			if (!isDead){
				animator.SetTrigger("dead");
				isDead = true;
			}
		}
	}

	public void TakeDamage(int damage){
		this.currentHealth -= damage;
	}
}
