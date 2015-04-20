using UnityEngine;
using System.Collections;

public class SummonerStats : MonoBehaviour {
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
		monsterDamage = 40;
		isDead = false;
		attackSpeed = 1f;
		speed = 15f;
		visionCone = 360f;
		visionRadius = 100f;
		seenPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}