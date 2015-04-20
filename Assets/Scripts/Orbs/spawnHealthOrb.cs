using UnityEngine;
using System.Collections;

public class spawnHealthOrb : MonoBehaviour {
	private float timeSinceSpawn;
	private bool spawned;
	public Transform healthOrb;


	void Start () {
		timeSinceSpawn = Time.time;
		bool spawned = false;
	}
	
	void Update () {
		if (Time.time - timeSinceSpawn > 2 && !spawned) {
			Instantiate (healthOrb, this.transform.position, Quaternion.identity);
			spawned = true;
				}
	}
}
