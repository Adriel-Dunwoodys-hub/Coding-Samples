using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUps : MonoBehaviour
{
	public bool startAlarm = false;
	public float timer = 3.0f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (startAlarm == true) {
			alarm ();
		}


	}

	public void alarm()
	{
		
		timer -= Time.deltaTime;
		if (Mathf.Round(timer) == 0.0f) 
		{
			Destroy (this.gameObject);
			startAlarm = false;
			timer = 3.0f;
		}
	}
}
