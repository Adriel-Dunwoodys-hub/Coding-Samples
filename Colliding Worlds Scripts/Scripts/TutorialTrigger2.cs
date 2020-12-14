using UnityEngine;
using System.Collections;

public class TutorialTrigger2 : MonoBehaviour
{

	private GameObject gController;
	private GameObject tController;
	public Color triggerboxColor;
	// Use this for initialization
	void Start ()
	{
		gController = GameObject.Find ("GameController");
		tController = GameObject.Find ("TutorialController");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = true;
			tController.GetComponent<Tutorial> ().increaseConversationCounter (14);
			gController.GetComponent<GameController> ().freeze = true;
			Destroy (this.gameObject); 
			/*
			tController.GetComponent<Tutorial> ().convo.text = "Interesting. It seems we can only only move in certain directions from place to place. " +
				"Try going right one more time and keep going up after.";
				*/
		}
	}
}
