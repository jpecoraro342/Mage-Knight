using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossMovement : MonoBehaviour
{
	public Text BossStats; 
	GameObject player;               // Reference to the player's position.
	PlayerHealth playerHealth;
	GameObject boss;
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	Animator anim;					// Reference to animator.
	ParticleSystem flames;
	bool detected;
	bool inRange;
	//bool attacking;
	bool flaming;
	bool bossDead;
	static float detection = 25f;
	static float attack = 5f;
	static float flameDuration = 2f;
	static float flameCooldown = 10f;
	float lastFlame;
	bool flameOnCooldown;
	int bossHealth = 200;
	
	
	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		boss = GameObject.FindGameObjectWithTag ("Boss");
		playerHealth = player.GetComponent <PlayerHealth> ();
		//bossHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		flames = GameObject.FindGameObjectWithTag ("BossFlame").GetComponent <ParticleSystem>();
		flames.Stop();
		nav.SetDestination (player.transform.position);
		//nav.Stop ();
		//nav.updatePosition = false;
		//nav.updateRotation = false;
		nav.enabled = true;
		anim = GetComponent <Animator> ();
		detected = anim.GetBool ("PlayerDetected");
		inRange = anim.GetBool ("PlayerInRange");
		bossDead = anim.GetBool ("BossDead");
		flaming = anim.GetBool ("Flame");
		flameOnCooldown = false;
	}
	
	
	void Update ()
	{
		if (bossDead)
			return;

		//detected = anim.GetBool ("PlayerDetected");
		//inRange = anim.GetBool ("PlayerInRange");
		//check to disable flaming
		if (flaming) {
			if (Time.time > flameDuration + lastFlame)
			{
				flaming = false;
				flames.Stop ();
				nav.Resume ();
				nav.SetDestination (player.transform.position);

			}
			else
			{
				//nav.updateRotation = true;
				//nav.updatePosition = false;
				//Check to deal damage to player
				Vector3 targetDir = player.transform.position - this.transform.position;
				Vector3 forward = this.transform.forward;
				float angle = Vector3.Angle(targetDir, forward);
				float distance2 = Vector3.Distance (player.transform.position, boss.transform.position);
				if(angle.CompareTo(30) < 0 && distance2 < 5)
				{
					//Damage Player
					//BossStats.text = "FLAME DAMAGE";
					playerHealth.TakeDamage(30 * Time.deltaTime);
				}
				//else BossStats.text = "No Flame Damage";
				return;
			}
		} else if (flameOnCooldown && Time.time > flameCooldown + lastFlame) //Check to put flame off cooldown
			flameOnCooldown = false;

		float distance = Vector3.Distance (player.transform.position, boss.transform.position);

		if (distance < detection) {
			detected = true;
			nav.Resume ();
			nav.SetDestination (player.transform.position);

			if (distance < attack)
			{
				//nav.updatePosition = false;
				//nav.updateRotation = true;

				inRange = true;
				if(!flameOnCooldown)
				{
					//start flaming
					lastFlame = Time.time;
					flaming = true;
					flameOnCooldown = true;
					//Activate flame particles
					flames.Play ();
					nav.Stop ();
				}

			}
			else {
				//nav.updatePosition = true;
				//nav.updateRotation = true;
				//nav.Resume ();
				inRange = false;
			}
		}
		else {
			nav.Stop ();
			detected = false;
			inRange = false;
			//nav.updatePosition = false;
			//nav.updateRotation = false;
		}
	
		anim.SetBool ("PlayerDetected", detected);
		anim.SetBool ("PlayerInRange", inRange);
		anim.SetBool ("Flame", flaming);
	} 

	void OnTriggerEnter( Collider col )
	{
		if (col.tag == "Player" && anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			//This is where you would tell the player to take damage
			playerHealth.TakeDamage(10);
		}
	}

	public void takeDamage(int damageTaken)
	{
		bossHealth -= damageTaken;
		if (bossHealth <= 0) {
			bossDead = true;
			anim.SetBool ("BossDead", true);
		}
	}
}