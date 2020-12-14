using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimerSquare : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    /// <summary>
    /// Shrinks the cube only on the Y by the current timer/the starting timer number.
    /// </summary>
    /// <param name="targetTime">The current countdown of the timer. </param>
    /// <param name="startTimer">The starting number of the timer. </param>
    public void ShrinkCube(float targetTime, float startTimer)
	{
		transform.localScale = new Vector3 (1.0f, targetTime/startTimer, 1.0f);
	}

    /// <summary>
    /// Grow the cube only on the Y by the current timer/the starting timer number.
    /// </summary>
    /// <param name="targetTime">The current countdown of the timer. </param>
    /// <param name="startTimer">The starting number of the timer. </param>
	public void GrowCube(float targetTime, float startTimer)
	{
        /*
		if (growth == 2) {
			transform.localScale += new Vector3 (0.0f, 2.0f - transform.localScale.y, 0.0f);
		} else {
			transform.localScale += new Vector3 (0.0f, growth, 0.0f);
		}
        */
        transform.localScale = new Vector3(0.0f, targetTime/startTimer, 0.0f);
    }
}