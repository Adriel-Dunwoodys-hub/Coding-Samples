using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour 
{
	private tile1properties aTile; //create instance of tile1properties to access functions
	private int ii = 1;
	private int jj = 0;
	private bool won = false;

	public GameObject tile1Prefab;//created a public var to use as prefab for world creation
	public GameObject lightningPrefab;//created a public var to use as prefab for world creation
	public GameObject player;
	public GameObject tController;
	public Collision dirTile;
	public int universeNum = 1; //number for what universe to tell number of worlds for it
	public int counter = 0; // var to help find out the world number
	public int worldNum = 0; //number of worlds to go by
	public int worldSize = 5; //how large each world is default is 5x5
	public float timer = 60.0f;//timer for when to delete,rotate,and change worlds
	public int deleter1 = 0;//counter to use to know what to delete from world1 array
	public int deleter2 = 0;//counter to use to know what to delete from world2 array
	public int deleter3 = 0;//counter to use to know what to delete from world3 array
	public int worldLocation = 1;//what world the player is currently in. default is 1
	public bool freeze = false;
	public bool noTimer = false;

	public int[] worldTiles;//array used to store which world goes to which one
	public GameObject[] world1;//array holds tiles in world 1 floor 1
	public GameObject[] world12;//array holds tiles in world 1 floor 2
	public GameObject[] world2;//array holds all tiles in world 2
	public GameObject[] world3;//array holds all tiles in world 3

	private AudioSource source;
	public AudioClip snd_delete;
	public AudioClip snd_rotate;
	public AudioClip snd_changeAll;

	public Text gameOverText;
	public Text gameOverText2;
	public Text counterText;
	public Text gameText;


	// Use this for initialization
	void Start ()
	{
		worldNum = universeNum + (2 + counter);
		int[] temp =  new int[worldNum+ 1]; //set up first half of goToWorld locations
		int[] temp2 = new int[worldNum + 1];
		int[] temp0 = new int[worldNum + 1];
		worldTiles = new int[2 * (worldNum + 1)];
		//for loop fills up temp with no duplicates
		int i = 0;
		for (i = 0; i < temp0.Length; i++)
		{
			temp0 [i] = i;
		}
		i = 0;
		int upperBound = 0;
		upperBound = worldNum;
		while(i < temp.Length)
		{
			int k = Mathf.RoundToInt (Random.Range (0.0f, upperBound));
			temp [i] = temp0 [k];
			for (int a = k; a < temp0.Length-1; a++)
			{
				temp0 [a] = temp0 [a + 1];
			}
			upperBound--;
			i++;
		}
		//filling up temp 2 with no duplicates
		for (i = 0; i < temp0.Length; i++)//reset temp0
		{
			temp0 [i] = i;
		}
		upperBound = worldNum;//reset upperBound
		i = 0;//reset i
		while(i < temp2.Length)
		{
			int k = Mathf.RoundToInt (Random.Range (0.0f, upperBound));
			temp2 [i] = temp0 [k];
			for (int a = k; a < temp0.Length-1; a++)
			{
				temp0 [a] = temp0 [a + 1];
			}
			upperBound--;
			i++;
		}
		//filling up array worldTiles with temp and temp2. temp goes first
		for (i = 0; i < temp.Length; i++)
		{
			worldTiles [i] = temp [i];
		}
		int p = 0;
		for (i = 4; i < worldTiles.Length; i++)
		{
			worldTiles [i] = temp2 [p];
			p++;
		}
		//initially assigning middletiles to a goToWorld Number
		GameObject middleInstance = GameObject.FindGameObjectWithTag("world1");
		middleInstance.GetComponent<tile1properties> ().setgoToWorld (worldTiles [0]);

		middleInstance = GameObject.FindGameObjectWithTag("world2");
		middleInstance.GetComponent<tile1properties> ().setgoToWorld (worldTiles [1]);

		middleInstance = GameObject.FindGameObjectWithTag("world3");
		middleInstance.GetComponent<tile1properties> ().setgoToWorld (worldTiles [2]);

		//Instantiate the player into the outermost location in the current world
		jj = ii;
		spawning();

		//Instantiate text to empty string
		gameOverText.text = "";
		gameOverText2.text = "";
		gameText.text = "";

		source = GetComponent<AudioSource>();
		if (GameObject.Find ("TutorialController") == null) {
		} else {
			if (GameObject.Find ("TutorialController").GetComponent<Tutorial> ().aTutorial == true) {
				freeze = true;
				noTimer = true;
				player.GetComponent<Transform> ().position = new Vector3 (world1 [12].transform.position.x, 1f, world1 [12].transform.position.z);
				worldTiles [0] = 1;
				worldTiles [1] = 3;
				worldTiles [2] = 0;
				worldTiles [3] = 2;

				GameObject.Find ("Vortex").GetComponent<tile1properties> ().setgoToWorld (worldTiles [0]);
				GameObject.Find ("Vortex (1)").GetComponent<tile1properties> ().setgoToWorld (worldTiles [1]);
				GameObject.Find ("Vortex (2)").GetComponent<tile1properties> ().setgoToWorld (worldTiles [2]);
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{

		if (!player.GetComponent<Player> ().checkGameOver () && !player.GetComponent<Player>().checkPlayerWin() && noTimer == false)
		{
			alarm ();
		}
		if(player.GetComponent<Player>().checkGameOver())
		{
			gameOverText.text = "GAME OVER!";
			gameOverText2.text = "Press 'Enter' to go to main menu ";
			if (!System.IO.File.Exists ("C:\\Users\\Adriel\\Desktop\\CTIN484\\Project\\MetaData.txt")) {
				System.IO.File.Create ("C:\\Users\\Adriel\\Desktop\\CTIN484\\Project\\MetaData.txt");
			}
			System.IO.File.AppendAllText("C:\\Users\\Adriel\\Desktop\\CTIN484\\Project\\MetaData.txt", "Time lasted in last scene before Game Over: " + Time.timeSinceLevelLoad);
			System.IO.File.AppendAllText("C:\\Users\\Adriel\\Desktop\\CTIN484\\Project\\MetaData.txt", "  Amount of powerup used: " + player.GetComponent<Player>().metadataPowerUp + "\n");
			System.IO.File.AppendAllText("C:\\Users\\Adriel\\Desktop\\CTIN484\\Project\\MetaData.txt", "\t\t");
		}

		if (Input.GetKeyDown (KeyCode.Return) && player.GetComponent<Player> ().checkGameOver ()) 
		{
			//Go to Main Menu room
			SceneManager.LoadSceneAsync (0);
		}

		if(player.GetComponent<Player>().checkPlayerWin())
		{
			gameOverText2.text = "Press 'Enter' to continue ";
		}

		if (Input.GetKeyDown (KeyCode.Return) && player.GetComponent<Player>().checkPlayerWin()) 
		{
			int indexSC = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(indexSC+1);
		}
	}

	public void levelTransition()
	{
		//float fadeTime = this.GetComponent<Fading> ().BeginFade (1);
		//yield return new WaitForSeconds (fadeTime);
		//Go to next scene
	}

	/// <summary>
	/// A void function that has an alarm to track when to change the current world
	/// </summary>
	public void alarm()
	{
		counterText.text = timer.ToString ("00");
		gameText.CrossFadeAlpha (0.0f, 1.5f, true);
		timer -= Time.deltaTime;
		if (Mathf.Round(timer) == 0.0f) 
		{
			gameText.text = "World Locations have moved down. All tiles switched and two tiles in " + GameObject.Find("WorldLocation").GetComponent<Text>().text + " have been deleted.";
			gameText.CrossFadeAlpha (1.0f, 0.0f, true);
			delete ();
			rotateTiles();
			rotateMiddleTiles ();
			timer = 60.0f;//reset timer back to 60
			source.PlayOneShot (snd_changeAll, 1f);
		}
		else
			if (Mathf.Floor(timer) == 20.0f) 
			{
				gameText.text = "Two tiles in " + GameObject.Find("WorldLocation").GetComponent<Text>().text + " have been deleted.";
				gameText.CrossFadeAlpha (1.0f, 0.0f, true);
				delete ();
				timer = Mathf.Floor(timer);
				source.PlayOneShot (snd_delete, 1f);
			}
			else
				if (Mathf.Floor(timer) == 30.0f) 
				{
					gameText.text = "All tiles have switched directions.";
					gameText.CrossFadeAlpha (1.0f, 0.0f, true);
					rotateTiles();
					if (player.GetComponent<Player> ().getCreateBeam ())
					{
						player.GetComponent<Player> ().beamCreation (dirTile);
					}
					timer = Mathf.Floor(timer);
					source.PlayOneShot (snd_rotate, 1f);
				}
				else
					if (Mathf.Floor(timer) == 40.0f) 
					{
						gameText.text = "Two tiles in " + GameObject.Find("WorldLocation").GetComponent<Text>().text + " have been deleted.";
						gameText.CrossFadeAlpha (1.0f, 0.0f, true);
						delete ();
						timer = Mathf.Floor(timer);
						source.PlayOneShot (snd_delete, 1f);
					}
	}

	/// <summary>
	/// A void function that rotates all the tiles direction in the current world
	/// </summary>
	public void rotateTiles()
	{
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
		foreach (GameObject tile in tiles) 
		{
			tile.GetComponent<tile1properties> ().swapDirection ();
		}
	}
	public void rotateMiddleTiles()
	{
		int temp;//var to hold world1's value to be shifted to the end as the final shift
		GameObject middleInstance = GameObject.FindGameObjectWithTag("world1");
		GameObject middleInstance2 = GameObject.FindGameObjectWithTag("world2");
		temp = middleInstance.GetComponent<tile1properties> ().getgoToWorld ();
		middleInstance.GetComponent<tile1properties> ().setgoToWorld (middleInstance2.GetComponent<tile1properties> ().getgoToWorld ());
		middleInstance = GameObject.FindGameObjectWithTag("world3");
		middleInstance2.GetComponent<tile1properties> ().setgoToWorld (middleInstance.GetComponent<tile1properties> ().getgoToWorld ());/*middleInstance this case is
		world3's value*/

		middleInstance.GetComponent<tile1properties> ().setgoToWorld (worldTiles [3]);//setting world3's value

		int i = 0;
		for (i = 0; i < worldTiles.Length - 1; i++)
		{
			worldTiles [i] = worldTiles [i + 1];
		}
		worldTiles [i] = temp;
	}

	public void delete()
	{
		if (worldLocation == 1)
		{
			GameObject lightning1 = GameObject.Instantiate (lightningPrefab);
			lightning1.GetComponent<PowerUps> ().startAlarm = true;
			lightning1.transform.Rotate (0, 0, 90.0f);
			lightning1.transform.position = new Vector3 ( world1 [deleter1].transform.position.x+1.5f, 4.5f,world1 [deleter1].transform.position.z);
			lightning1 = GameObject.Instantiate (lightningPrefab);
			lightning1.GetComponent<PowerUps> ().startAlarm = true;
			lightning1.transform.Rotate (0, 0, 90.0f);
			lightning1.transform.position = new Vector3 ( world1 [deleter1+1].transform.position.x+1.5f, 4.5f,world1 [deleter1+1].transform.position.z);
			Destroy (world1 [deleter1]);
			deleter1++;
			Destroy (world1 [deleter1]);
			deleter1++;
		}
		if (worldLocation == 2)
		{
			GameObject lightning1 = GameObject.Instantiate (lightningPrefab);
			lightning1.GetComponent<PowerUps> ().startAlarm = true;
			lightning1.transform.Rotate (0, 0, 90.0f);
			lightning1.transform.position = new Vector3 ( world2 [deleter2].transform.position.x+1.5f, 4.5f,world2 [deleter2].transform.position.z);
			lightning1 = GameObject.Instantiate (lightningPrefab);
			lightning1.GetComponent<PowerUps> ().startAlarm = true;
			lightning1.transform.Rotate (0, 0, 90.0f);
			lightning1.transform.position = new Vector3 ( world2 [deleter2+1].transform.position.x+1.5f, 4.5f,world2 [deleter2+1].transform.position.z);
			Destroy (world2 [deleter2]);
			deleter2++;
			Destroy (world2 [deleter2]);
			deleter2++;
		}
		if (worldLocation == 3)
		{
			GameObject lightning1 = GameObject.Instantiate (lightningPrefab);
			lightning1.GetComponent<PowerUps> ().startAlarm = true;
			lightning1.transform.Rotate (0, 0, 90.0f);
			lightning1.transform.position = new Vector3 ( world3 [deleter3].transform.position.x+1.5f, 4.5f,world3 [deleter3].transform.position.z);
			lightning1 = GameObject.Instantiate (lightningPrefab);
			lightning1.GetComponent<PowerUps> ().startAlarm = true;
			lightning1.transform.Rotate (0, 0, 90.0f);
			lightning1.transform.position = new Vector3 ( world3 [deleter3+1].transform.position.x+1.5f, 4.5f,world3 [deleter3+1].transform.position.z);
			Destroy (world3 [deleter3]);
			deleter3++;
			Destroy (world3 [deleter3]);
			deleter3++;
		}
	}


	public void newSpawning()
	{
		if (worldLocation == 1) {
			if (deleter1 + 1 == 3 * (worldNum + 2) + ii) {
				jj -= 2;
			}
			int blah = Mathf.RoundToInt (Random.Range (deleter1, 3 * (worldNum + 2) + jj));
			Instantiate (player, new Vector3(world1 [blah].transform.position.x, world1 [blah].transform.position.y + 1, world1 [blah].transform.position.z),
				Quaternion.identity);
		}
	}


	public void spawning()
	{
		if (worldLocation == 1) {
			if (deleter1 + 1 == 3 * (worldNum + 2) + ii) {
				jj -= 2;
			}
			int blah = Mathf.RoundToInt (Random.Range (deleter1, 3 * (worldNum + 2) + jj));
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.transform.position = new Vector3 (world1 [blah].transform.position.x, world1 [blah].transform.position.y + 1, world1 [blah].transform.position.z);
			if (GameObject.Find ("TutorialController") == null) {
			} else {
				if (tController.GetComponent<Tutorial> ().aTutorial == true) {
					player.GetComponent<Transform> ().position = new Vector3 (world1 [12].transform.position.x, 1f, world1 [12].transform.position.z);
				}
			}
		}

		if (worldLocation == 2) {
			if (deleter2 + 1 == 3 * (worldNum + 2) + ii) {
				jj -= 2;
			}
			int blah = Mathf.RoundToInt (Random.Range (deleter1, 3 * (worldNum + 2) + jj));
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.transform.position = new Vector3 (world2 [blah].transform.position.x, world2 [blah].transform.position.y + 1, world2 [blah].transform.position.z);
		}

		if (worldLocation == 3) {
			if (deleter3 + 1 == 3 * (worldNum + 2) + ii) {
				jj -= 2;
			}
			int blah = Mathf.RoundToInt (Random.Range (deleter1, 3 * (worldNum + 2) + jj));
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			player.transform.position = new Vector3 (world3 [blah].transform.position.x, world3 [blah].transform.position.y + 1, world3 [blah].transform.position.z);
		}
	}
}