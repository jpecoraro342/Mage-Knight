using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	GameObject player;
	Animator anim;
	Rigidbody rigid;
	NavMeshAgent nav;
	MonsterStats stats;

	float atkTime;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		rigid = GetComponent<Rigidbody> ();
		nav = GetComponent<NavMeshAgent> ();
		stats = GetComponent<MonsterStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		checkAttack ();
	}

	void checkAttack(){
		//animator.SetBool ("grabweapon", true);
		if ((player.transform.position - this.transform.position).magnitude < 3 && !stats.isDead) {
			if ((Time.time - atkTime) > 1){
				anim.SetFloat ("random", Random.Range(0, 4));
				atkTime = Time.time;
				anim.SetTrigger ("attacking");
				//if weapon hits player: player.takedamage
			}
			//http://answers.unity3d.com/questions/750785/mecanim-trigger-event-at-end-of-animation-state.html
		}
	}
}
