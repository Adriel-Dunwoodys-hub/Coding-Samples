using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackController : MonoBehaviour 
{
	private float jackSpawnTimer = 4.0f;
	private int jackX;
	private int jackY;
	private int maxMinJackX;
	private int maxMinJackY;
	private int numOfSpawns = 1;
	private bool staticspawnX;

	public GameObject jackPrefab;
	public GameObject puzzleCreator;
    public bool justOne = true;

	// Use this for initialization
	void Start () 
	{
		//puzzleCreator = GameObject.Find ("Puzzle_Creator");
	}
	
	// Update is called once per frame
	void Update () 
	{
		jackSpawnTimer -= Time.deltaTime;
		if (jackSpawnTimer <= 0) {
			spawnJack ();
			jackSpawnTimer = 4.0f;
		}
		
	}

	/// <summary>
	/// Gets the current jack spawning timer.
	/// </summary>
	/// <returns>The jack spawning timer.</returns>
	public float getJackTimer()
	{
		return jackSpawnTimer;
	}

	/// <summary>
	/// Resets the jack spawn timer.
	/// </summary>
	public void resetJackSpawnTimer()
	{
		jackSpawnTimer = 4.0f;
	}

	/// <summary>
	/// Spawns the jack.
	/// </summary>
	public void spawnJack()
	{
        if (!justOne)
        {
            int temp;
            temp = Mathf.RoundToInt(Random.Range(0.0f, 1.0f));

            if (numOfSpawns % 2 == 1)
            {
                if (temp == 0)
                {//X is static in being min or max
                    int[] tempX = { -1, puzzleCreator.GetComponent<LevelCreator>().getCurrentColumnSize() };
                    maxMinJackX = tempX[Mathf.RoundToInt(Random.Range(0.0f, 1.0f))];
                    jackY = Mathf.RoundToInt(Random.Range(1.0f, -puzzleCreator.GetComponent<LevelCreator>().getCurrentRowSize()));
                    Instantiate(jackPrefab, new Vector3(maxMinJackX, jackY, 0), Quaternion.identity);
                    staticspawnX = true;
                    numOfSpawns++;
                }

                if (temp == 1)
                {//Y is static in being min or max
                    int[] tempY = { 1, -puzzleCreator.GetComponent<LevelCreator>().getCurrentRowSize() };
                    maxMinJackY = tempY[Mathf.RoundToInt(Random.Range(0.0f, 1.0f))];
                    jackX = Mathf.RoundToInt(Random.Range(-1.0f, puzzleCreator.GetComponent<LevelCreator>().getCurrentColumnSize()));
                    Instantiate(jackPrefab, new Vector3(jackX, maxMinJackY, 0), Quaternion.identity);
                    staticspawnX = false;
                    numOfSpawns++;
                }
            }
            else if (numOfSpawns % 2 == 0)//it is an even # just mirror the last spawn 
            {
                if (staticspawnX)
                {
                    if (maxMinJackX == puzzleCreator.GetComponent<LevelCreator>().getCurrentColumnSize())
                    {
                        //X is currently max mirror with min
                        Instantiate(jackPrefab, new Vector3(-1, jackY, 0), Quaternion.identity);
                    }
                    else
                    {
                        //X is currently min mirror with max
                        Instantiate(jackPrefab, new Vector3(puzzleCreator.GetComponent<LevelCreator>().getCurrentColumnSize(), jackY, 0), Quaternion.identity);
                    }
                }
                else //it was a static Y spawn 
                {
                    if (maxMinJackY == -puzzleCreator.GetComponent<LevelCreator>().getCurrentRowSize())
                    {
                        //Y is currently 'max' mirror with min
                        Instantiate(jackPrefab, new Vector3(jackX, 1, 0), Quaternion.identity);
                    }
                    else
                    {
                        //Y is currently min mirror with 'max'
                        Instantiate(jackPrefab, new Vector3(jackX, -puzzleCreator.GetComponent<LevelCreator>().getCurrentRowSize(), 0), Quaternion.identity);
                    }
                }
                numOfSpawns++;
            }
        }
        else
        {
            int temp;
            temp = Mathf.RoundToInt(Random.Range(0.0f, 1.0f));

            if (temp == 0)
            {//X is static in being min or max
                int[] tempX = { -1, puzzleCreator.GetComponent<LevelCreator>().getCurrentColumnSize() };
                maxMinJackX = tempX[Mathf.RoundToInt(Random.Range(0.0f, 1.0f))];
                jackY = Mathf.RoundToInt(Random.Range(1.0f, -puzzleCreator.GetComponent<LevelCreator>().getCurrentRowSize()));
                Instantiate(jackPrefab, new Vector3(maxMinJackX, jackY, 0), Quaternion.identity);
                staticspawnX = true;
                numOfSpawns++;
            }

            if (temp == 1)
            {//Y is static in being min or max
                int[] tempY = { 1, -puzzleCreator.GetComponent<LevelCreator>().getCurrentRowSize() };
                maxMinJackY = tempY[Mathf.RoundToInt(Random.Range(0.0f, 1.0f))];
                jackX = Mathf.RoundToInt(Random.Range(-1.0f, puzzleCreator.GetComponent<LevelCreator>().getCurrentColumnSize()));
                Instantiate(jackPrefab, new Vector3(jackX, maxMinJackY, 0), Quaternion.identity);
                staticspawnX = false;
                numOfSpawns++;
            }
        }
	}
}