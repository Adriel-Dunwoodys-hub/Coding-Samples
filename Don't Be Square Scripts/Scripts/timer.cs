using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class timer : MonoBehaviour 
{
    private float targetTime = 0.0f;
    private Text timerText;
    private bool freezeTime = false;
    private float freezeTimer = 5.0f;
    private GameObject cube_Timer_Fill;

    public float startTimer = 0.0f;
	public GameObject timerCube;
	public float currCountdownValue;
	public GameObject gameController;
    public Material frozen_Cube_Timer_Fill_Color;
    public Material regular_Cube_Timer_Fill_Color;

	// Use this for initialization
	void Start () {
        targetTime = startTimer;
		timerText = GameObject.Find ("Clock").GetComponent<Text> ();
		timerText.text = ((int)targetTime).ToString();
		//StartCoroutine (StartCountdown());
		gameController = GameObject.Find ("Game Controller");
        cube_Timer_Fill = GameObject.Find("Cube Timer Fill");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!gameController.GetComponent<GameController> ().getGameOver()) {
			if (targetTime > 0 && freezeTime == false) {
				targetTime -= Time.deltaTime;
                timerCube.GetComponent<TimerSquare>().ShrinkCube(targetTime,startTimer);
				timerText.text = ((int)targetTime).ToString ();
			}
			//timerCube.GetComponent<TimerSquare> ().shrinkCube ();


			if (targetTime <= 0.0f) {
				gameController.GetComponent<GameController> ().gameNowOver ();
				timerText.enabled = false;
			}
		}

        if(freezeTime == true)
        {
            freezeTimer -= Time.deltaTime;
            if(freezeTimer <=0.0f)
            {
                freezeTime = false;
                cube_Timer_Fill.GetComponent<Renderer>().material = regular_Cube_Timer_Fill_Color;
                freezeTimer = 5.0f;
            }
        }
	}
    /*
	public IEnumerator StartCountdown(float countdownValue = 20)
	{
		currCountdownValue = countdownValue;
		while (currCountdownValue > 0) {
			yield return new WaitForSeconds (1.0f);
			currCountdownValue--;
			timerCube.GetComponent<TimerSquare> ().shrinkCube ();
		}
	}
    */

	/// <summary>
	/// Get the timer of the board.
	/// </summary>
	/// <returns>The current board clock time.</returns>
	public float getTimer()
	{
		return targetTime;
	}

	/// <summary>
	/// Add or subtract to current timer.
	/// </summary>
	/// <param name="add">A positive or negative int to add to timer.</param>
	public void addToTimer(float add)
	{
		if (targetTime + add > 20.0f) {
			targetTime = 20.0f;
			//currCountdownValue += 10.0f;
			timerCube.GetComponent<TimerSquare> ().GrowCube (targetTime, startTimer);
		} else {
			targetTime += add;
			//currCountdownValue += 10;
			timerCube.GetComponent<TimerSquare> ().GrowCube (targetTime, startTimer);
		}
	}

    /// <summary>
    /// Set the variable freezeTime to true or false.
    /// </summary>
    /// <param name="value">A boolean value to set freezeTime to.</param>
    public void SetFreezeTimer(bool value)
    {
        freezeTime = value;
        cube_Timer_Fill.GetComponent<Renderer>().material = frozen_Cube_Timer_Fill_Color;
    }

    /// <summary>
    /// Returns the boolean variable freezeTime.
    /// </summary>
    /// <returns>The variable freezeTime</returns>
    public bool GetFreezeTimer()
    {
        return freezeTime;
    }
}