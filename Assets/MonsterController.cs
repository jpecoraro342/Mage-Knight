using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {
	public float speed = 10f;
	public float maxHP = 50f;
	public float currentHP;

	bool grounded = true;

	Animator animator;
	Rigidbody rigidbody;


	void Awake(){
		animator = GetComponent<Animator> ();
		animator.SetBool ("grounded", true);
		animator.SetFloat ("speed", 0f);
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
