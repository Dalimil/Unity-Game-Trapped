using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player1;               // Reference to the player's position.
	Transform player2;
    PlayerStats playerStats1;      // Reference to the player's health.
	PlayerStats playerStats2;      // Reference to the player's health.
    EnemyHealth enemyHealth;        // Reference to this enemy's health.
    NavMeshAgent nav;               // Reference to the nav mesh agent.


    void Awake ()
    {
        // Set up the references.
        player1 = GameObject.FindGameObjectWithTag ("Player").transform;
		player2 = GameObject.FindGameObjectWithTag ("Player_2").transform;

        playerStats1 = player1.GetComponent <PlayerStats> ();
		playerStats2 = player2.GetComponent <PlayerStats> ();

        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
        // If the enemy and the player have health left...
		if(enemyHealth.currentHealth > 0){
			if (playerStats1.currentHealth <= 0 && playerStats2.currentHealth <= 0) { // both dead
				// ... disable the nav mesh agent.
				nav.enabled = false;
			} else if (playerStats1.currentHealth <= 0) { // one dead
				nav.SetDestination (player2.position);
			} else if (playerStats2.currentHealth <= 0) { // one dead
				nav.SetDestination (player1.position);
			} else if (!playerStats1.hasGun && playerStats1.currentShield <= 0) { // shield down, no gun
				nav.SetDestination (player1.position); // set the destination of the nav mesh agent to the player
			} else if (!playerStats2.hasGun && playerStats2.currentShield <= 0) { // shield down, no gun
				nav.SetDestination (player2.position);
			} else if (playerStats1.hasGun) { // someone still has bubble -> chase gun instead
				nav.SetDestination (player1.position);
			} else {
				nav.SetDestination (player2.position);
			}
        // Otherwise...
		} else
        {
            // ... disable the nav mesh agent.
            nav.enabled = false;
        }
    }
}
