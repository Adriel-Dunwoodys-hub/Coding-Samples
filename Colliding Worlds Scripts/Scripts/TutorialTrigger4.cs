using UnityEngine;
using System.Collections;

public class TutorialTrigger4 : MonoBehaviour
{

	private GameObject gController;
	private GameObject tController;
	private bool enteredOnce = false;

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
		if (other.tag == "Player" && GameObject.Find ("Vortex").GetComponent<tile1properties> ().getgoToWorld () == 3) {
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = true;
			tController.GetComponent<Tutorial> ().increaseConversationCounter (23);
			gController.GetComponent<GameController> ().freeze = true;
			gController.GetComponent<GameController> ().noTimer = true;
			gController.GetComponent<GameController> ().player.GetComponent<Player> ().canUse = false;
			//RenderSettings.skybox = skyBoxMaterial;
			Destroy (this.gameObject); 
		} else {
			if (other.tag == "Player" && GameObject.Find ("Vortex").GetComponent<tile1properties> ().getgoToWorld () == 1 && enteredOnce == false) {
				GameObject temp = GameObject.Find ("TutorialCanvas");
				temp.GetComponent<Canvas> ().enabled = true;
				tController.GetComponent<Tutorial> ().increaseConversationCounter (23);
				gController.GetComponent<GameController> ().freeze = true;
				gController.GetComponent<GameController> ().noTimer = true;
				gController.GetComponent<GameController> ().player.GetComponent<Player> ().canUse = false;
				//RenderSettings.skybox = skyBoxMaterial;
				enteredOnce = true;
				Debug.Log (enteredOnce);
			}
		}
	}
}
