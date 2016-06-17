using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	
	Animator anim;                              // Reference to the animator component.
	GameObject player;                          // Reference to the player GameObject.
	PlayerStats playerHealth;                  // Reference to the player's health.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.


	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerStats> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponent <Animator> ();
	}


	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}


	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}


	void Update ()
	{

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(playerInRange && enemyHealth.currentHealth > 0)
		{
			// ... attack.
			Attack ();
		}

		// If the player has zero or less health...
		if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			anim.SetTrigger ("PlayerDead");
		}
	}


	void Attack ()
	{
		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
