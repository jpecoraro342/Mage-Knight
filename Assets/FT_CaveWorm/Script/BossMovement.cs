using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
	Transform player;               // Reference to the player's position.
	//Transform boss;
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	Animator anim;					// Reference to animator.
	SphereCollider sphere;
	
	
	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//boss = GameObject.FindGameObjectWithTag ("CaveWorm").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		sphere = GetComponent <SphereCollider> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	
	void Update ()
	{
		nav.SetDestination (player.position);
		//if the player is close enough to the enemy
		//float distance = Vector3.Distance (player.position, boss.position);
		//if (distance < float.MaxValue)

		//anim.SetTrigger ("a");
		//else
		//	anim.SetBool ("PlayerInRange", false);


		//	anim.SetTrigger ("PlayerInRange");
		//else if (distance < 30) {
		//	anim.SetTrigger ("PlayerDetected");
		//	nav.SetDestination (player.position);
		//}
		// If the enemy and the player have health left...
		//if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		//{
			// ... set the destination of the nav mesh agent to the player.
		//	nav.SetDestination (player.position);
		//}
		// Otherwise...
		//else
		//{
			// ... disable the nav mesh agent.
		//	nav.enabled = false;
		//}
		System.Console.WriteLine (anim.GetCurrentAnimatorStateInfo(0));
	} 

	void OnTriggerEnter( Collider col )
	{
		if(col.tag == "Player")
			anim.SetBool ("PlayerInRange", true);
	}

	void OnTriggerExit( Collider col )
	{
		if (col.tag == "Player")
			anim.SetBool ("PlayerInRange", false);
	}

	void OnTriggerEnter( Collider col )
	{

}