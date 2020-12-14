using UnityEngine;
using System.Collections;

public class AlwaysRotate : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		keepRotating ();

	}

	public void keepRotating()
	{
		this.transform.Rotate (Random.Range(-15,15),Random.Range(-15,15),Random.Range(-15,15));
	}
}