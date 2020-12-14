using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAni : MonoBehaviour 
{
	public int translateSpeed = 5;
	public int rotateSpeed = 800;
	public Material[] material = new Material[9];
	public float currCountdownValue;
	public GameObject gameController;
	public bool beginAni = true;

	// Use this for initialization
	void Start () 
	{
		translateSpeed = 5;
		rotateSpeed = 800;
		gameController = GameObject.Find("Game Controller");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameController.GetComponent<GameController> ().getGameOver ()) 
		{
			if (beginAni) {
				StartCoroutine (StartCountdown ());
				beginAni = false;
			}
			transform.Translate (Vector3.down * (Time.deltaTime * translateSpeed), Space.World);
			transform.Rotate (Vector3.left * (Time.deltaTime * rotateSpeed));
		}
	}

	public IEnumerator StartCountdown(float countdownValue = 4)
	{
		currCountdownValue = countdownValue;
		while (currCountdownValue > 0) {
			yield return new WaitForSeconds (0.1f);
			currCountdownValue-=.1f;
			this.gameObject.GetComponent<MeshRenderer> ().material = material [Mathf.RoundToInt (Random.Range (0.0f, 8.0f))];
		}
		GameObject.Destroy (this.gameObject);
	}
}
