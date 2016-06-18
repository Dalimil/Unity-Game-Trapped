using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	public static bool player1Alive;
	public static bool player2Alive;

	GameObject player1;
	GameObject player2;
	GameObject gun1, gun2, bubble1, bubble2, gunB1, gunB2;
    PlayerStats player1Stats;       // Reference to the player's health.
	PlayerStats player2Stats;

    Animator anim;                          // Reference to the animator component.

	public float restartDelay = 1f;         // Time to wait before restarting the level
	float restartTimer = 0f;                     // Timer to count up to restarting the level

    void Awake ()
    {
		player1Alive = player2Alive = true;
        // Set up the reference.
        anim = GetComponent <Animator> ();
		player1 = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player_2");
		player1Stats = player1.GetComponent <PlayerStats> ();
		player2Stats = player2.GetComponent <PlayerStats> ();
		gun1 = GameObject.FindGameObjectWithTag ("Gun_1");
		gun2 = GameObject.FindGameObjectWithTag ("Gun_2");
		bubble1 = GameObject.FindGameObjectWithTag ("Shield_1");
		bubble2 = GameObject.FindGameObjectWithTag ("Shield_2");
		gunB1 = GameObject.FindGameObjectWithTag ("GunB_1");
		gunB2 = GameObject.FindGameObjectWithTag ("GunB_2");
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
			// print ("GameOver");
			// ... tell the animator the game is over.
			anim.SetTrigger ("GameOver");

			// .. increment a timer to count up to restarting.
			restartTimer += Time.deltaTime;

			// .. if it reaches the restart delay...
			if(restartTimer >= restartDelay)
			{
				// .. then reload the level.
				SceneManager.LoadScene (0);
			}
		}
    }


	private float timeToPass = 0f;
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.Space) && player1Alive && player2Alive) {
			timeToPass -= Time.deltaTime;
			if (timeToPass <= 0f) {
				timeToPass = 2f;
				SwitchPlayers ();
			}
		}
	}

	void SwitchPlayers ()
	{
		int t = player1Stats.currentHealth;
		player1Stats.currentHealth = player2Stats.currentHealth;
		player2Stats.currentHealth = t;

		t = player1Stats.currentShield;
		player1Stats.currentShield = player2Stats.currentShield;
		player2Stats.currentShield = t;


		SwitchWeapons (player1Stats.hasGun);

		if(player1Stats.hasBubble) {
			SwitchBubbles (true);
		} else if (player2Stats.hasBubble) {
			SwitchBubbles (false);
		}
	}

	void SwitchBubbles (bool firstToSecond) {
		bool a = !firstToSecond;
		bool b = firstToSecond;

		player1Stats.hasBubble = a;
		player2Stats.hasBubble = b;
		bubble1.SetActive (a);
		bubble2.SetActive (b);
	}

	void SwitchWeapons (bool firstToSecond) {
		bool a = !firstToSecond;
		bool b = firstToSecond;

		player1Stats.hasGun = a;
		player2Stats.hasGun = b;
		gun1.SetActive (a);
		gunB1.SetActive (a);
		gun2.SetActive (b);
		gunB2.SetActive (b);
	}
			
}
