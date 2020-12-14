using UnityEngine;
using System.Collections;

public class TutorialTrigger3 : MonoBehaviour 
{

	private GameObject gController;
	private GameObject tController;
	public Color triggerboxColor;
	public Material skyBoxMaterial;
	// Use this for initialization
	void Start ()
	{
		gController = GameObject.Find ("GameController");
		tController = GameObject.Find ("TutorialController");
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = true;
			tController.GetComponent<Tutorial> ().increaseConversationCounter (15);
			gController.GetComponent<GameController> ().freeze = true;
			RenderSettings.skybox = skyBoxMaterial;
			Destroy (this.gameObject); 
		}
	}
}
