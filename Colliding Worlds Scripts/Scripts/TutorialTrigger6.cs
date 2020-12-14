using UnityEngine;
using System.Collections;

public class TutorialTrigger6 : MonoBehaviour 
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
		if (other.tag == "Player" && GameObject.Find("Vortex (1)").GetComponent<tile1properties>().getgoToWorld() == 0) {
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = true;
			tController.GetComponent<Tutorial> ().increaseConversationCounter (27);
			gController.GetComponent<GameController> ().freeze = true;
			gController.GetComponent<GameController> ().noTimer = true;
			gController.GetComponent<GameController> ().player.GetComponent<Player> ().canUse = false;
			tController.GetComponent<Tutorial> ().aTutorial = false;
			//RenderSettings.skybox = skyBoxMaterial;
			Destroy (this.gameObject); 
		}

		if (other.tag == "Player" && GameObject.Find("Vortex (1)").GetComponent<tile1properties>().getgoToWorld() != 0) {
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = true;
			tController.GetComponent<Tutorial> ().increaseConversationCounter (29);
			gController.GetComponent<GameController> ().freeze = true;
			gController.GetComponent<GameController> ().noTimer = true;
			tController.GetComponent<Tutorial> ().aTutorial = false;
			gController.GetComponent<GameController> ().player.GetComponent<Player> ().canUse = false;
			//RenderSettings.skybox = skyBoxMaterial;
			Destroy (this.gameObject); 
		}
	}
}