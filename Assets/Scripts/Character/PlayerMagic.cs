using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMagic : MonoBehaviour
{
	public int startingMagic = 100;                            // The amount of health the player starts the game with.
	public int currentMagic;                                   // The current health the player has.
	public Text magicText;
	public Slider magicSlider;                                 // Reference to the UI's health bar.
	//public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	//public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	//public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	//public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	private float timestamp = 0f;
	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	//	PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isExpended;                                                // Whether the player is dead.
	bool spellCasted;                                               // True when the player gets damaged.

	public AudioClip magicCasted;
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		//		playerShooting = GetComponentInChildren <PlayerShooting> ();
		
		// Set the initial health of the player.
		currentMagic = startingMagic;
	}
	
	
	void Update ()
	{
		
		if (Input.GetKeyDown ("e")) {

			TakeMagic (10);

		}
		magicSlider.value = currentMagic;
		if (currentMagic < 100) {

			timestamp += Time.deltaTime;
			if(timestamp > .5f){
				timestamp = 0;
				currentMagic++;
			}
		}
		// If the player has just been damaged...
		if(spellCasted)
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
		spellCasted = false;
		magicText.text = currentMagic + "/" + startingMagic;
	}
	
	
	
	public void TakeMagic (int amount)
	{
		// Set the damaged flag so the screen will flash.
		spellCasted = true;
		
		// Reduce the current health by the damage amount.
		if (currentMagic >= amount) {
			currentMagic -= amount;
		} else {
			print("You are out of magic!");
		}

		// Set the health bar's value to the current health.
		magicSlider.value = currentMagic;
		
		// Play the hurt sound effect.
		playerAudio.clip = magicCasted;
		playerAudio.Play ();
		
		// If the player has lost all it's health and the death flag hasn't been set yet...

	}
	
	
	    
}