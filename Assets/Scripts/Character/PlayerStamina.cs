using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStamina : MonoBehaviour
{
	public int startingStamina = 100;                            // The amount of health the player starts the game with.
	public int currentStamina;                                   // The current health the player has.
	public Text staminaText;
	public Slider staminaSlider;                                 // Reference to the UI's health bar.
	//public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	//public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	//public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	//public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	
	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	//	PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isTired;                                                // Whether the player is dead.
	bool powerAttacked;                                               // True when the player gets damaged.
	
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		//		playerShooting = GetComponentInChildren <PlayerShooting> ();
		
		// Set the initial health of the player.
		currentStamina = startingStamina;
	}
	
	
	void Update ()
	{
		
		if (Input.GetKeyDown ("f")) {
			
			TakeStamina (10);
			
		}
		// If the player has just been damaged...
		if(powerAttacked)
		{
			// ... set the colour of the damageImage to the flash colour.
			//damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			//damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		// Reset the damaged flag.
		powerAttacked = false;
		staminaText.text = currentStamina + "/" + startingStamina;
	}
	
	
	
	public void TakeStamina (int amount)
	{
		// Set the damaged flag so the screen will flash.
		powerAttacked = true;
		
		// Reduce the current health by the damage amount.
		if (currentStamina >= amount) {
			currentStamina -= amount;
		} else {
			print("You are out of stamina!");
		}
		
		// Set the health bar's value to the current health.
		staminaSlider.value = currentStamina;
		
		// Play the hurt sound effect.
		playerAudio.Play ();
		
		// If the player has lost all it's health and the death flag hasn't been set yet...
		
	}
	
	
	
}