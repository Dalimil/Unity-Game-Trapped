using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    PlayerStats player1Stats;       // Reference to the player's health.
	PlayerStats player2Stats;
	public static bool player1Alive;
	public static bool player2Alive;
    Animator anim;                          // Reference to the animator component.

    void Awake ()
    {
		player1Alive = player2Alive = true;
        // Set up the reference.
        anim = GetComponent <Animator> ();
		GameObject player1 = GameObject.FindGameObjectWithTag ("Player");
		GameObject player2 = GameObject.FindGameObjectWithTag ("Player_2");
		player1Stats = player1.GetComponent <PlayerStats> ();
		player2Stats = player2.GetComponent <PlayerStats> ();
    }

    void Update ()
    {
        // If the player has run out of health...
        if(player1Stats.currentHealth <= 0)
        {
			player1Alive = false;
            
        }
		if (player2Stats.currentHealth <= 0) {
			player2Alive = false;
		}

		if (!player1Alive && !player2Alive) {
			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");
		}
    }
}
