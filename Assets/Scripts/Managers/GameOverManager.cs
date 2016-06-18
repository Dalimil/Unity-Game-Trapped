using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    PlayerStats player1Stats;       // Reference to the player's health.
	PlayerStats player2Stats;
	public static bool player1Alive;
	public static bool player2Alive;
    Animator anim;                          // Reference to the animator component.

	public float restartDelay = 5f;         // Time to wait before restarting the level
	float restartTimer = 0f;                     // Timer to count up to restarting the level

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
        if(player1Stats.currentHealth <= 0) {
			player1Alive = false;
        }
		if (player2Stats.currentHealth <= 0) {
			player2Alive = false;
		}
		// print (player1Alive + " " + player2Alive);

		if (!player1Alive && !player2Alive) {
			print ("GameOver");
			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");

			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the currently loaded level.
				Application.LoadLevel(Application.loadedLevel);
			}
		}
    }
}
