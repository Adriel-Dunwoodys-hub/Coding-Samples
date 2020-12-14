using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void loadLevel(int level)
	{
		SceneManager.LoadSceneAsync(level);
	}

	public void quitGame()
	{
		Application.Quit ();
	}
}
