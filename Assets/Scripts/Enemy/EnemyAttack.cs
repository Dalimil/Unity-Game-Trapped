using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
    public int attackDamage = 100;               // The amount of health taken away per attack.


    Animator anim;                              // Reference to the animator component.
    GameObject player1;                          // Reference to the player GameObject.
	PlayerStats player1Health;                  // Reference to the player's health.
	GameObject player2;                          // Reference to the player GameObject.
	PlayerStats player2Health;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool player1InRange;                         // Whether player is within the trigger collider and can be attacked.
	bool player2InRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.


    void Awake ()
    {
        // Setting up the references.
        player1 = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player_2");
		player1Health = player1.GetComponent <PlayerStats> ();
		player2Health = player2.GetComponent <PlayerStats> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        // If the entering collider is the player...
		if (other.gameObject == player1) {
			// ... the player is in range.
			player1InRange = true;
		} else if (other.gameObject == player2) {
			player2InRange = true;
		}
    }


    void OnTriggerExit (Collider other)
    {
        // If the exiting collider is the player...
		if (other.gameObject == player1) {
			// ... the player is no longer in range.
			player1InRange = false;
		} else if (other.gameObject == player2) {
			player2InRange = false;
		}
    }


    void Update ()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if(timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
        {
			if (player1InRange) {
				
				// ... attack.
				Attack (player1Health);
			} else if (player2InRange) {
				Attack (player2Health);
			}
		}

        // If the player has zero or less health...
		if(player1Health.currentHealth <= 0 || player2Health.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack (PlayerStats playerHealth)
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if(playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
