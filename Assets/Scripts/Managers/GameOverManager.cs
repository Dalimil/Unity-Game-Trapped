using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerStats player1Stats;       // Reference to the player's health.
	public PlayerStats player2Stats;
	public bool player1Alive;
	public bool player2Alive;
    Animator anim;                          // Reference to the animator component.
	GameObject bubble1;
	GameObject bubble2;

    void Awake ()
    {
        // Set up the reference.
        anim = GetComponent <Animator> ();
		GameObject player1 = GameObject.FindGameObjectWithTag ("Player");
		GameObject player2 = GameObject.FindGameObjectWithTag ("Player_2");
		bubble1 = GameObject.FindGameObjectWithTag ("Shield_1");
		bubble2 = GameObject.FindGameObjectWithTag ("Shield_2");
		player1Stats = player1.GetComponent <PlayerStats> ();
		player2Stats = player2.GetComponent <PlayerStats> ();
    }

	public void removeBubble(bool isPlayerOne)
	{
		if(isPlayerOne){
			bubble1.SetActive(false);
		} else {
			bubble2.SetActive(false);
		}
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
