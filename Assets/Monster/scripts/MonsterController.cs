using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
	public float speed = 10f;
	public float maxHP = 50f;
	public float currentHP;

	bool grounded = true;

	Animator animator;
	Rigidbody rigidbody;
	NavMeshAgent navmeshAgent;
	GameObject player;


	void Awake(){
		animator = GetComponent<Animator> ();
		animator.SetBool ("grounded", grounded);
		animator.SetFloat ("speed", 0f);
		navmeshAgent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	void Start () {
		navmeshAgent.enabled = true;
		navmeshAgent.destination = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		navmeshAgent.destination = player.transform.position;
		animator.SetFloat ("speed", this.navmeshAgent.speed);
	}
}
