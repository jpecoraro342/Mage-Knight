using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossMovement : MonoBehaviour
{
	public Text BossStats; 
	GameObject player;               // Reference to the player's position.
	GameObject boss;
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	Animator anim;					// Reference to animator.
	//SphereCollider sphere;
	bool detected;
	bool inRange;
	static float detection = 25;
	static float attack = 5;
	
	
	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		boss = GameObject.FindGameObjectWithTag ("Boss");
		//playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		//sphere = GetComponent <SphereCollider> ();
		nav = GetComponent <NavMeshAgent> ();
		nav.SetDestination (player.transform.position);
		nav.updatePosition = false;
		nav.updateRotation = false;
		anim = GetComponent <Animator> ();
		detected = anim.GetBool ("PlayerDetected");
		inRange = anim.GetBool ("PlayerInRange");
	}
	
	
	void Update ()
	{
		//nav.SetDestination (player.position);
		//nav.enabled = false;
		//if the player is close enough to the enemy
		//detected = anim.GetBool ("PlayerDetected");
		//inRange = anim.GetBool ("PlayerInRange");
		float distance = Vector3.Distance (player.transform.position, boss.transform.position);
		nav.SetDestination (player.transform.position);
		if (distance < detection) {
			detected = true;

			if (distance < attack)
			{
				nav.updatePosition = false;
				nav.updateRotation = true;
				inRange = true;
			}
			else {
				nav.updatePosition = true;
				nav.updateRotation = true;
				//nav.Resume ();
				inRange = false;
			}
			/*if (!detected)
			{
				detected = true;
				nav.Resume();
			}
			nav.SetDestination (player.transform.position);
			if (distance < attack && !inRange)
			{
				inRange = true;
				nav.Stop();
			}
			else if (distance > attack && inRange)
			{
				inRange = false;
				nav.Resume();
			}*/
		}
		else {
			detected = false;
			inRange = false;
			nav.updatePosition = false;
			nav.updateRotation = false;
		}
	

		anim.SetBool ("PlayerDetected", detected);
		anim.SetBool ("PlayerInRange", inRange);
		//BossStats.text = "Detected: " + detected + "\n In Range : " + inRange + "\n Distance : " + distance  + "\n Player Position : " + player.transform.position + "\n Boss Position : " + boss.transform.position;

		/*if (anim.GetBool ("PlayerDetected")) {
			nav.SetDestination (player.position);
			if (distance > 40.0f) {
				nav.enabled = false;
				anim.SetBool ("PlayerDetected", false);
			} 
			if (distance < 7)
				anim.SetTrigger ("AttackPlayer");
			//else if (distance < 7.0f)
			//	anim.SetBool ("PlayerInRange", true);
			//else if (distance > 7.0f)
			//	anim.SetBool ("PlayerInRange", false);
		} else if (distance < 40.0f) {
			nav.enabled = true;
			nav.SetDestination (player.position);
			anim.SetBool ("PlayerDetected", true);
		}*/
		/*if (!(anim.GetBool("PlayerDetected")) && distance < 20.0f) {
			nav.enabled = true;
			nav.SetDestination (player.position);
			anim.SetBool ("PlayerDetected", true);
		} 
		else if (anim.GetBool("PlayerDetected") && distance > 20.0f){
			nav.enabled = false;
			anim.SetBool ("PlayerDetected", false);
		}
		if (!anim.GetBool("PlayerInRange") && distance < 5.0f)
			anim.SetBool ("PlayerInRange", true);
		else if (anim.GetBool("PlayerInRange") && distance > 5.0f)
			anim.SetBool ("PlayerInRange", false);*/
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
		//BossStats.text += "\n" + anim.GetCurrentAnimatorStateInfo(0).IsName ("Attack");
	} 

	void OnTriggerEnter( Collider col )
	{
		if (col.tag == "Player" && anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			BossStats.text = "entered";
		}
	}

	void OnTriggerExit( Collider col )
	{
		if (col.tag == "Player")
			BossStats.text = "exited";
	}
}