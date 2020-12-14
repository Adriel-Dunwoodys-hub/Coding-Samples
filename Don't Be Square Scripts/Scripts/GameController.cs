using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour 
{
	private bool gameOver = false;
    private bool boolFlag = true;
    private float gameOverUITimer = 4.1f;
	public GameObject playAgainButton;
	public GameObject mainMenuButton;
    public GameObject levelCreator;
	private GameObject[] orbs;
	private GameObject[] jacks;
	private Text gameOverUIText;
    private Text scoreText;
	

	// Use this for initialization
	void Start () 
	{
		gameOverUIText = GameObject.Find ("Game Over").GetComponent<Text> ();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        playAgainButton = GameObject.Find ("Play Again Button");
		mainMenuButton = GameObject.Find ("Main Menu Button");
		playAgainButton.SetActive (false);
		mainMenuButton.SetActive (false);
        levelCreator = GameObject.Find("Puzzle_Creator");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameOver) {
			if (gameOverUITimer > 0) {
				gameOverUITimer -= Time.deltaTime;
			}
		}
        else
        {
            GameStart.score++;
        }

		if (gameOverUITimer <= 0) {
			gameOverUIText.enabled = true;
			playAgainButton.SetActive (true);
			mainMenuButton.SetActive (true);
			Destroy (GameObject.Find ("Puzzle_Creator"));
			Destroy (GameObject.Find ("Jack Controller"));
			Destroy (GameObject.Find ("Cube Timer Glass"));
			Destroy (GameObject.Find ("Cube Timer Graphic"));
			if (boolFlag)
			{
				orbs = GameObject.FindGameObjectsWithTag ("orb");
				jacks = GameObject.FindGameObjectsWithTag ("jack");
				//delete orbs
				for (int i = 0; i < orbs.Length; i++) {
					Destroy (orbs [i]);
				}
				//delete jacks
				for (int i = 0; i < jacks.Length; i++) {
					Destroy (jacks [i]);
				}
				boolFlag = false;
			}
		}

        scoreText.text = "Score: " + GameStart.score;

        if(Input.GetKeyDown(KeyCode.P))
        {
            levelCreator.GetComponent<LevelCreator>().DeleteOneColorPowerup();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            GameObject.Find("Clock").GetComponent<timer>().SetFreezeTimer(true);
        }
    }

	/// <summary>
	/// sets gameOver to true and does actions not that player lost
	/// </summary>
	public void gameNowOver()
	{
		gameOver = true;
	}

	/// <summary>
	/// Gets the gameOver variable.
	/// </summary>
	/// <returns><c>true</c>, if gameOver was gotten, <c>false</c> otherwise.</returns>
	public bool getGameOver()
	{
		return gameOver;
	}
}
