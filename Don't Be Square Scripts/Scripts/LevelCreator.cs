using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCreator : MonoBehaviour 
{
	public GameObject gameStart;
	public int squareSize = 1;
	private int currentColumnSize = 1;
	private int currentRowSize = 1;
	public int[] temp;
	public int[] temp0;
	public bool succ = false;
	public bool flag = true;
	public bool foundColumn = false;
	public bool freeze = false;
	public bool startcheck = false;
	public GameObject prefabRedOrb;
	public GameObject prefabBlueOrb;
	public GameObject prefabGreenOrb;
	public GameObject prefabOrangeOrb;
	public GameObject prefabTealOrb;
	public GameObject prefabWhiteOrb;
	public GameObject prefabPurpleOrb;
	public GameObject prefabYellowOrb;
	public GameObject prefabGrayOrb;
	public GameObject player;
	List<GameObject> orbs;

	// Use this for initialization
	void Start () 
	{
		gameStart = GameObject.Find ("Game Starter");
		squareSize = gameStart.GetComponent<GameStart> ().level + 2;
		currentColumnSize = squareSize;
		currentRowSize = squareSize;
		orbs = new List<GameObject>();
		temp = new int [squareSize];
		temp0 = new int[squareSize];

		if (gameStart.GetComponent<GameStart> ().level == 1) {
			level1 ();
		}

		if (gameStart.GetComponent<GameStart> ().level == 2) {
			level2 ();
		}

		if (gameStart.GetComponent<GameStart> ().level == 3) {
			level3 ();
		}

		if (gameStart.GetComponent<GameStart> ().level == 4) {
			level4 ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(currentRowSize == 0 || currentColumnSize == 0)
		{
			int indexSC = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadSceneAsync (indexSC + 1);
		}
		if (startcheck) {
			flag = true;
			searchColumns ();
			flag = true;
			if (!freeze) {
				searchRows ();
			}
			startcheck = false;
		}
	}

	/// <summary>
	/// Shuffle the contents of the array temp and temp0.
	/// </summary>
	public void shuffle()
	{
		int i = 0;
		for (i = 0; i < temp0.Length; i++){
			temp0 [i] = i;
		}
		i = 0;
		int upperBound = 0;
		upperBound = squareSize - 1;
		while (i < temp.Length) {
			int k = Mathf.RoundToInt (Random.Range (0.0f, upperBound));
			temp [i] = temp0 [k];
			for (int a = k; a < temp0.Length - 1; a++) {
				temp0 [a] = temp0 [a + 1];
			}
			upperBound--;
			i++;
		}
	}

	/// <summary>
	/// Resets the all objects with tag "orb".
	/// </summary>
	public void resetOrbVariables()
	{
		GameObject[] currentOrbs = new GameObject[GameObject.FindGameObjectsWithTag ("orb").Length];
		GameObject.FindGameObjectsWithTag ("orb").CopyTo (currentOrbs, 0);
		for (int i = 0; i < currentOrbs.Length; i++) {
			currentOrbs [i].GetComponent<OrbScript> ().shift = false;
			currentOrbs [i].GetComponent<OrbScript> ().stop = false;
		}
	}

	/// <summary>
	/// Searches the columns for a matching column.
	/// </summary>
	public void searchColumns()
	{
		RaycastHit hit;
		RaycastHit hit2;
		for (int i = 0; i < currentColumnSize; i++) {
			Physics.Raycast (new Vector3 (i, 0, -1), new Vector3 (0, 0, 1), out hit, 5.0f);
			for (int k = 1; k < currentRowSize; k++) {
				Physics.Raycast (new Vector3 (i, -k, -1), new Vector3 (0, 0, 1), out hit2, 5.0f);
				if (hit2.collider.gameObject.GetComponent<OrbScript> ().col == hit.collider.gameObject.GetComponent<OrbScript> ().col) {
					succ = true;
				} else {
					succ = false;
					break;
				}
			}
			if (currentRowSize == 1) {
				succ = true;
			}
			if (succ == true) {
				//call method to delete passing i
				foundColumn = true;
				freeze = true;
				deleteColumn(i);
				break;
			}
		}
	}

	/// <summary>
	/// Deletes a column.
	/// </summary>
	/// <param name="column">An integer between 0-currentColumnSize.</param>
	public void deleteColumn(int column)
	{
		RaycastHit hit;
		for (int k = 0; k < currentRowSize; k++) {
			Physics.Raycast (new Vector3 (column, -k, -1), new Vector3 (0, 0, 1), out hit, 5.0f);
			//Destroy (hit.collider.gameObject);
			hit.collider.GetComponent<OrbScript> ().animationStart ();
			hit.collider.GetComponent<OrbScript> ().column = column;
		}
        GameStart.score += (currentRowSize * 100);
	}

	/// <summary>
	/// Shifts the columns based on the param onward if needed after deletion.
	/// </summary>
	/// <param name="column">An integer between 0-currentColumnsize.</param>
	public void shiftColumn(int column)
	{
		RaycastHit hit;
		if (column < currentColumnSize - 1) {
			for (int i = column + 1; i < currentColumnSize; i++) {
				for (int k = 0; k < currentRowSize; k++) {
					Physics.Raycast (new Vector3 (i, -k, -1), new Vector3 (0, 0, 1), out hit, 5.0f);
					hit.collider.transform.Translate (new Vector3 (-1, 0, 0));
				}
			}
		}
		startcheck = true;
	}

	/// <summary>
	/// Searches the rows for a matching row.
	/// </summary>
	public void searchRows()
	{
		RaycastHit hit;
		RaycastHit hit2;
		for (int i = 0; i < currentRowSize; i++) {
			Physics.Raycast (new Vector3 (0, -i, -1), new Vector3 (0, 0, 1), out hit, 5.0f);
			for (int k = 1; k < currentColumnSize; k++) {
				Physics.Raycast (new Vector3 (k, -i, -1), new Vector3 (0, 0, 1), out hit2, 5.0f);
				if (hit2.collider.gameObject.GetComponent<OrbScript> ().col == hit.collider.gameObject.GetComponent<OrbScript> ().col) {
					succ = true;
				} else {
					succ = false;
					break;
				}
			}
			if (currentColumnSize == 1) {
				succ = true;
			}
			if (succ == true) {
				//call method to delete passing i
				foundColumn = false;
				freeze = true;
				deleteRow(i);
				break;
			}
		}
	}

	/// <summary>
	/// Deletes a row.
	/// </summary>
	/// <param name="row">An integer between 0-currentRowSize.</param>
	public void deleteRow(int row)
	{
		RaycastHit hit;
		for (int k = 0; k < currentColumnSize; k++) {
			Physics.Raycast (new Vector3 (k, -row, -1), new Vector3 (0, 0, 1), out hit, 5.0f);
			hit.collider.GetComponent<OrbScript> ().animationStart ();
			hit.collider.GetComponent<OrbScript> ().row = row;
		}
        GameStart.score += (currentColumnSize * 100);
	}

	/// <summary>
	/// Shifts the rows based on the param if needed after deletion.
	/// </summary>
	/// <param name="row">An integer between 0-currentRowSize.</param>
	public void shiftRows(int row)
	{
		RaycastHit hit;
		if (row < currentRowSize - 1) {
			for (int i = row + 1; i < currentRowSize; i++) {
				for (int k = 0; k < currentColumnSize; k++) {
					Physics.Raycast (new Vector3 (k, -i, -1), new Vector3 (0, 0, 1), out hit, 5.0f);
					hit.collider.transform.Translate (new Vector3 (0, 1, 0));
				}
			}
		}
		startcheck = true;
	}

	/// <summary>
	/// adds/sub private variable currentColumnSize. To subtract make param a negative #.
	/// </summary>
	/// <returns>
	/// Void
	/// </returns>
	/// <param name="setter"> An integer value.</param>
	public void setCurrentColumnSize(int setter)
	{
		currentColumnSize += setter; 
	}

	/// <summary>
	/// Gets the size of the current column.
	/// </summary>
	/// <returns>The current column size.</returns>
	public int getCurrentColumnSize()
	{
		return currentColumnSize;
	}

	/// <summary>
	/// adds/sub private variable currentRowSize. To subtract make param a negative #.
	/// </summary>
	/// <param name="setter">An integer value.</param>
	public void setCurrentRowSize(int setter)
	{
		currentRowSize += setter;
	}

	/// <summary>
	/// Gets the size of the current row.
	/// </summary>
	/// <returns>The current row size.</returns>
	public int getCurrentRowSize()
	{
		return currentRowSize;
	}

    public void DeleteOneColorPowerup()
    {
        //GameObject[] orbTemp = new GameObject [getCurrentColumnSize()*getCurrentRowSize()];
        GameObject[] orbTemp;
        orbTemp = GameObject.FindGameObjectsWithTag("orb");

        //find orbs of the player orb color and deactivate them all
        for(int i = 0; i < orbTemp.Length; i++)
        {
            if(orbTemp[i].GetComponent<OrbScript>().col == player.GetComponent<OrbScript>().col)
            {
                orbTemp[i].SetActive(false);
            }
        }
        setCurrentRowSize(-1);

        //Create another orbtemp array that now has all orbs that werent deactivated
        GameObject[] orbTemp2;
        orbTemp2 = GameObject.FindGameObjectsWithTag("orb");
        //Rearange the orbs in orbtemp2[] properly into proper grid format
        int q = 0;
        //Store the total number of orbs in the game
        int currentNumberOfOrbs = orbTemp2.Length;
        for (float k = 0.0f; k < getCurrentRowSize(); k++)
        {
            for (float i = 0.0f; i < getCurrentColumnSize(); i++)
            {
                
                orbTemp2[q].transform.position = new Vector3(i, 0.0f + -k, 0.0f);
                q++;
            }
        }

        //Go through orbtemp[] and delete all the deactivated colored orbs
        for (int i = 0; i < orbTemp.Length; i++)
        {
            if (orbTemp[i].GetComponent<OrbScript>().col == player.GetComponent<OrbScript>().col)
            {
                Destroy(orbTemp[i]);
            }
        }

        //Check to see if we have to add 1 more orb to be player orb if even or just take last orb in orbTemp and put it at top left corner if odd
        //Check if even #
        if(currentNumberOfOrbs % 2 == 0)
        {
            int p = 0;
            //we have to add 1 more orb
            p = Mathf.RoundToInt(Random.Range(0.0f, orbTemp2.Length - 1));
            GameObject temporb = Instantiate(orbTemp2[p], new Vector3(-1, 1, 0), Quaternion.identity);
            temporb.GetComponent<OrbScript>().moveable = true;
            player = temporb;
			searchColumns();
			searchRows();
        }
        else //It was an odd #
        {
            orbTemp2[orbTemp2.Length - 1].transform.position = new Vector3(-1, 1, 0);
            orbTemp2[orbTemp2.Length - 1].GetComponent<OrbScript>().moveable = true;
            player = orbTemp2[orbTemp2.Length - 1];
			searchRows();
			searchColumns();
        }
    }

	/// <summary>
	/// Initializes level 1 puzzle randomly.
	/// </summary>
	public void level1()
	{
		orbs.Add (prefabRedOrb);
		orbs.Add (prefabGreenOrb);
		orbs.Add (prefabBlueOrb);
		for (int k = 0; k < orbs.Count; k++) {
			shuffle ();
			for (int i = 0; i < orbs.Count; i++) {
				Instantiate (orbs [temp [i]], new Vector3 (i, 0 + -k, 0), Quaternion.identity);
			}
		}
		GameObject temporb = Instantiate(orbs[Mathf.RoundToInt(Random.Range(0.0f,orbs.Count-1))], new Vector3(-1, 1,0), Quaternion.identity);
		temporb.GetComponent<OrbScript> ().moveable = true;
		player = temporb;
	}

	/// <summary>
	/// Initializes level 2 puzzle ramdomly.
	/// </summary>
	public void level2()
	{
		orbs.Add (prefabRedOrb);
		orbs.Add (prefabGreenOrb);
		orbs.Add (prefabBlueOrb);
		orbs.Add (prefabPurpleOrb);
		for (int k = 0; k < orbs.Count; k++) {
			shuffle ();
			for (int i = 0; i < orbs.Count; i++) {
				Instantiate (orbs [temp [i]], new Vector3 (i, 0 + -k, 0), Quaternion.identity);
			}
		}
		GameObject temporb = Instantiate(orbs[Mathf.RoundToInt(Random.Range(0.0f,orbs.Count-1))], new Vector3(-1, 1,0), Quaternion.identity);
		temporb.GetComponent<OrbScript> ().moveable = true;
		player = temporb;
	}

	/// <summary>
	/// Initializes level 3 puzzle randomly.
	/// </summary>
	public void level3()
	{
		orbs.Add (prefabRedOrb);
		orbs.Add (prefabGreenOrb);
		orbs.Add (prefabBlueOrb);
		orbs.Add (prefabPurpleOrb);
		orbs.Add (prefabOrangeOrb);
		for (int k = 0; k < orbs.Count; k++) {
			shuffle ();
			for (int i = 0; i < orbs.Count; i++) {
				Instantiate (orbs [temp [i]], new Vector3 (i, 0 + -k, 0), Quaternion.identity);
			}
		}
		GameObject temporb = Instantiate(orbs[Mathf.RoundToInt(Random.Range(0.0f,orbs.Count-1))], new Vector3(-1, 1,0), Quaternion.identity);
		temporb.GetComponent<OrbScript> ().moveable = true;
		player = temporb;
	}

	/// <summary>
	/// Initializes level 4 puzzle randomly
	/// </summary>
	public void level4()
	{
		orbs.Add (prefabRedOrb);
		orbs.Add (prefabGreenOrb);
		orbs.Add (prefabBlueOrb);
		orbs.Add (prefabPurpleOrb);
		orbs.Add (prefabOrangeOrb);
		orbs.Add (prefabYellowOrb);
		for (int k = 0; k < orbs.Count; k++) {
			shuffle ();
			for (int i = 0; i < orbs.Count; i++) {
				Instantiate (orbs [temp [i]], new Vector3 (i, 0 + -k, 0), Quaternion.identity);
			}
		}
		GameObject temporb = Instantiate(orbs[Mathf.RoundToInt(Random.Range(0.0f,orbs.Count-1))], new Vector3(-1, 1,0), Quaternion.identity);
		temporb.GetComponent<OrbScript> ().moveable = true;
		player = temporb;
	}
}