using UnityEngine;
using System.Collections;

public class TutorialTrigger5 : MonoBehaviour
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
		if (other.tag == "Player" && GameObject.Find("Vortex (2)").GetComponent<tile1properties>().getgoToWorld() == 2) {
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = true;
			tController.GetComponent<Tutorial> ().increaseConversationCounter (25);
			gController.GetComponent<GameController> ().freeze = true;
			gController.GetComponent<GameController> ().noTimer = true;
			gController.GetComponent<GameController> ().player.GetComponent<Player> ().canUse = false;
			//RenderSettings.skybox = skyBoxMaterial;
			Destroy (this.gameObject); 
		}
	}
}
