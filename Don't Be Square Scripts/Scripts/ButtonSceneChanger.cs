using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonSceneChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Pass in an int to change to that scene in build index.
	/// </summary>
	public void ChangeToScene(int scene)
	{
		SceneManager.LoadScene (scene);
        GameStart.score = 0;
	}
}