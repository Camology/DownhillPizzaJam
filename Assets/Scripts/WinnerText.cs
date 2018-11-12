using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerText : MonoBehaviour {

	public PlayerBehavior playerScript;

	public Text scoreBox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float score = playerScript.generateScore();
		scoreBox.text = "Score: $" +  score.ToString();
	}
}
