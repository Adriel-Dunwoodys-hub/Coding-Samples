﻿using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour
{
	public Texture2D fadeoutTexture;
	public GUITexture fOT;
	public float fadeSpeed = .8f;

	//private int drawDepth = 1;
	//private float alpha = 1.0f;
	//private int fadeDir = -1;

	/*void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),fadeoutTexture);
	}*/

	/*public IEnumerator FadeOut (float speed)
	{
		while (fadeoutTexture.alphaIsTransparency) {
		}
	}*/
}
