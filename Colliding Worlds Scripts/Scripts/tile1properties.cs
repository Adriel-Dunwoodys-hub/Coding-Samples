using UnityEngine;
using System.Collections;

public class tile1properties : MonoBehaviour 
{
	//private variables
	public bool middleTile = false; //var to tell if tile is the middle tile of the world
	public bool upOrDown = true;//var that dictates if player can move up or down if true and if false left or right to another tile
	public int goToWorld;//a value from 0-x for what world the player will go when reaching middleTile
	public bool isElevator = false;//if a tile is an elevator that'll go up and down from its current floor
	public bool canCollapse = false;//if a tile is an collapsing tile. Collapses after player exits the tile
	public float timer = 3.0f;//timer for how long tile should stay still for
	public float timer2 = 8.0f;//timer for how long it should move for

	private bool moveUp = false;
	private bool move = false;
	private bool goingDown = false;
	//private GameObject child;

	// Use this for initialization
	void Start () 
	{
		setUpOrDown (Mathf.RoundToInt (Random.Range (0.0f, 1.0f)));
		if (this.isElevator == true) {
			Transform firstChild = transform.GetChild (0);
			if (firstChild != null) {
				firstChild.gameObject.SetActive (false);
				// Destroy (firstChild.gameObject);
			}
			/*foreach (Transform child in this.transform.GetChild(0)) {
				GameObject.Destroy (child.gameObject);
			}*/
		}

		if (GameObject.Find ("TutorialController") == null) {
		} else {
			if (GameObject.Find ("TutorialController").GetComponent<Tutorial> ().aTutorial == true) {
				GameObject temp = GameObject.Find ("GameController");
				temp.GetComponent<GameController> ().world1 [12].GetComponent<tile1properties> ().upOrDown = false;
				temp.GetComponent<GameController> ().world1 [11].GetComponent<tile1properties> ().upOrDown = false;
				temp.GetComponent<GameController> ().world1 [10].GetComponent<tile1properties> ().upOrDown = true;
				temp.GetComponent<GameController> ().world1 [21].GetComponent<tile1properties> ().upOrDown = true;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (this.isElevator == true) {
			timer -= Time.deltaTime;
			if (Mathf.Round (timer) == 0.0f) {
				this.move = true;
				if (moveUp == false && goingDown == false) {
					moveUp = true;
				}
				timer2 = 3.5f;
			}
			if (this.move == true) {
				timer2 -= Time.deltaTime;
				if (moveUp == true) {
					transform.Translate (new Vector3 (0.0f, 2.0f, 0.0f) * Time.deltaTime);
				} else {
					transform.Translate (new Vector3 (0.0f, -2.0f, 0.0f) * Time.deltaTime);
				}
				if (Mathf.Round (timer2) == 0.0f) {
					this.move = false;
					this.moveUp = false;
					goingDown = !goingDown;
					timer2 = 0.0f;
					timer = 3.0f;
				}
			}
		}

	}


	/// <summary>
	///access private variable middleTile to set to true
	/// </summary>
	public void setMiddleTile()
	{
		this.middleTile = true;
	}

	/// <summary>
	///returns middleTile
	/// </summary>
	public bool getMiddleTile()
	{
		return this.middleTile;
	}

	/// <summary>
	///access private variable upOrDown passing in an int 1 or 0
	///1 sets variable to true and 0 sets it to false
	/// </summary>
	public void setUpOrDown(int val)
	{
		if (val == 1) {
			this.upOrDown = true;
		} 
		else if (val == 0) 
		{
			this.upOrDown = false;
		}
	}

	/// <summary>
	///void function that changes tiles upOrDown variable to what it is not.
	/// from true to false or false to true
	/// </summary>
	public void swapDirection()
	{
		if (this.upOrDown == true) {
			this.upOrDown = false;
			this.transform.Rotate (0.0f, 90.0f, 0.0f);
		} else {
			this.upOrDown = true;
			this.transform.Rotate (0.0f, 0.0f, 0.0f);
		}
	}

	/// <summary>
	///returns private variable upOrDown
	/// </summary>
	public bool getUpOrDown()
	{
		return this.upOrDown;
	}

	/// <summary>
	///sets the middleTile's goToWorld int variable.
	/// param worldNumber is an int that represents what world player will go to
	/// </summary>
	public void setgoToWorld(int worldNumber)
	{
		this.goToWorld = worldNumber;
	}

	/// <summary>
	/// returns middle tile's worldNumber variable
	/// </summary>
	public int getgoToWorld()
	{
		return this.goToWorld;
	}
}
