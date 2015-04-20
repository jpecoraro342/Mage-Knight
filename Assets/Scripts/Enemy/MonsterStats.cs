using UnityEngine;
using System.Collections;

public class MonsterStats : MonoBehaviour {
	public float maxHealth;
	public float currentHealth;
	public int monsterDamage;
	public bool isDead;
	public float attackSpeed;
	public float speed;
	public float visionCone;
	public float visionRadius;
	public bool seenPlayer;
	public float timeSinceSeenPlayer;

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		monsterDamage = 10;
		isDead = false;
		attackSpeed = 1f;
		speed = 10f;
		visionCone = 120f;
		visionRadius = 20f;
		seenPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
