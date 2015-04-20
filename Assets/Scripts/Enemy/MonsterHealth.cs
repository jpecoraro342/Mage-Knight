﻿using UnityEngine;
using System.Collections;

public class MonsterHealth : MonoBehaviour {
	GameObject player;
	Animator animator;
	NavMeshAgent agent;
	MonsterStats stats;
	AudioSource monsterAudio;
	public AudioClip monsterDeath;
	public AudioClip monsterPain;
	public Transform healthOrb;
	private float deathTime;

	private Rigidbody rigid;

	bool hasNotifiedPlayerOfDeath;

	// Use this for initialization
	void Start () {
		hasNotifiedPlayerOfDeath = false;

		stats = GetComponent<MonsterStats> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		animator = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
		monsterAudio = GetComponent <AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (stats.currentHealth <= 0) {
			agent.enabled = false;
			monsterAudio.clip = monsterDeath;
			monsterAudio.Play();
			if (!stats.isDead){
				animator.SetTrigger("dead");
				stats.isDead = true;
				deathTime = Time.time;
				//if (Random.value < 0.75){
				Instantiate(healthOrb, this.gameObject.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
				//}
			}

			if (stats.isDead && !hasNotifiedPlayerOfDeath) {
				hasNotifiedPlayerOfDeath = true;
				player.GetComponent<PlayerAttacking>().enemyHasDied(this.gameObject);
			}

			if (stats.isDead && Time.time - deathTime > 5) {
				this.transform.root.Translate (new Vector3(0,-0.1f,0) * Time.deltaTime);
			}
			
			if (stats.isDead && Time.time - deathTime > 13) {
				DestroyObject (this.gameObject);
			}
		}
	}

	public bool isDead() {
		return stats.isDead;
	}

	public void TakeDamage(float damage) {
		stats.currentHealth -= damage;
		monsterAudio.clip = monsterPain;
		monsterAudio.Play ();
	}
}
