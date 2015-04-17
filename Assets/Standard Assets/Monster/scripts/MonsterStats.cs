using UnityEngine;
using System.Collections;

public class MonsterStats : MonoBehaviour {
	public int maxHealth;
	public int currentHealth;
	public int monsterDamage;
	public bool isDead;
	public float attackSpeed;
	public float speed;

	// Use this for initialization
	void Start () {
		maxHealth = 50;
		currentHealth = maxHealth;
		monsterDamage = 10;
		isDead = false;
		attackSpeed = 1f;
		speed = 10f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
