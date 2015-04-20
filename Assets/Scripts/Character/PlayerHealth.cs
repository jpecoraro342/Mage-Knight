using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
	public Text healthText;
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public AudioClip painClip;
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	
	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
//	PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	
	
	void Awake ()
	{
		isDead = false;
		// Setting up the references.
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
//		playerShooting = GetComponentInChildren <PlayerShooting> ();
		
		// Set the initial health of the player.
		currentHealth = startingHealth;
	}
	
	
	void Update ()
	{

		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		// Reset the damaged flag.
		damaged = false;
		healthText.text = currentHealth + "/" + startingHealth;
		healthSlider.value = currentHealth;
	}


	
	public void TakeDamage (int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;
		
		// Reduce the current health by the damage amount.
		if (currentHealth > 0) {
			if (currentHealth - amount < 0) {
				currentHealth = 0;
			} else {
				currentHealth -= amount;
			}
		}
		// Set the health bar's value to the current health.
		//healthSlider.value = currentHealth;
		
		// Play the hurt sound effect.
		playerAudio.clip = painClip;
		playerAudio.Play ();
		
		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}

	public void gainHealth(int health){
		if (currentHealth < startingHealth) {
			if (currentHealth + health >= startingHealth) {
				currentHealth = startingHealth;
				healthSlider.value = currentHealth;
			} else {
				currentHealth += health;
				healthSlider.value = currentHealth;
			}
		}
	}

	public bool getIsDead() {
		return isDead;
	}
	
	
	void Death()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;
		Debug.Log ("DEATH");
		// Turn off any remaining shooting effects.
		//	playerShooting.DisableEffects ();
		
		// Tell the animator that the player is dead.
		anim.SetTrigger ("Die");
		
		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
<<<<<<< HEAD
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
=======
		playerAudio.clip = deathClip;
		playerAudio.Play ();
>>>>>>> d1ae3ec5e027621e5f89dd5560c7fa81b5823878
		
		// Turn off the movement and shooting scripts.
		playerMovement.enabled = false;
	//	playerShooting.enabled = false;

		StartCoroutine(DelayReloadLevel(3f));
	} 

	IEnumerator DelayReloadLevel(float time) {
		yield return null;
		yield return new WaitForSeconds(time);
		Application.LoadLevel(Application.loadedLevel);
	}
}