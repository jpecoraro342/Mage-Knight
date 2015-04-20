using UnityEngine;
using System.Collections;

public class SummonerAttack : MonoBehaviour {
	GameObject player;
	Animator anim;
	Rigidbody rigid;
	NavMeshAgent nav;
	MonsterStats stats;
	SummonerController controller;

	public int summonDelay;
	float summonTime;

	public bool isAttacking;
	public Transform Groll;

	int escortMaxCount;
	ArrayList escort;
	
	float atkTime = 0f;
	// Use this for initialization
	void Start () {
		summonTime = 0f;
		controller = GetComponent<SummonerController> ();
		isAttacking = false;
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
		if ((player.transform.position - this.transform.position).magnitude < 3 && !stats.isDead ) {
			if ((Time.time - atkTime) > 1/stats.attackSpeed){

				atkTime = Time.time;
				anim.speed = stats.attackSpeed;
				anim.SetTrigger ("attack");
				isAttacking = true;
				
				//if weapon hits player: player.takedamage
			}
			else isAttacking = false;			
		}
		else{
			if (controller.canSeePlayer() && Time.time - summonTime > summonDelay && !stats.isDead){
				summonTime = Time.time;
				Vector3 midpoint = Vector3.Lerp(this.transform.position, player.transform.position,0.5f);
				Vector3 direction = this.transform.position - player.transform.position;
				direction.Normalize ();
				//Debug.DrawLine (this.transform.position, player.transform.position, Color.white, 1000);
				summonGroll (midpoint, Quaternion.LookRotation (-1 * direction));
			}
			anim.speed = 1;
		}
	}
	void summonGroll(Vector3 position, Quaternion direction){
		Instantiate(Groll, position, direction);
	}
}
