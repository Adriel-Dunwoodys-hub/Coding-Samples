using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// Public variables that will show up in the Editor
	public float Acceleration = 50f;
	public float MaxSpeed = 20f;
	public float JumpStrength = 500f;

	public AudioClip win;
	public AudioClip snd_jump;
	public AudioClip snd_land;
	public AudioClip snd_lostLife;
	public AudioClip snd_gameOver;
	public AudioClip snd_switch;
	public AudioClip snd_changeWorld;

	public Sprite upDown;
	public Sprite leftRight;
	public GameObject wall;
	public GameObject Beam;
	public GameObject gController;
	public Text numOfLives;
	public Text switchUI;
	public float timer = 50.0f;//timer for when to add to powerups
	public float timer2 = 7.0f;//timer for going to the next scene after winning

	private bool createBeam = true;
	private AudioSource source;
	private Image img;
	public int metadataPowerUp;
	private int lives = 3;
	private int Switch;//powerup that allows player to rotate the current tile they are on
	private bool playerWin = false;
	public bool canUse = true;

	// Private variables.  These will not be accessible from any other class.
	private bool _onGround = false;

	// Use this for initialization
	void Start () {
		numOfLives.text = lives.ToString ("00");
		Switch = 2;
		switchUI.text = Switch.ToString ("00");
		gController = GameObject.Find ("GameController");
		if (GameObject.Find ("TutorialController") == null) {
		} else {
			if (gController.GetComponent<GameController> ().tController.GetComponent<Tutorial> ().aTutorial == true) {
				createBeam = false;
				canUse = false;
			}
		}
	}

	void Awake () {
		
		source = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		
		if (!checkGameOver () && !checkPlayerWin()) {
			if (gController.GetComponent<GameController> ().freeze == false) {
				alarm ();
			}
			transform.Translate (.01f, 0f, .01f);
			transform.Translate (-.01f, 0f, -.01f);
		}

		if (gController.GetComponent<GameController> ().freeze == false) {
			// Get the player's input axes
			float xSpeed = Input.GetAxis ("Horizontal");
			float zSpeed = Input.GetAxis ("Vertical");
			// Get the movement vector
			Vector3 velocityAxis = new Vector3 (xSpeed, 0, zSpeed);
			// Rotate the movement vector based on the camera
			velocityAxis = Quaternion.AngleAxis (Camera.main.transform.eulerAngles.y, Vector3.up) * velocityAxis;

			// Move the player
			if (!checkGameOver ()) {
				GetComponent<Rigidbody> ().AddForce (velocityAxis.normalized * Acceleration);
			}
		}

		// Check to see if the player is on the ground.
		_onGround = CheckGroundCollision();


		// Check the player's input
		if (!checkGameOver ()) {
			if (Input.GetButtonDown ("Jump") && gController.GetComponent<GameController>().freeze == false) {
				Jump ();
			}
		}

		if (checkGameOver ()) {
			if (Input.GetButtonDown ("Restart")) {
				int indexSC = SceneManager.GetActiveScene().buildIndex;
				SceneManager.LoadScene(indexSC);

			}
		}

		if (!checkGameOver ()) {
			if (Input.GetButtonDown ("Rotate Camera") && gController.GetComponent<GameController>().freeze == false) {
				//Rotate the camera around the player 90 degrees
				Camera.main.transform.RotateAround (this.transform.position, Vector3.up, 90.0f);
				this.GetComponentInChildren<Canvas> ().GetComponentInChildren<Image> ().transform.Rotate (0.0f, 0.0f, 90.0f);
			}
			LimitVelocity ();
		}

		if(checkPlayerWin()){
			alarm2 ();
			this.gameObject.GetComponent<Rigidbody> ().Sleep ();
		}

		if (checkGameOver ()) {
			GameObject.Find ("BGMusic").GetComponent<AudioSource> ().volume = 0;
			alarm2 ();
			source.PlayOneShot (snd_gameOver, 1);
		}
	}

	/// <summary>
	/// Keeps the player's velocity limited so it will not go too fast.
	/// </summary>
	private void LimitVelocity() {
		Vector2 xzVel = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z);
		if (xzVel.magnitude > MaxSpeed) {
			xzVel = xzVel.normalized * MaxSpeed;
			GetComponent<Rigidbody>().velocity = new Vector3(xzVel.x, GetComponent<Rigidbody>().velocity.y, xzVel.y);
		}
	}

	/// <summary>
	/// Checks to see if the player is on the ground.
	/// </summary>
	/// <returns><c>true</c>, if the player is touching the ground, <c>false</c> otherwise.</returns>
	public bool CheckGroundCollision(){
		// We can use a layer mask to tell the Physics Raycast which layers we are trying to hit.
		// This will allow us to restrict which objects this applies to.
		int layerMask = 1 << LayerMask.NameToLayer("Default");

		// We will get the bounds of the MeshFilter (our player's sphere) so we can
		// get the coordinates of where the bottom is.
		Bounds meshBounds = GetComponent<MeshFilter>().mesh.bounds;

		// We will use a Physics.Raycast to see if there is anything on the ground below the player.
		// We can limit the distance to make sure that we are touching the bottom of the collider.
		if (Physics.Raycast(transform.position+meshBounds.center,Vector3.down,meshBounds.extents.y,layerMask)){
			return true;
		}
		return false;
	}

	/// <summary>
	/// Applies force to the player's rigidbody to make him jump.
	/// </summary>
	private void Jump(){
		if (_onGround){
			source.PlayOneShot (snd_jump, 1);
			GetComponent<Rigidbody>().AddForce(new Vector3(0,JumpStrength,0));
		}
	}

	/// <summary>
	/// Resets the player's lives to 3 no matter what
	/// </summary>
	private void resetLives()
	{
		lives = 3;
		numOfLives.text = lives.ToString ("00");
	}

	/// <summary>
	/// Adds to the current lives the players has by addAmount
	/// PARAM: Must pass in an int that will add to the player's current lives
	/// </summary>
	private void addLives(int addAmount)
	{
		lives += addAmount;
		numOfLives.text = lives.ToString ("00");
	}

	/// <summary>
	/// subtracts to the current lives the players has by subAmount
	/// PARAM: Must pass in an int that will subtract from the player's current lives
	/// </summary>
	private void loseLives(int subAmount)
	{
		lives -= subAmount;
		numOfLives.text = lives.ToString ("00");
	}

	/// <summary>
	/// Checks to see if player has no more lives
	/// RETURNS: a bool value
	/// </summary>
	public bool checkGameOver()
	{
		if(lives <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Checks to see if player has won the level
	/// RETURNS: a bool value
	/// </summary>
	public bool checkPlayerWin()
	{
		if(playerWin == true)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// A void function that has an alarm to track when to add to powerups
	/// </summary>
	public void alarm()
	{
		timer -= Time.deltaTime;
		if (Mathf.Round(timer) == 0.0f) 
		{
			addSwitch (1);//add one extra use to Switch powerup
			timer = 50.0f;//reset timer back to 45
		}
	}

	/// <summary>
	/// A void function that has an alarm to track when to go to next scene after winning
	/// </summary>
	public void alarm2()
	{
		timer2 -= Time.deltaTime;
		if (Mathf.Round(timer2) == 0.0f) 
		{
			GameObject.Find ("BGMusic").GetComponent<AudioSource> ().volume = .4f;
		}
	}

	/// <summary>
	/// Sets Switch powerup to 3
	/// </summary>
	public void setSwitch()
	{
		Switch = 3;
		switchUI.text = Switch.ToString ("00");
	}

	/// <summary>
	/// Subtract 1 from switch
	/// </summary>
	public void useSwitch()
	{
		Switch--;
		switchUI.text = Switch.ToString ("00");
	}

	/// <summary>
	/// Add the param 'amount' to switch
	/// PARAM: Must be of type int
	/// </summary>
	public void addSwitch(int amount)
	{
		Switch += amount;
		switchUI.text = Switch.ToString ("00");
		if (Switch > 3) {
			Switch = 3;
		}
	}

	public void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name.Contains ("Out Of Bounds")) 
		{
			GameObject temp;
			temp = GameObject.Find ("GameController");
			if (SceneManager.GetActiveScene().buildIndex != 2) {
				loseLives (1);
			}
			if (lives != 0) {
				source.PlayOneShot (snd_lostLife, .4f);
			}
			temp.GetComponent<GameController> ().spawning ();
		}
		else
		if (other.gameObject.tag == "walls") {
			return;
		} else {
			source.PlayOneShot (snd_land, 1);
			gController.GetComponent<GameController> ().dirTile = other;
			if (other.gameObject.GetComponent<tile1properties> ().getMiddleTile ()) {
				//it is a middle tile
				source.PlayOneShot (snd_changeWorld, 1);
				if (other.gameObject.GetComponent<tile1properties> ().getgoToWorld () == 0) {
					source.PlayOneShot (win, 1);
					this.gameObject.layer = 10;
					GameObject.Find ("Rover").layer = 10;
					playerWin = true;
					GameObject.Find ("BGMusic").GetComponent<AudioSource> ().volume = 0;
					GameObject.Find ("WorldLocation").GetComponent<Text> ().text = "You Won";
					if (other.gameObject.tag != "Beams") {
						GameObject[] BeamArray = GameObject.FindGameObjectsWithTag ("Beams");
						if (BeamArray.Length == 0) {
							//do nothing
						} else {
							foreach (GameObject aBeam in BeamArray) {
								Object.Destroy (aBeam);
							}
						}
					}
				}
			} else {
					//Beam Direction
				if (createBeam) {
					beamCreation (other);
				}
			}
		}





	}

	public void OnCollisionStay(Collision other)
	{
		if (Input.GetKeyDown (KeyCode.P) && canUse == true) {
			if(Switch > 0)
			{
				metadataPowerUp++;
				useSwitch ();
				other.gameObject.GetComponent<tile1properties> ().swapDirection ();
				if (createBeam) {
					beamCreation (gController.GetComponent<GameController> ().dirTile);
				}
				source.PlayOneShot (snd_switch,1);
			}
		}

		if (other.gameObject.tag == "walls") {
			return;
		} else {
			if (other.gameObject.GetComponent<tile1properties> ().getUpOrDown () == false) {
				GetComponentInChildren<Canvas> ().GetComponentInChildren<Image> ().sprite = leftRight;
			} else {
				GetComponentInChildren<Canvas> ().GetComponentInChildren<Image> ().sprite = upDown;
			}
		}




		//delete previous created walls if not null

		if (other.gameObject.tag != "walls") {
			GameObject[] wallArray = GameObject.FindGameObjectsWithTag ("walls");
			if (wallArray.Length == 0) {
				//do nothing
			} else {
				foreach (GameObject awall in wallArray) {
					Object.Destroy (awall);
				}
			}


			//direction of block to limit movement UP AND DOWN WALLS
			if (other.gameObject.GetComponent<tile1properties> ().getUpOrDown ()) {
				GameObject aWall = (GameObject)Instantiate (wall, other.gameObject.GetComponent<Transform> ().position, Quaternion.identity);
				aWall.transform.Rotate (0.0f, 0.0f, 90.0f);

				//find what size block it is for one side
				if (other.gameObject.name.Contains ("Tile1")) {
					aWall.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator					aBeam.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x + 1.5f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
				}
				if (other.gameObject.name.Contains ("Tile2")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x + 1, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					aWall.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
				}
				if (other.gameObject.name.Contains ("Tile3")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x + .8f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					aWall.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
				}

				aWall = (GameObject)Instantiate (wall, other.gameObject.GetComponent<Transform> ().position, Quaternion.identity);
				aWall.transform.Rotate (0.0f, 0.0f, -90.0f);

				//find what size block it is for the other side
				if (other.gameObject.name.Contains ("Tile1")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x - 1.5f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					aWall.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
				}
				if (other.gameObject.name.Contains ("Tile2")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x - 1, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					aWall.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
				}
				if (other.gameObject.name.Contains ("Tile3")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x - .8f, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
					aWall.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
				}
			} else {//Walls go left and right
				GameObject aWall = (GameObject)Instantiate (wall, other.gameObject.GetComponent<Transform> ().position, Quaternion.identity);
				aWall.transform.Rotate (0.0f, 90.0f, 90.0f);

				//find what size block is is for one side
				if (other.gameObject.name.Contains ("Tile1")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z - 1.5f);
				}
				if (other.gameObject.name.Contains ("Tile2")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z - 1);
				}
				if (other.gameObject.name.Contains ("Tile3")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z - .8f);
				}

				aWall = (GameObject)Instantiate (wall, other.gameObject.GetComponent<Transform> ().position, Quaternion.identity);
				aWall.transform.Rotate (0.0f, -90.0f, 90.0f);

				//find what size block it is for one side
				if (other.gameObject.name.Contains ("Tile1")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z + 1.5f);
				}
				if (other.gameObject.name.Contains ("Tile2")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z + 1);
				}
				if (other.gameObject.name.Contains ("Tile3")) {
					aWall.transform.position = new Vector3 (other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z + .8f);
				}
			}
		}

		//Changing world locations
		if (other.gameObject.GetComponent<tile1properties> ().getMiddleTile ()) {
			//it is a middle tile
			if (other.gameObject.GetComponent<tile1properties> ().getgoToWorld () == 1) {
				GameObject.Find("WorldLocation").GetComponent<Text>().text  = "Sector 1";
				GameObject temp;
				temp = GameObject.Find ("GameController");
				temp.GetComponent<GameController> ().worldLocation = other.gameObject.GetComponent<tile1properties> ().getgoToWorld ();
				temp.GetComponent<GameController> ().spawning ();
				GetComponent<Rigidbody> ().velocity = new Vector3 (0, GetComponent<Rigidbody> ().velocity.y, 0);
			}
			if (other.gameObject.GetComponent<tile1properties> ().getgoToWorld () == 2) {
				GameObject.Find("WorldLocation").GetComponent<Text>().text  = "Sector 2";
				GameObject temp;
				temp = GameObject.Find ("GameController");
				temp.GetComponent<GameController> ().worldLocation = other.gameObject.GetComponent<tile1properties> ().getgoToWorld ();
				temp.GetComponent<GameController> ().spawning ();
				GetComponent<Rigidbody> ().velocity = new Vector3 (0, GetComponent<Rigidbody> ().velocity.y, 0);

			}
			if (other.gameObject.GetComponent<tile1properties> ().getgoToWorld () == 3) {
				GameObject.Find("WorldLocation").GetComponent<Text>().text  = "Sector 3";
				GameObject temp;
				temp = GameObject.Find ("GameController");
				temp.GetComponent<GameController> ().worldLocation = other.gameObject.GetComponent<tile1properties> ().getgoToWorld ();
				temp.GetComponent<GameController> ().spawning ();
				GetComponent<Rigidbody> ().velocity = new Vector3 (0, GetComponent<Rigidbody> ().velocity.y, 0);
			}
		}
	}

	public void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "tile" && other.gameObject.GetComponent<tile1properties>().canCollapse == true)
		{
			Destroy (other.gameObject);
		}
	}

	public void beamCreation(Collision other)
	{
		if (other.gameObject.tag != "Beams") {
			GameObject[] BeamArray = GameObject.FindGameObjectsWithTag ("Beams");
			if (BeamArray.Length == 0) {
				//do nothing
			} else {
				foreach (GameObject aBeam in BeamArray) {
					Object.Destroy (aBeam);
				}
			}
			//direction of block to limit movement UP AND DOWN WALLS
			if (other.gameObject.GetComponent<tile1properties> ().getUpOrDown ()) {
				GameObject aBeam = (GameObject)Instantiate (Beam, other.gameObject.GetComponent<Transform> ().position, Quaternion.identity);

				//find what size block it is
				if (other.gameObject.name.Contains ("Tile1")) {
					//aBeam.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
					aBeam.transform.localScale = new Vector3(3.0f, 1.0f , 1.0f);
				}
				if (other.gameObject.name.Contains ("Tile2")) {
					//aBeam.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
					aBeam.transform.localScale = new Vector3(2.0f, 1.0f , 1.0f);
				}
				if (other.gameObject.name.Contains ("Tile3")) {
					//aBeam.transform.SetParent (other.transform);//set as parent to move with it if it is an elevator
					aBeam.transform.localScale = new Vector3(1.5f, 1.0f , 1.0f);
				}
					
			} else {//Walls go left and right
				GameObject aBeam = (GameObject)Instantiate (Beam, other.gameObject.GetComponent<Transform> ().position, Quaternion.identity);
				aBeam.transform.Rotate (0.0f, 90.0f, 0.0f);

				//find what size block is is for one side
				if (other.gameObject.name.Contains ("Tile1")) {
					aBeam.transform.localScale = new Vector3(3.0f, 1.0f , 1.0f);
				}
				if (other.gameObject.name.Contains ("Tile2")) {
					aBeam.transform.localScale = new Vector3(2.0f, 1.0f , 1.0f);
				}
				if (other.gameObject.name.Contains ("Tile3")) {
					aBeam.transform.localScale = new Vector3(1.5f, 1.0f , 1.0f);
				}
			}
		}
	}

	public void setCreateBeam(int value)
	{
		if (value == 1) {
			createBeam = true;
		}
		if (value == 0) {
			createBeam = false;
		}
	}

	public bool getCreateBeam()
	{
		return createBeam;
	}
}