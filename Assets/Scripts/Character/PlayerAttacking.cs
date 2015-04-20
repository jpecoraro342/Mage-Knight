using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
 * This class will handle all of the attacking for the player
 * The players chosen attacks will be saved here, and are mapped to the different buttons. Each weapon has attacks 1-4
 * Appropriate animations will be available for each of the attacks
 * 
 * Once an attack button is pressed, This script will check the AltAttack button, and then perform the attack with the appropriate weapon
 * 
 * If an enemy is struck (some form of trigger or something will be used) the player will take away health from that enemy
 * 
 */
public class PlayerAttacking : MonoBehaviour {
	Animator animator;

	public Text stats;
	public RawImage StaffEnabled;
	public RawImage SwordEnabled;

	static string EnemyTag = "Enemy";
	static string BossTag = "Boss";

	LinkedList<GameObject> enemyTargetList;

	//Button Mappings
	static string Attack1 = "Attack1";
	static string Attack2 = "Attack2";
	static string Attack3 = "Attack3";
	static string Attack4 = "Attack4";

	string[] attackButtons = new string[4] { "Attack1", "Attack2", "Attack3", "Attack4" }; 
	bool[] meleeAttacksEnabled = new bool[4] { true, true, true, false }; 
	bool[] mageAttacksEnabled = new bool[4] { true, true, true, false };

	float[] meleeAttackTime = new float[4] { .8f, .633f, .633f, 0f };
	float[] mageAttackTime = new float[4] { .8f, .8f, .8f, 0f };

	float[] meleeAnimationTime = new float[4] { 1.167f, .633f, .633f, 0f };
	float[] mageAnimationTime = new float[4] { 1f, 1f, 1f, 0f };

	float[] mageAttackDistance = new float[4] { 20f, 20f, 20f, 0f };
	public GameObject[] mageAttackParticle = new GameObject[4];
	public GameObject[] swordAttackWeapons = new GameObject[4];

	static string alternateAttack = "AltAttack";

	//Attack Bools
	bool isAttacking = false;
	bool canAttack = true;

	bool altAttackPressed = false;

	bool meleeAttacksActive = true;
	bool mageAttacksActive = false;

	int BaseLayerIndex = 0;
	int MeleeAttackLayerIndex = 1;
	int MageAttackLayerIndex = 2;
	
	void Awake () {
		animator = GetComponent<Animator>();
		enemyTargetList = new LinkedList<GameObject>();
	}

	void Update () {
		if (Input.GetButton(alternateAttack) && !altAttackPressed) {
			altAttackPressed = true;
			meleeAttacksActive = !meleeAttacksActive;
			mageAttacksActive = !mageAttacksActive;

			if (SwordEnabled != null) {
				SwordEnabled.enabled = meleeAttacksActive;
			}
			if (StaffEnabled != null) {
				StaffEnabled.enabled = mageAttacksActive;
			}
		}
		else if (!Input.GetButton(alternateAttack)) {
			altAttackPressed = false;
		}

		for (int i = 0; i < 3; i++) {
			if (Input.GetButton(attackButtons[i]) && !isAttacking && canAttack) {
				isAttacking = true;
				animator.SetTrigger(attackButtons[i]);
				
				if (meleeAttacksActive && meleeAttacksEnabled[i]) {
					StartCoroutine(StartMeleeAttack(i));
				}
				else if (mageAttacksActive && mageAttacksEnabled[i]) {
					StartCoroutine(StartMageAttack(i));
				}

				return;
			}
			else if (!Input.GetButton(attackButtons[i])) {
				canAttack = true;
			}
		}
	}

	//These are for the auto targetting system
	public void OnTriggerEnter(Collider other) {
		GameObject triggerObject = other.gameObject;
		if (triggerObject.tag == EnemyTag || triggerObject.tag == BossTag) {
			if (!triggerObject.GetComponent<MonsterHealth>().isDead()) {
				enemyTargetList.AddLast(other.gameObject);
			}
		}
	}

	public void OnTriggerExit(Collider other) {
		GameObject triggerObject = other.gameObject;
		if (triggerObject.tag == EnemyTag || triggerObject.tag == BossTag) {
			enemyTargetList.Remove(other.gameObject);
		}
	}

	public void enemyHasDied(GameObject enemy) {
		enemyTargetList.Remove(enemy);
	}

	GameObject getClosestEnemy() {
		GameObject closest = null;
		float closestDistance = float.MaxValue;
		
		foreach (GameObject enemy in enemyTargetList) {
			Vector3 enemyPosition = enemy.transform.position;
			float distance = Vector3.Distance(transform.position, enemyPosition);
			if (distance < closestDistance) {
				closest = enemy;
				closestDistance = distance;
			}
		}

		return closest;
	}

	Vector3 getTargetTransformDirection() {
		Vector3 closestEnemyDirection = transform.forward;

		GameObject closestEnemy = getClosestEnemy();

		if (closestEnemy != null) {
			closestEnemyDirection = closestEnemy.transform.position;
			float oldY = closestEnemyDirection.y;
			closestEnemyDirection = closestEnemyDirection - transform.position;
			closestEnemyDirection.y = 0f;//oldY + .5f;
		}

		return closestEnemyDirection;
	}

	public void setMageAttack(int index, float animationTime, float attackTime, float attackDistance, GameObject particleObject) {
		mageAttacksEnabled[index] = true;
		mageAttackTime[index] = attackTime;
		mageAnimationTime[index] = animationTime;
		mageAttackDistance[index] = attackDistance;
		mageAttackParticle[index] = particleObject;
	}

	//Attack Enumerators

	IEnumerator StartMeleeAttack(int index) {
		yield return null;
		float attackTime = meleeAttackTime[index];
		float animationTime = meleeAnimationTime[index];

		animator.SetLayerWeight(MeleeAttackLayerIndex, 1);
		swordAttackWeapons[index].SetActive(true);

		yield return new WaitForSeconds(attackTime);
		swordAttackWeapons[index].SetActive(false);

		yield return new WaitForSeconds(animationTime - attackTime);
		animator.SetLayerWeight(MeleeAttackLayerIndex, 0);

		isAttacking = false;
	}

	IEnumerator StartMageAttack(int index) {
		yield return null;

		animator.SetLayerWeight(MageAttackLayerIndex, 1);

		RaycastHit hit;
		Vector3 attackTransform = transform.position;
		attackTransform.y = transform.position.y + .5f;

		if (index > 1) {
			GameObject closestEnemy = getClosestEnemy();
			if (closestEnemy != null) {
				attackTransform = closestEnemy.transform.position;
			}
		}

		GameObject attack = (GameObject)Instantiate (mageAttackParticle[index], attackTransform, Quaternion.identity);
		attack.transform.parent = gameObject.transform.parent;


		Vector3 Direction = getTargetTransformDirection();
		attack.transform.forward = Direction;

		if (stats != null) {
			stats.text = "My Position: " + transform.position + "\nEnemy Position: " + attackTransform + "\nMy Direction: " + transform.forward + "\nDirection To Enemy: " + Direction;
		}

		yield return new WaitForSeconds(mageAttackTime[index]);

		animator.SetLayerWeight(MageAttackLayerIndex, 0);

		isAttacking = false;
	}
}
