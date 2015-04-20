﻿using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {
	GameObject player;
	Animator anim;
	Rigidbody rigid;
	NavMeshAgent nav;
	MonsterStats stats;
	public bool isAttacking;
	public AudioClip monsterClaw;
	AudioSource monsterAudio;

	float atkTime = 0f;
	// Use this for initialization
	void Start () {
		isAttacking = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		monsterAudio = GetComponent<AudioSource> ();
		rigid = GetComponent<Rigidbody> ();
		nav = GetComponent<NavMeshAgent> ();
		stats = GetComponent<MonsterStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkAttack ();
	}

	public void tryDealingDamage(){
		if (isAttacking) {
			player.GetComponent<PlayerHealth>().TakeDamage(stats.monsterDamage);
		}
	}


	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "PlayerHitBox") {
			player.GetComponent<PlayerHealth>().TakeDamage(stats.monsterDamage);
		}
	}

	void checkAttack(){
		//animator.SetBool ("grabweapon", true);
		if ((player.transform.position - this.transform.position).magnitude < 3 && !stats.isDead ) {
			if ((Time.time - atkTime) > 1/stats.attackSpeed){
				anim.SetFloat ("random", Random.Range(0, 4));
				atkTime = Time.time;
				anim.speed = stats.attackSpeed;
				anim.SetTrigger ("attacking");
				isAttacking = true;
				monsterAudio.clip = monsterClaw;
				monsterAudio.Play();
				//if weapon hits player: player.takedamage
			}
			else isAttacking = false;

			//http://answers.unity3d.com/questions/750785/mecanim-trigger-event-at-end-of-animation-state.html
		}
		else{
			//if((player.transform.position - this.transform.position).magnitude > 3)
				anim.speed = 1;
		}
	}
}
