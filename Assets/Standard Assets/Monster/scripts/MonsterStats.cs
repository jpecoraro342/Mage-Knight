using UnityEngine;
using System.Collections;

public class MonsterStats : MonoBehaviour {
	public int maxHealth;
	public int currentHealth;
	public int monsterDamage;
	public bool isDead;
	public float attackSpeed;
	public float speed;
	public float visionCone;
	public float visionRadius;
	public bool seenPlayer;

	// Use this for initialization
	void Start () {
		maxHealth = 50;
		currentHealth = maxHealth;
		monsterDamage = 10;
		isDead = false;
		attackSpeed = 2f;
		speed = 10f;
		visionCone = 90f;
		visionRadius = 10f;
		seenPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
