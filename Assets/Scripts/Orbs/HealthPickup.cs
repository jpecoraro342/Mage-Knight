using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	GameObject player;
	public float pickupDistance;
	private int healthAmount;
	private int minHealth = 5;
	private int smallHealth = 10;
	private int medHealth = 20;
	private int bigHealth = 40;
	private int maxHealth = 80;
	private float randomF;
	private bool pickupable;
	private float spawnTime;

	PlayerHealth playerHealth;

	public int pickupDelayTime;
	void Start () {
		playerHealth = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerHealth> ();
		pickupable = false;
		spawnTime = Time.time;
		randomF = Random.value;
		if (randomF > 0.95)
						healthAmount = maxHealth;
				else if (randomF > 0.9)
						healthAmount = bigHealth;
				else if (randomF > 0.8)
						healthAmount = medHealth;
				else if (randomF > 0.6)
						healthAmount = smallHealth;
				else
						healthAmount = minHealth;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	bool playerAtMaxHealth(){
		if (playerHealth.currentHealth == playerHealth.startingHealth)
			return true;
		else
			return false;
	}

	void Update () {

		if (Time.time - spawnTime > pickupDelayTime && !pickupable)
						pickupable = true;
		if ((player.transform.position - this.transform.position).magnitude < this.pickupDistance && pickupable && !playerAtMaxHealth ()) {

			//player.gainHealth(healthAmount);
			DestroyObject (this.gameObject);
		}
	}
}
