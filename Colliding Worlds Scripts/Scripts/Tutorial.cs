using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour 
{
	public GameObject controller;
	public GameObject player;
	public Text convo;
	public Text AvinaText;
	public Image Avina;
	public AudioClip snd_conversation;
	public AudioClip snd_Avina1;
	public AudioClip snd_Avina2;
	public AudioClip snd_Avina2wLaugh;
	public bool activeTutorial = false;
	public bool aTutorial = true;
	public bool talking = true;

	private AudioSource source;
	private int conversationCounter = 0;
	private bool avinaTalk = true;

	// Use this for initialization
	void Start ()
	{
		activeTutorial = false;
		source = GetComponent<AudioSource>(); 

		if (conversationCounter == 0) {
			source.PlayOneShot (snd_Avina1, 1);
			convo.text = "Professor...?!?";
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Jump") && !source.isPlaying && talking == true) {
			//source.PlayOneShot (snd_conversation,0.65f);
			avinaTalk = true;
			if (conversationCounter == 14) {
				conversationCounter = -1;
			} else {
				conversationCounter++;
			}
			talking = false;
		}

		switch (conversationCounter)
		{
		case 1:
			talk (1);
			convo.text = "Professor...!?!";
			talking = true;
			break;
		case 2:
			talk (1);
			convo.text = "Professor. It seems we are in a predicament.";
			talking = true;
			break;
		case 3:
			talk (2);
			convo.text = "Well what's wrong is that your first field test on interdimensional travel for teleportation has created an unforseen problem.";
			talking = true;
			break;
		case 4:
			talk (2);
			convo.text = "From what I can gather there are multiple dimensional portals instead of just one as hypothesized.";
			talking = true;
			break;
		case 5:
			talk (2);
			convo.text = "Before I continue let me introduce myself...";
			talking = true;
			break;
		case 6:
			talk (2);
			convo.text = "...I am your Advanced Virtual Intelligence Navigational Assistant. You can call me Avina.";
			talking = true;
			break;
		case 7:
			talk (3);
			AvinaText.enabled = true;
			convo.text = "I was installed on your rover's systems before the field test was initiated, and it seems you are very fortunate that I was.";
			talking = true;
			break;
		case 8:
			talk (2);
			convo.text = "Continuing from before I cannot not fully comprehend what is going on here. Regardless, I know staying here is not ideal.";
			talking = true;
			break;
		case 9:
			talk (2);
			convo.text = "Give me some time to fully understand our situation. In the mean time go to that portal there using the arrow keys to move " +
				" and 'A' to turn left and 'D' to turn right. Press 'C' to change your view.";
			talking = true;
			break;
		case 10:
			talk (2);
			convo.text = "Professor, remember we are in alternate dimensions. Some rules of our dimension may be nonexistant or partially apply here. Be careful.";
			talking = true;
			break;
		case 11:
			GameObject temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = false; 
			controller.GetComponent<GameController> ().freeze = false;
			//talking = true;
			break;
		case 12:
			talk (2);
			convo.text = "Interesting. It seems we can only only move up and down or left and right from each place. " +
				"Try going right one more time and keep going up after.";
			talking = true;
			break;
		case 13:
			temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = false;
			controller.GetComponent<GameController> ().freeze = false;
			//talking = true;
			break;
		case 14:
			talk (2);
			convo.text = "Good. Try going up one more time. Don't be afraid. Just go.";
			talking = true;
			break;
		case 15:
			talk (2);
			convo.text = "Pretty, and I get what's going on. It's complicated, but you are smart so I'm sure you will not have trouble following.";
			controller.GetComponent<GameController> ().noTimer = true;
			talking = true;
			break;
		case 16:
			talk (2);
			convo.text = "Simply put we are currently in a dimension with 3 sectors. For every sector there is a portal and " +
				"a portal can bring you to a DIFFERENT sector, the SAME sector, OR it will bring us OUT of that dimension. Bringing us closer to home.";
			talking = true;
			GameObject.Find ("ETB (3)").GetComponent<Transform> ().position = GameObject.Find ("Vortex").GetComponent<Transform> ().position;
			break;
		case 17:
			talk (2);
			convo.text = "We were just in a stable dimension and where we are now is unstable. Which means" +
				" there are changes that happen in the dimension. But worry not professor while you were unconscious I was searching for a stable dimension... ";
			talking = true;
			break;
		case 18:
			talk (2);
			convo.text = " and I recognized the sectors were changing in a universal pattern. A pattern that cycles every 60 seconds.";
			talking = true;
			break;
		case 19:
			talk (2);
			convo.text = "Every 20 seconds two places in the sector we were in got deleted in a spiral clockwise fashion. " +
				"Every 30 seconds the direction of where we could go from place to place changed. Every 60 seconds...";
			talking = true;
			break;
		case 20:
			talk (2);
			convo.text = "sector 3's portal or portal 3 will move to sector 2, portal 2 to sector 1, and portal 1 dissapears I guess, and a new portal appears in sector 3. " +
				"I'm about to give you the rest of the information you need.";// Ok, professor I'm giving you full control now...";
			talking = true;
			break;
		case 21:
			talk (2);
			convo.text = "Pressing 'P' will let you use a discharge to force the direction of the place you are on. You have a limited amount but it does recharge." +
			"I'll help guide you with this dimension that has 3 sectors after I must limit my functionality to conserve the power of this vessel So get to that portal.";
			talking = true;
			GameObject.Find ("BGMusic").GetComponent<AudioSource> ().Play ();
			break;
		case 22:
			temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = false;
			controller.GetComponent<GameController> ().freeze = false;
			controller.GetComponent<GameController> ().noTimer = false;
			player.GetComponent<Player> ().setCreateBeam (1);
			player.GetComponent<Player> ().canUse = true;
			player.GetComponentInChildren<Canvas> ().enabled = true;
			break;
		case 23:
			talk (2);
			convo.text = "Professor, look at the sector location above. It still says 'sector 1'. This means to get to a different sector " +
				"we need to wait for the portals to move. When they move sector 2's current portal will come to sector 1 allowing us to get to a different sector.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			break;
		case 25:
			talk (2);
			convo.text = "Professor we are now in sector 3 of this dimension. Because the portals moved that means " +
			"we still need to check portal 3 and portal 2 as they might be the way out. So get to the portal of sector 3.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			break;
		case 27:
			talk (2);
			convo.text = "Professor we are now in sector 2. If the portals have not moved again that means " +
			"we  need to hurry to sector 2's portal as the way out must be here. But you must hurry before the portals change. So get to the portal of sector 2 " +
			"press 'P' and change anything you need to.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			player.GetComponent<Player> ().setSwitch ();
			break;
		case 29:
			talk (2);
			convo.text = "Well done Professor! I must limit my functionality now, but you are quite capable. I will always tell you at the start of each dimension how many sectors there are.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			player.GetComponent<Player> ().setSwitch ();
			break;
		case 31:
			talk (2);
			convo.text = "The portals moved. Sectors 2's portal will keep bringing us back here. We have no way to get to sector 1's portal which now " +
				"has the portal to escape. You have to wait for a new portal to come that might have the portal to escape. I must limit my functionality now " +
				"professor. I'll always tell you how many sectors there are at every new dimension.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			player.GetComponent<Player> ().setSwitch ();
			break;

		default:
			if (conversationCounter == 0 || conversationCounter == 14) {
				
			} else {
				temp = GameObject.Find ("TutorialCanvas");
				temp.GetComponent<Canvas> ().enabled = false;
				controller.GetComponent<GameController> ().freeze = false;
				controller.GetComponent<GameController> ().noTimer = false;
				player.GetComponent<Player> ().canUse = true;
			}
			break;
		}
	}

	public void talk(int phrase)
	{
		

		if(avinaTalk)
		{
			switch (phrase)
			{
			case 1:
				source.PlayOneShot (snd_Avina1, 1);
				break;
			case 2:
				source.PlayOneShot (snd_Avina2, 1);
				break;
			case 3:
				source.PlayOneShot (snd_Avina2wLaugh, 1);
				break;
			default:
				
				break;
			}
		}
		avinaTalk = false;
	}

	public void increaseConversationCounter(int inc)
	{
		conversationCounter = inc;
	}
	/*
	 * case 16:
			talk (2);
			convo.text = "Simply put and generally speaking we are currently in a dimension with an x number of dimensional portals, or just portals. " +
			"The places surrounding each portal we will refer to them as a world. For every portal there is a world, for every world there is a portal. " +
			"A portal can bring you to another ";
			talking = true;
			break;
		case 17:
			talk (2);
			convo.text = "World or it will bring us out of that dimension. Bringing us closer to home.We were also just in a stable dimension. Where we are currently is very unstable and by that it seems" +
			" where each of the portals will teleport you move in a simple timed fashion in which if there are ";
			talking = true;
			break;
		case 18:
			talk (2);
			convo.text = "three worlds a new portal will move to world 3, portal 3 will move to world 2, portal 2 to world 1, and I'm not sure where portal 1 goes.";
			talking = true;
			break;
		case 19:
			talk (2);
			convo.text = "Some more information is that if none of the x portals let us escape that are initially here when we first arrive in the dimension then when the portals move " +
			"what will be portal 3, in my example of a dimension of three worlds, should always be the one to escape.";
			talking = true;
			break;
		case 20:
			talk (2);
			convo.text = "If it so happens we miss the portal to leave; a new portal should come to portal 3, in my example, or generally the highest numbered portal. " +
			"Let me give you the information you need. Doing all this has used a lot of power so I need to limit my functionality soon.";
			talking = true;
			break;
		case 21:
			talk (2);
			convo.text = "Oh, while you were unconscious and while I was searching for a semi-stable dimension they were changing in a repeated pattern. " +
			"That pattern cycles every 60 seconds.";
			talking = true;
			break;
		case 22:
			talk (2);
			convo.text = "Every 20 seconds two places in the world we were in got deleted in a clockwise fashion. Every 30 seconds " +
			"the direction of the places changed and every 60 seconds is when the portals would move like I explained before.";
			talking = true;
			break;
		case 23:
			talk (2);
			convo.text = "Ok, professor I'm giving you full control now which will let you use some energy in a discharge to force the direction of the places. " +
				"You have a limited amount but it does recharge slowly. I'll at least help guide you with this dimension that has 3 worlds after I must limit my functionality to conserve " +
			"the power of this vessel. So get to that portal.";
			talking = true;
			break;
		case 24:
			temp = GameObject.Find ("TutorialCanvas");
			temp.GetComponent<Canvas> ().enabled = false;
			controller.GetComponent<GameController> ().freeze = false;
			controller.GetComponent<GameController> ().noTimer = false;
			player.GetComponent<Player> ().setCreateBeam (1);
			player.GetComponent<Player> ().canUse = true;
			player.GetComponentInChildren<Canvas> ().enabled = true;
			break;
		case 25:
			talk (2);
			convo.text = "Professor we are now in world 3 of this dimension. Because the portals moved that means " +
			"we still need to check world 3 and world 2 as they might be the way out. So get to the portal of world 3.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			break;
		case 27:
			talk (2);
			convo.text = "Professor we are now in world 2 of this dimension. If the portals have not moved again that means " +
			"we  need to hurry to world 2's portal as the way out must be here. But you must hurry before the portals change. So get to the portal of world 2 " +
			"press 'P' and change anything you need to.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			player.GetComponent<Player> ().setSwitch ();
			break;
		case 29:
			talk (2);
			convo.text = "Well done Professor! I must limit my functionality now, but you are quite capable. I will always tell you at the start of each dimension how many worlds there are.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			player.GetComponent<Player> ().setSwitch ();
			break;
		case 31:
			talk (2);
			convo.text = "The portals moved. World 2's portal will keep bringing us to world 2. We have no way to get to world 1's portal which now " +
				"has the portal to escape. You have to wait for a new portal to come that might have the portal to escape. I must limit my functionality now " +
				"professor. I'll always tell you how many worlds there are at every new dimension.";
			talking = true;
			controller.GetComponent<GameController> ().noTimer = true;
			player.GetComponent<Player> ().canUse = false;
			player.GetComponent<Player> ().setSwitch ();
			break;
			*/
}