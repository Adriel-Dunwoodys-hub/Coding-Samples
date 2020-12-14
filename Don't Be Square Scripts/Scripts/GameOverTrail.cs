using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrail : MonoBehaviour {

	public int scaleSpeed = 5;
	public float currCountdownValue;
	public bool stopTrail = false;
	public GameObject gameController;
	public bool beginAni = true;

	// Use this for initialization
	void Start () 
	{
		scaleSpeed = 5;
		gameController = GameObject.Find("Game Controller");
	}

	// Update is called once per frame
	void Update () 
	{
		if (gameController.GetComponent<GameController> ().getGameOver ())
		{
			if (beginAni) {
				beginAni = false;
				StartCoroutine (StartCountdown());
			}
			if (!stopTrail) {
				transform.localScale -= new Vector3 (0.0f, 1.0f * (Time.deltaTime * scaleSpeed), 0.0f);
			}
		}
	}

	public IEnumerator StartCountdown(float countdownValue = 4)
	{
		currCountdownValue = countdownValue;
		while (currCountdownValue > 0) {
			yield return new WaitForSeconds (0.1f);
			currCountdownValue-=.1f;
		}
		stopTrail = true;
	}
}
