using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour {
    private AudioSource SFX_Source;

	public bool moveable = false;// if this is the orb the player is controlling
	public bool shift = false;// if the orb is allowed to move once based on dir
	public bool stop = false;
	public int dir = 0; //0 = up, 1 = right, 2 = down, 3 = left
	public GameObject puzzleCreator;
	public int col = 0;
	public int column = 0;
	public int row = 0;
	public Animation anim;
	public GameObject clock;
	public GameObject timerCube;
    public AudioClip orbMove;

	// Use this for initialization
	void Start () {
        SFX_Source = GetComponent<AudioSource>();
		puzzleCreator = GameObject.Find ("Puzzle_Creator");
		anim = GetComponent<Animation> ();
		clock = GameObject.Find ("Clock");
		timerCube = GameObject.Find ("Cube Timer Graphic");
	}
	
	// Update is called once per frame
	void Update () {
		if (moveable == true) {
			stop = true;
		}
		
		if (!puzzleCreator.GetComponent<LevelCreator> ().freeze) {
			Movement ();
		}
		if (shift == true && stop == false) {
			stop = true;
			switch (dir) {
			case 1:
				transform.Translate (1, 0, 0);
				break;
			case 2:
				transform.Translate (0, -1, 0);
				break;
			case 3:
				transform.Translate (-1, 0, 0);
				break;
			default:
				transform.Translate (0, 1, 0);
				break;
			}
			newMoveable ();
		}
	}
		


	/// <summary>
	/// Finds the new orb that got pushed out the square and makes it movable.
	/// </summary>
	public void newMoveable()
	{
		if (transform.position.y <= -puzzleCreator.GetComponent<LevelCreator> ().getCurrentRowSize()) {
			moveable = true;
			puzzleCreator.GetComponent<LevelCreator> ().resetOrbVariables ();
			puzzleCreator.GetComponent<LevelCreator> ().startcheck = true;
			puzzleCreator.GetComponent<LevelCreator> ().player = this.gameObject;
		} else if (transform.position.y > 0) {
			moveable = true;
			puzzleCreator.GetComponent<LevelCreator> ().resetOrbVariables ();
			puzzleCreator.GetComponent<LevelCreator> ().startcheck = true;
			puzzleCreator.GetComponent<LevelCreator> ().player = this.gameObject;
		} else if (transform.position.x < 0) {
			moveable = true;
			puzzleCreator.GetComponent<LevelCreator> ().resetOrbVariables ();
			puzzleCreator.GetComponent<LevelCreator> ().startcheck = true;
			puzzleCreator.GetComponent<LevelCreator> ().player = this.gameObject;
		} else if (transform.position.x >= puzzleCreator.GetComponent<LevelCreator> ().getCurrentColumnSize()) {
			moveable = true;
			puzzleCreator.GetComponent<LevelCreator> ().resetOrbVariables ();
			puzzleCreator.GetComponent<LevelCreator> ().startcheck = true;
			puzzleCreator.GetComponent<LevelCreator> ().player = this.gameObject;
		}

	}

	/// <summary>
	/// Movement for the current moveable orb
	/// </summary>
	public void Movement()
	{
		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			dir = 2;
			if (moveable == true && this.transform.position.y > -puzzleCreator.GetComponent<LevelCreator> ().getCurrentRowSize ())
            {
				RaycastHit hit;
                SFX_Source.Play();
                Ray tempRay = new Ray (new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.down);
				Physics.Raycast (tempRay, out hit, 1);
				if (hit.collider == null || hit.collider.tag == "orb") {
					transform.Translate (0, -1, 0);
				} else {
					if (hit.collider != null || hit.collider.tag == "jack") {
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			dir = 0;
			if (moveable == true && this.transform.position.y <= 0) 
			{
				RaycastHit hit;
                SFX_Source.Play();
                Ray tempRay = new Ray (new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.up);
				Physics.Raycast (tempRay, out hit, 1);
				if (hit.collider == null || hit.collider.tag == "orb") {
					transform.Translate (0, 1, 0);
				} else {
					if (hit.collider != null || hit.collider.tag == "jack") {
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
			dir = 3;
			if (moveable == true && this.transform.position.x >= 0) 
			{
				RaycastHit hit;
                SFX_Source.Play();
                Ray tempRay = new Ray (new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.left);
				Physics.Raycast (tempRay, out hit, 1);
				if (hit.collider == null || hit.collider.tag == "orb") {
					transform.Translate (-1, 0, 0);
				} else {
					if (hit.collider != null || hit.collider.tag == "jack") {
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.RightArrow))
        {
			dir = 1;
			if (moveable == true && this.transform.position.x < puzzleCreator.GetComponent<LevelCreator> ().getCurrentColumnSize ()) 
			{
				RaycastHit hit;
                SFX_Source.Play();
                Ray tempRay = new Ray (new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Vector3.right);
				Physics.Raycast (tempRay, out hit, 1);
				if (hit.collider == null || hit.collider.tag == "orb") {
					transform.Translate (1, 0, 0);
				} else {
					if (hit.collider != null || hit.collider.tag == "jack") {
					}
				}
			}
		}
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	public void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "orb") {
			other.gameObject.GetComponent<OrbScript> ().shift = true;
			moveable = false;
		}
	}

	/// <summary>
	/// Start the animation tied to the orb.
	/// </summary>
	public void animationStart()
	{
		anim.Play ();
	}

	/// <summary>
	/// Deletion animation for the orb, adds to timer, shifts row/column if needed, resets variables.
	/// </summary>
	public void animationDelete()
	{
		if (puzzleCreator.GetComponent<LevelCreator> ().flag && puzzleCreator.GetComponent<LevelCreator> ().foundColumn) {
			puzzleCreator.GetComponent<LevelCreator> ().flag = false;
			puzzleCreator.GetComponent<LevelCreator> ().shiftColumn (column);
            puzzleCreator.GetComponent<LevelCreator>().setCurrentColumnSize(-1);
            puzzleCreator.GetComponent<LevelCreator> ().freeze = false;
			clock.GetComponent<timer> ().addToTimer (10.0f);
			if (puzzleCreator.GetComponent<LevelCreator> ().player.transform.position.x > puzzleCreator.GetComponent<LevelCreator> ().getCurrentColumnSize()) {
				puzzleCreator.GetComponent<LevelCreator> ().player.transform.Translate (-1, 0, 0);
			}
		} else if (puzzleCreator.GetComponent<LevelCreator> ().flag) {
			puzzleCreator.GetComponent<LevelCreator> ().flag = false;
			puzzleCreator.GetComponent<LevelCreator> ().shiftRows (row);
            puzzleCreator.GetComponent<LevelCreator>().setCurrentRowSize(-1);
            puzzleCreator.GetComponent<LevelCreator> ().freeze = false;
			clock.GetComponent<timer> ().addToTimer (10.0f);
			if (puzzleCreator.GetComponent<LevelCreator> ().player.transform.position.y < puzzleCreator.GetComponent<LevelCreator> ().getCurrentRowSize()) {
				puzzleCreator.GetComponent<LevelCreator> ().player.transform.Translate (0, 1, 0);
			}
		}
		Destroy (this.gameObject);
	}
}
