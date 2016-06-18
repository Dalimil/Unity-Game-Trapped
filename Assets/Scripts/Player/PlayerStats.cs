﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

	public bool isPlayerOne;
	public bool hasBubble;
	public bool hasGun;
	public int startingShield = 600;
	public int currentShield;
	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth = 100;                                   // The current health the player has.
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	GameObject bubble;

	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren <PlayerShooting> ();

		// Set the initial health of the player.
		currentHealth = startingHealth;
		if (hasBubble) {
			currentShield = startingShield;
			if (isPlayerOne) {
				bubble = GameObject.FindGameObjectWithTag ("Shield_1");
			} else {
				bubble = GameObject.FindGameObjectWithTag ("Shield_2");
			}
		} else {
			currentShield = 0;
		}
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
	}


	public void TakeDamage (int amount)
	{
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.

		if (hasBubble && currentShield > 0) {
			currentShield -= amount;

			// Set the health bar's value to the current health.
			healthSlider.value = currentShield * 100.0f / startingShield;

			if(currentShield <= 0) {
				// Remove bubble object
				bubble.SetActive(false);
				hasBubble = false;
			}
		} else {
			currentHealth -= amount;
		}

		// Play the hurt sound effect.
		playerAudio.Play ();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !isDead)
		{
			// ... it should die.
			Death ();
		}
	}


	void Death ()
	{
		// Set the death flag so this function won't be called again.
		isDead = true;

		// Turn off any remaining shooting effects.
		if(hasGun) playerShooting.DisableEffects ();

		// Tell the animator that the player is dead.
		anim.SetTrigger ("Die");

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		playerAudio.clip = deathClip;
		playerAudio.Play ();

		// Turn off the movement and shooting scripts.
		playerMovement.enabled = false;
		if(hasGun) playerShooting.enabled = false;
	}


	//public void RestartLevel ()
	//{
		// Reload the level that is currently loaded.
		// SceneManager.LoadScene (0);
	//}
}

