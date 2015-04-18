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
	void Start () {
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
	

	void Update () {

		if (Time.time - spawnTime > 1 && !pickupable)
						pickupable = true;
		if ((player.transform.position - this.transform.position).magnitude < this.pickupDistance && pickupable) {

			//player.gainHealth(healthAmount);
			DestroyObject (this.gameObject);
				}
	}
}
