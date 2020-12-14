using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackBlock : MonoBehaviour 
{
	private float jackLifeTimer = 8.0f;

	// Use this for initialization
	void Start () 
	{
        //Make Jack last for 4 secs to match spawning time of JackController to only have max of 1 out at a time
		if(GameObject.Find("Jack Controller").GetComponent<JackController>().justOne)
        {
            jackLifeTimer = 4.0f;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		jackLifeTimer -= Time.deltaTime;
		if (jackLifeTimer <= 0) {
			Destroy (this.gameObject);
		}
	}
}
